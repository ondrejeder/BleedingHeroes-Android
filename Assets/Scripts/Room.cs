using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public bool closeWhenEntered, openWhenEnemiesCleared = true;
    //public bool activateEnemies;
    public bool firstRoom;
    public bool lastRoom;
    private bool roomActive = false;
    private bool roomCleared;
    private bool enemiesSpawnDone;

    public GameObject[] doors;  // door array
    // public GameObject[] foes; // enemies array
    public List<GameObject> enemies = new List<GameObject>();   // list of enemies


    private int numberOfEnemies;
    private int numberOfSpikes;

    public int minEnemiesIn, maxEnemiesOut, minSpikesIn, maxSpikesOut;

    private int enemiesToSpawn = 0;
    private int spikesToSpawn = 0;

    // referencing various enemies
    public GameObject meleeSkeleton;
    public GameObject shootingSkeleton;
    public GameObject giantSkeleton;
    public GameObject redDevilSkeleton1, redDevilSkeleton2, redDevilSkeleton3, redDevilSkeleton4;
    public GameObject doubleHeadShotSkeleton;

    public GameObject spikes;


    private float selectedX, selectedY;
    private float selectedXSpikes, selectedYSpikes;

    private Vector3 selectedPosition;

    private bool bossSpawned = false;
    private bool winObjectspawned;

    public GameObject winObject;

    public GameObject pathStoneH, pathStoneV;
    private bool pathStonesSpawned = false;

    //public bool dontOpenDoors;

    private int roomsClearedNo;

    private int roomsToSpawnNo;

    private GameObject[] altars;
    private GameObject wanderingSoul;

    public GameObject roomHider;



    

    

    void Start()
    {
        spikesToSpawn = Random.Range(minSpikesIn, maxSpikesOut);    // decide how many enemies to spawn
        enemiesToSpawn = Random.Range(minEnemiesIn, maxEnemiesOut);   // decide how many enemies to spawn

        roomsToSpawnNo = FindObjectOfType<LevelGenerator>().roomsToSpawn;


        wanderingSoul = GameObject.FindObjectOfType<WanderingSoul>().gameObject;

        altars = GameObject.FindGameObjectsWithTag("altar");   // search for altars based on tag

    }


    void Update()
    {
        roomsClearedNo = CharacterTracker.instance.roomsClearedNo;

        // ENEBLE and DISABLE BOX COLLIDER FOR ALTARS
        if (roomActive)  
        {
            

            if (enemies.Count > 0)
            {
                foreach(GameObject altar in altars)
                {
                    //SpriteRenderer renderer = altar.GetComponent<SpriteRenderer>();
                    //renderer.enabled = false;

                    BoxCollider2D boxCol = altar.GetComponent<BoxCollider2D>();
                    boxCol.enabled = false;
                    
                }      
            }
            if(enemies.Count == 0)
            {
                foreach (GameObject altar in altars)
                {
                    //SpriteRenderer renderer = altar.GetComponent<SpriteRenderer>();
                    //renderer.enabled = true;

                    BoxCollider2D boxCol = altar.GetComponent<BoxCollider2D>();
                    boxCol.enabled = true;
                }
            }
        }

        // ENEBLE and DISABLE BOX COLLIDER FOR WS
        if(roomActive && wanderingSoul.activeInHierarchy)
        {
            
            BoxCollider2D boxCol = wanderingSoul.GetComponent<BoxCollider2D>();

            if (enemies.Count > 0)
            {
                
                boxCol.enabled = false;
            }

            if (enemies.Count == 0)
            {
               
                boxCol.enabled = true;
            }


        }



        if (enemies.Count > 0 && roomActive && openWhenEnemiesCleared)   // controlling enemies list and closing/opening doors
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i] == null)   // enemies list at i place null then remove that place from list
                {
                    enemies.RemoveAt(i);
                    i--;
                }
            }

            

            if (enemies.Count == 0)   // no enemy
            {

                if (enemiesSpawnDone)   // already spawned all enemies
                {
                    foreach (GameObject door in doors)
                    {
                        door.SetActive(false);

                    }
                }
            }


        }

        if (numberOfEnemies == enemiesToSpawn || lastRoom)
        {
            enemiesSpawnDone = true;   // set room as "cleared" when all enemies spawned

        }


        if (roomActive && lastRoom && !bossSpawned)  // SPAWN GIANT SKELETON
        {
            SpawnBoss();
            bossSpawned = true;
        }


        if (roomActive && !firstRoom && !lastRoom && numberOfEnemies < enemiesToSpawn)
        {
            SpawnEnemies();
        }


        if (roomActive && !firstRoom && !lastRoom && numberOfSpikes < spikesToSpawn)  // SPAWN SPIKES
        {
            SpawnSpikes();
        }


        if (lastRoom && enemiesSpawnDone && enemies.Count == 0 && roomActive && !winObjectspawned)
        {
            Instantiate(winObject, transform.position, transform.rotation);
            winObjectspawned = true;
        }


        if (roomActive && !pathStonesSpawned)  // spawn Pathstones when not spawned
        {
            SpawnPathStones();
        }


        if (roomActive && enemies.Count == 0 && !roomCleared)  // add 1 to CHtracker of roomCleared
        {
            CharacterTracker.instance.roomsClearedNo++;
            roomCleared = true;
        }


        if(lastRoom && roomsClearedNo < roomsToSpawnNo)   // lock the doors 
        {
            foreach (GameObject door in doors)
            {
                door.SetActive(true);    
            }
        }
        if(lastRoom && roomsClearedNo >= roomsToSpawnNo - 1 && !roomActive)
        {
            foreach (GameObject door in doors)
            {
                door.SetActive(false);    
            }
        }
        

    }



    private void FixedUpdate()
    {
        selectedX = Random.Range(-10, 10);
        selectedY = Random.Range(-3, 3);

        selectedXSpikes = Random.Range(-10, 10);
        selectedYSpikes = Random.Range(-3, 3);

    }

    private void OnTriggerEnter2D(Collider2D other)  // PLAYER ENTERING ROOM
    {
        if(other.tag == "Player")
        {
            CameraController.instance.ChangeTarget(transform);

            //Debug.Log("roomActivator");

            if (roomHider != null)
            {
                roomHider.SetActive(false);
            }

            if(closeWhenEntered)   // activate doors when entering a room
            {
                foreach(GameObject door in doors)
                {
                    door.SetActive(true);    // activate doors when entering a room
                }
            }

            roomActive = true;    // set room as active when player enter the trigger collision
        }

        

        
    }


    private void OnTriggerExit2D(Collider2D other)  // PLAYER EXIT ROOM
    {
        if (other.tag == "Player")
        {
            roomActive = false;        // set room as inactive when player exit the trigger collision
            closeWhenEntered = false;
        }
    }


    public void SpawnEnemies()  // SPAWN ENEMIES
    {
        float selectedEnemy = Random.Range(1, 3);  // can choose 1 or 2
        
        switch (selectedEnemy)    
        {
            case 1:   // melee skeleton
                enemies.Add( Instantiate(meleeSkeleton, transform.position + new Vector3(selectedX, selectedY, 0f), transform.rotation));
                break;
            case 2:   // shooting skeleton
                enemies.Add(Instantiate(shootingSkeleton, transform.position + new Vector3(selectedX, selectedY, 0f), transform.rotation));
                break;
        }

        numberOfEnemies++;   // add 1 to no. of enemies
    }

    public void SpawnSpikes()   // SPAWN SPIKES
    {
        Instantiate(spikes, transform.position + new Vector3(selectedXSpikes, selectedYSpikes, 0f), transform.rotation);

        numberOfSpikes++;   // add 1 to int no. of spikes
    }

    public void SpawnBoss()  // SPAWN BOSS ENEMY
    {
        float selectedBoss = Random.Range(1, 3);


        switch(selectedBoss)
        {
            case 1:
                enemies.Add(Instantiate(giantSkeleton, transform.position, transform.rotation));
                break;
            case 2:

                enemies.Add(Instantiate(doubleHeadShotSkeleton, transform.position, transform.rotation));

                break;


        }

    }

    public void SpawnPathStones()
    {
        Instantiate(pathStoneV, transform.position + new Vector3(14, 0, 0), transform.rotation);
        Instantiate(pathStoneV, transform.position + new Vector3(-14, 0, 0), transform.rotation);
        Instantiate(pathStoneH, transform.position + new Vector3(0, 6.2f, 0), transform.rotation);
        Instantiate(pathStoneH, transform.position + new Vector3(0, -6.2f, 0), transform.rotation);

        pathStonesSpawned = true;
    }

    
}
