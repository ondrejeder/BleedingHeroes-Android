using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public float moveSpeed;

    private Vector2 moveInput;
    private Vector2 moveInputPC;  // wasd controls on pc unity editor

    public Rigidbody2D theRB;

    public float dmgToGive;  
    

    private float rotDelta;   // set value to subtract from angle when rotating weaponArm

    //private Camera theCamera;

    public Animator theAnimator;


    public Transform moveJoyBackground;    // move joy
    public Transform moveJoy;   // move joy
    //public Transform aimJoystick;   // aim joy
    //public Transform aimJoystickBackground;   // aim joy


    public GameObject bulletToFire;
    public Transform firePoint;

    public float timeBtwShots;
    private float shotCounter;

    public float distanceToLockOnEnemy;
    public float distanceToClosestEnemy;
    public Enemy closestEnemy;

    public bool canMove = true;

     

    public SpriteRenderer bodySR;

    private int notLockedSpread;
    private int lockedSpread;

    private bool wantsToShoot;

    //public GameObject aimJoyBackgroundObj;

    //public GameObject barrageProjectile;

    //public GameObject shootVolleyButtton;
    public GameObject shootOnceButton;
    //public GameObject shootBarrageButton;

    public GameObject[] foes;
    
    public float shootSelfDmg;   

    private bool facingR, facingL;

    private bool isShooting;

    private bool hasShotRecently;
    public float recentShotLength;
    private float recentShotCounter;

    private int dmgToGiveDefault;

    public int projectileCount;

    public GameObject bulletSize2, bulletSize3;

    private GameObject actualBullet;
    


    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        PlayerController.instance.transform.position = new Vector3(-4f, 2f, 0f);  // set player starting position

        notLockedSpread = Random.Range(-4, 4);

        // determines wheter enemy facing L or R
        if (transform.localScale.x < 0)
        {
            facingL = true;
        }
        if (transform.localScale.x > 0)
        {
            facingR = true;
        }

        //dmgToGiveDefault = dmgToGive;


        //dmgToGive = CharacterTracker.instance.playerDMG;
        //shootSelfDmg = CharacterTracker.instance.playerSelfDMG;
    }





    void Update()
    {
        // gets values from character tracker
        projectileCount = CharacterTracker.instance.projectileCount;
        dmgToGive = CharacterTracker.instance.playerDMG;
        shootSelfDmg = CharacterTracker.instance.playerSelfDMG;
        moveSpeed = CharacterTracker.instance.playerMoveSpeed;

        switch (CharacterTracker.instance.projectileSize)
        {
            case 1:
                actualBullet = bulletToFire;
                break;
            case 2:
                actualBullet = bulletSize2;
                break;
            case 3:
                actualBullet = bulletSize3;
                break;
            
        }


        // SETTING ANIMATOR STATES 

        if (moveInput != Vector2.zero)
        {
            theAnimator.SetBool("moving", true);
        }
        else
        {
            theAnimator.SetBool("moving", false);
        }


       

        // ROTATE PLAYER by joysticks
        /*
        if (aimJoyBackgroundObj.activeInHierarchy)   // touching area and float joystick is active   // set orientation by aimJoy
        {
            if(aimJoystickBackground.position.x < aimJoystick.position.x)  
            {
                //transform.localScale = new Vector3(1.35f, 1.35f, 1.35f);    // !! NEED TO ADJUST HERE IF DIFFERENT SCALING OF PLAYER !!
                if (facingL)
                {
                    transform.localScale = new Vector3(transform.localScale.x * -1f, transform.localScale.y, transform.localScale.z);  // flip x value
                    facingL = false;
                    facingR = true;
                }
            }
            else
            {
                //transform.localScale = new Vector3(-1.35f, 1.35f, 1.35f);
                if (facingR)
                {
                    transform.localScale = new Vector3(transform.localScale.x * -1f, transform.localScale.y, transform.localScale.z);  // flip x value
                    facingR = false;
                    facingL = true;
                }
            }
        }
        */
         if (Vector3.Distance(moveJoy.position, moveJoyBackground.position) > 15f)    // moveJoy moved more than given distance
        {

            if (closestEnemy)
            {
                if (!isShooting && Vector3.Distance(instance.transform.position, closestEnemy.transform.position) > distanceToLockOnEnemy)  // if not shooting get oriented by movejoy
                {
                    MoveViaMoveJoy();
                    
                }
            }
            else
            {
                MoveViaMoveJoy();
                
            }
        }


        // AIMING (for shoot button)

        if (closestEnemy)   // closest enemy exists
        {
            if (Vector3.Distance(instance.transform.position, closestEnemy.transform.position) < distanceToLockOnEnemy)  // in lockOn distance
            {
                
                    Vector2 aimDirection = new Vector2(closestEnemy.transform.position.x - firePoint.transform.position.x, closestEnemy.transform.position.y - firePoint.transform.position.y);
                    float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
                    firePoint.rotation = Quaternion.Euler(0, 0, angle);

                    Debug.DrawLine(firePoint.transform.position, closestEnemy.transform.position, Color.red);  // debug line showing aim direction
            }
           
            else  // outside lockOn distance
            {
                    Vector2 aimDirection = new Vector2(closestEnemy.transform.position.x - firePoint.transform.position.x, closestEnemy.transform.position.y - firePoint.transform.position.y);
                    float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
                    firePoint.rotation = Quaternion.Euler(0, 0, (angle - notLockedSpread));   // bcs outside of lockOn add SPREAD

                    Debug.DrawLine(firePoint.transform.position, closestEnemy.transform.position, Color.red);  // debug line showing aim direction
            }
        }

        else   // closestenemy doesnt exist
        {
            if(transform.localScale.x > 0)  // if player facing right
            {
                firePoint.rotation = Quaternion.Euler(0, 0, 0);   // firepoint aims to right
            }
            else
            {
                firePoint.rotation = Quaternion.Euler(0, 0, 180);   // firepoint aims left
            }

        }


        /*
        // AIMING with aimJoy

        if (aimJoyBackgroundObj.activeInHierarchy)  // when  aimJoy active, set rotation by that
        {
            Vector2 aimDirection = new Vector2(aimJoystick.position.x - aimJoystickBackground.position.x, aimJoystick.position.y - aimJoystickBackground.position.y);
            float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
            firePoint.rotation = Quaternion.Euler(0, 0, angle);
        }
        */






        // SHOOTING 

        /*
        if (CrossPlatformInputManager.GetButton("shootClick") )
        {
            shotCounter -= Time.deltaTime;

            isShooting = true;
            

            //recentShotCounter = recentShotLength;

            

            if(CrossPlatformInputManager.GetButton("shootClick"))   // set rotation according to enemy just when touching the button and not when wantstoshoot (aimJoy active)
            {
                if (closestEnemy)
                {
                    if (instance.transform.position.x < closestEnemy.transform.position.x)    // if shooting player rotates towards closest enemy
                    {
                        //transform.localScale = new Vector3(1.35f, 1.35f, 1.35f);
                        if (facingL)
                        {
                            transform.localScale = new Vector3(transform.localScale.x * -1f, transform.localScale.y, transform.localScale.z);  // flip x value
                            facingL = false;
                            facingR = true;
                        }
                    }
                    else
                    {
                        //transform.localScale = new Vector3(-1.35f, 1.35f, 1.35f);
                        if (facingR)
                        {
                            transform.localScale = new Vector3(transform.localScale.x * -1f, transform.localScale.y, transform.localScale.z);  // flip x value
                            facingR = false;
                            facingL = true;
                        }
                    }
                }
            }

            


            if (shotCounter <= 0f)
            {
                Instantiate(actualBullet, firePoint.position, firePoint.rotation);


                shotCounter = timeBtwShots;

                PlayerHealthController.instance.DamagePlayer(shootSelfDmg);   // self dmg player when shooting
            }
            


        }
        else
        {
            isShooting = false;
        }

        
        */

    }
    


    private void FixedUpdate()
    {
        /*
        if (aimJoystick.position.x > 5 && aimJoystick.position.y > 5)
        {
            wantsToShoot = true;
        }

        if (!aimJoyBackgroundObj.activeInHierarchy)
        {
            wantsToShoot = false;
        }
        */

        notLockedSpread = Random.Range(-4, 4);

        FindClosestEnemy();



        moveInput.x = CrossPlatformInputManager.GetAxisRaw("HorizontalJoy");
        moveInput.y = CrossPlatformInputManager.GetAxisRaw("VerticalJoy");


        moveInputPC.x = Input.GetAxisRaw("Horizontal");   // pc input using wasd in editor
        moveInputPC.y = Input.GetAxisRaw("Vertical");

        moveInput.Normalize();

        if (canMove)
        {
            if (moveInput != Vector2.zero)  // if joystick the use joystick
            {
                theRB.velocity = moveInput * moveSpeed;
            }
            else  // not using joystick then use wasd
            {
                theRB.velocity = moveInputPC * moveSpeed;
            }
        }
    }





    void FindClosestEnemy()
    {
        float distanceToClosestEnemy = Mathf.Infinity;
        closestEnemy = null;
        Enemy[] allEnemies = GameObject.FindObjectsOfType<Enemy>();

        if(allEnemies.Length == 0)
        {
            
        }
        else
        {
            foreach (Enemy currentEnemy in allEnemies)
            {
                float distanceToEnemy = (currentEnemy.transform.position - this.transform.position).sqrMagnitude;
                if (distanceToEnemy < distanceToClosestEnemy)
                {
                    distanceToClosestEnemy = distanceToEnemy;
                    closestEnemy = currentEnemy;

                }
            }
        }

        // Debug.DrawLine(this.transform.position, closestEnemy.transform.position);  // draw line to closest enemy

    }


    
    
    
    public void ShootOnce()
    {
        if (projectileCount == 1)
        {
            Instantiate(actualBullet, firePoint.position, firePoint.rotation);
        }
        if(projectileCount == 2)
        {

            Instantiate(actualBullet, firePoint.position, firePoint.rotation);
            Instantiate(actualBullet, new Vector3(firePoint.position.x, firePoint.position.y + 0.66f, firePoint.position.z), firePoint.rotation);  // spawns second projectile 0.66 higher
        }
        if(projectileCount >= 3)
        {

            Instantiate(actualBullet, firePoint.position, firePoint.rotation);
            Instantiate(actualBullet, new Vector3(firePoint.position.x, firePoint.position.y + 0.75f, firePoint.position.z), firePoint.rotation);  // spawns second projectile 0.66 higher
            Instantiate(actualBullet, new Vector3(firePoint.position.x, firePoint.position.y - 0.75f, firePoint.position.z), firePoint.rotation);
        }
        




        PlayerHealthController.instance.DamagePlayer(shootSelfDmg);   // goes to playerhealthcontroller script to call dmgplayer function



        if (closestEnemy)
        {
            if (instance.transform.position.x < closestEnemy.transform.position.x)    // if shooting player rotates towards closest enemy
            {
                //transform.localScale = new Vector3(1.35f, 1.35f, 1.35f);
                if (facingL)
                {
                    transform.localScale = new Vector3(transform.localScale.x * -1f, transform.localScale.y, transform.localScale.z);  // flip x value
                    facingL = false;
                    facingR = true;
                }
            }
            else
            {
                //transform.localScale = new Vector3(-1.35f, 1.35f, 1.35f);
                if (facingR)
                {
                    transform.localScale = new Vector3(transform.localScale.x * -1f, transform.localScale.y, transform.localScale.z);  // flip x value
                    facingR = false;
                    facingL = true;
                }
            }
        }
    }
    

    public void MoveViaMoveJoy()
    {
        if (moveJoyBackground.position.x < moveJoy.position.x)
        {
            //transform.localScale = new Vector3(1.35f, 1.35f, 1.35f);
            if (facingL)
            {
                transform.localScale = new Vector3(transform.localScale.x * -1f, transform.localScale.y, transform.localScale.z);  // flip x value
                facingL = false;
                facingR = true;
            }
        }
        else
        {
            //transform.localScale = new Vector3(-1.35f, 1.35f, 1.35f);
            if (facingR)
            {
                transform.localScale = new Vector3(transform.localScale.x * -1f, transform.localScale.y, transform.localScale.z);  // flip x value
                facingR = false;
                facingL = true;
            }
        }
    }


    public void IncreaseAttack()
    {
        //dmgToGive += 10;
        CharacterTracker.instance.playerDMG += 15;

        Debug.Log("AttackIncreased");
        //CharacterTracker.instance.attackBuffNo++;
    }




}
