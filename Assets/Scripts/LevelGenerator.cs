using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelGenerator : MonoBehaviour
{
    public GameObject roomBase;  // object that is used to create layout for room to be placed on
    public GameObject prisoner;

    public int roomsToSpawn;

    public Color startColor, endColor;

    public Transform generationPoint;

    public enum Direction { up, right, down, left};
    public Direction selectedDirection;

    public float xOffset = 32f, yOffset = 16f;

    public LayerMask whatIsRoomBase;

    private GameObject lastRoomBase;   

    private List<GameObject> roomsBaseObjects = new List<GameObject>();   //list of room bases
    private List<GameObject> generatedRooms = new List<GameObject>();  // list of actual rooms


    public RoomPrefabs rooms;

    private GameObject newRoom;
    public GameObject endRoom;
    private GameObject startRoom;

    public GameObject hellAltar, heavenAltar;

    private int prisonerRoomNumber;
    private int altarsRoomNumber;



    void Start()
    {
        Instantiate(roomBase, generationPoint.position, generationPoint.rotation).GetComponent<SpriteRenderer>().color = startColor;   

        selectedDirection = (Direction)Random.Range(0, 4);
        prisonerRoomNumber = Random.Range(2, roomsToSpawn - 1);
        altarsRoomNumber = Random.Range(2, roomsToSpawn - 1);
        
        while(altarsRoomNumber == prisonerRoomNumber)
        {
            altarsRoomNumber = Random.Range(2, 5);
        }


        MoveGenerationPoint();

        for (int i =0; i < roomsToSpawn; i++)   //GENERATING ROOMS on room bases
        {
            GameObject newRoomBase = Instantiate(roomBase, generationPoint.position, generationPoint.rotation);  //creates roomBase object
            roomsBaseObjects.Add(newRoomBase);   // adds new created room to list  

            if(i +1 == roomsToSpawn)  // check if we are creating last room /  NOTING WHAT ROOM IS LAST
            {
                newRoomBase.GetComponent<SpriteRenderer>().color = endColor;

                roomsBaseObjects.RemoveAt(roomsBaseObjects.Count - 1);   // remove endroom from list

                lastRoomBase = newRoomBase;

            }

            if(i + prisonerRoomNumber == roomsToSpawn)   // GENERATING WANDERING SOUL
            {
                Instantiate(prisoner, generationPoint.position, generationPoint.rotation);
            }

            if(i + altarsRoomNumber == roomsToSpawn)
            {
                Vector3 hellAltarPos = new Vector3(generationPoint.position.x + 9, generationPoint.position.y + 4, generationPoint.position.z);
                Vector3 heavenAltarPos = new Vector3(generationPoint.position.x - 9, generationPoint.position.y - 4, generationPoint.position.z);

                Instantiate(hellAltar, hellAltarPos, generationPoint.rotation);
                Instantiate(heavenAltar, heavenAltarPos, generationPoint.rotation);

            }

            selectedDirection = (Direction)Random.Range(0, 4);

            MoveGenerationPoint();

            while (Physics2D.OverlapCircle(generationPoint.position, 0.22f, whatIsRoomBase ))
            {
                MoveGenerationPoint();
            }


        }

        CreateRooms(Vector3.zero);  // create first room at 0,0

        if(newRoom.transform.position == Vector3.zero)  // check if some created room has vector3.zero
        {
            startRoom = newRoom;   // mark that room as firstroom

            startRoom.GetComponent<Room>().firstRoom = true;  // disable activate enemies for first room
            startRoom.GetComponent<Room>().closeWhenEntered = false;
            startRoom.GetComponent<Room>().openWhenEnemiesCleared = false;

        }
        
        foreach(GameObject room in roomsBaseObjects)   // for each created roomBase, instantiate room on top of it
        {
            CreateRooms(room.transform.position);
        }

        CreateRooms(lastRoomBase.transform.position);  //create room on top of lastRoomBase

        if (generatedRooms.Count == (roomsToSpawn + 1))  // if generation is complete
        {
            generatedRooms.RemoveAt((roomsToSpawn));  // remove last room form the list 
            endRoom = newRoom;  // make removed room from the list thelastroom 
            endRoom.GetComponent<Room>().lastRoom = true;   // set lastlevel bool in this room to true
            endRoom.GetComponent<Room>().roomHider.SetActive(false);  // disable roomhider so last room can be seen on minimap from getgo
        }
    }

    
    void Update()
    {
        
        if(Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }


    }

    public void MoveGenerationPoint()
    {
        switch(selectedDirection)
        {
            case Direction.up:
                generationPoint.position += new Vector3(0, yOffset, 0);
                break;
            case Direction.down:
                generationPoint.position += new Vector3(0, -yOffset, 0);
                break;
            case Direction.right: 
                generationPoint.position += new Vector3(xOffset, 0, 0);
                break;
            case Direction.left:
                generationPoint.position += new Vector3(-xOffset, 0, 0);
                break;

        }
    }


    public void CreateRooms(Vector3 roomPosition)
    {
        bool roomAbove = Physics2D.OverlapCircle(roomPosition + new Vector3(0f, yOffset, 0f), 0.25f, whatIsRoomBase);
        bool roomBelow = Physics2D.OverlapCircle(roomPosition + new Vector3(0f, -yOffset, 0f), 0.25f, whatIsRoomBase);
        bool roomRight = Physics2D.OverlapCircle(roomPosition + new Vector3(xOffset, 0f, 0f), 0.25f, whatIsRoomBase);
        bool roomLeft = Physics2D.OverlapCircle(roomPosition + new Vector3(-xOffset, 0f, 0f), 0.25f, whatIsRoomBase);

        int directionCount = 0;
        if(roomAbove)
        {
            directionCount++;
        }
        if (roomBelow)
        {
            directionCount++;
        }
        if (roomRight)
        {
            directionCount++;
        }
        if (roomLeft)
        {
            directionCount++;
        }

        switch(directionCount)
        {
            case 0:
                break;

            case 1:
                if(roomAbove)
                {
                    newRoom = Instantiate(rooms.oneU, roomPosition, transform.rotation);  // names newly created room newRoom
                    generatedRooms.Add(newRoom);         // adds created room to list of generetedrooms
                }
                if (roomBelow)
                {
                    newRoom = Instantiate(rooms.oneD, roomPosition, transform.rotation);
                    generatedRooms.Add(newRoom);
                }
                if (roomRight)
                {
                    newRoom = Instantiate(rooms.oneR, roomPosition, transform.rotation);
                    generatedRooms.Add(newRoom);
                }
                if (roomLeft)
                {
                    newRoom = Instantiate(rooms.oneL, roomPosition, transform.rotation);
                    generatedRooms.Add(newRoom);
                }

                break;

            case 2:
                if(roomAbove && roomRight)
                {
                    newRoom = Instantiate(rooms.twoUR, roomPosition, transform.rotation);
                    generatedRooms.Add(newRoom);
                }
                if (roomAbove && roomLeft)
                {
                    newRoom = Instantiate(rooms.twoUL, roomPosition, transform.rotation);
                    generatedRooms.Add(newRoom);
                }
                if (roomAbove && roomBelow)
                {
                    newRoom = Instantiate(rooms.twoUD, roomPosition, transform.rotation);
                    generatedRooms.Add(newRoom);
                }
                if (roomBelow && roomRight)
                {
                    newRoom = Instantiate(rooms.twoDR, roomPosition, transform.rotation);
                    generatedRooms.Add(newRoom);
                }
                if (roomBelow && roomLeft)
                {
                    newRoom = Instantiate(rooms.twoDL, roomPosition, transform.rotation);
                    generatedRooms.Add(newRoom);
                }
                if (roomLeft && roomRight)
                {
                    newRoom = Instantiate(rooms.twoRL, roomPosition, transform.rotation);
                    generatedRooms.Add(newRoom);
                }

                break;

            case 3:
                if(roomAbove && roomRight && roomBelow)
                {
                    newRoom = Instantiate(rooms.threeURD, roomPosition, transform.rotation);
                    generatedRooms.Add(newRoom);
                }
                if (roomAbove && roomRight && roomLeft)
                {
                    newRoom = Instantiate(rooms.threeURL, roomPosition, transform.rotation);
                    generatedRooms.Add(newRoom);
                }
                if (roomAbove && roomLeft && roomBelow)
                {
                    newRoom = Instantiate(rooms.threeUDL, roomPosition, transform.rotation);
                    generatedRooms.Add(newRoom);
                }
                if (roomLeft && roomRight && roomBelow)
                {
                    newRoom = Instantiate(rooms.threeDRL, roomPosition, transform.rotation);
                    generatedRooms.Add(newRoom);
                }


                break;

            case 4:
                if(roomAbove && roomRight && roomBelow && roomLeft)
                {
                    newRoom = Instantiate(rooms.fourURDL, roomPosition, transform.rotation);
                    generatedRooms.Add(newRoom);
                }
                
                break;


                

        }

        

    }

}


[System.Serializable]
public class RoomPrefabs
{
    public GameObject oneU, oneR, oneD, oneL,
                      twoUR, twoUD, twoUL, twoDR, twoDL, twoRL,
                      threeURD, threeURL, threeUDL, threeDRL,
                      fourURDL;
}
