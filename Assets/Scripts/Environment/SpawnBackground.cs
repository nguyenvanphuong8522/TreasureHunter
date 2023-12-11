using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBackground : MonoBehaviour
{
    public GameObject roomPrefab;

    private List<GameObject> currentRooms = new List<GameObject>();
    private List<GameObject> roomsToRemove = new List<GameObject>();

    public GameObject player;
    public GameObject floor;
    private float screenWidth;
    private float posSpawnRoom = 0;
    void Start()
    {
        screenWidth = Camera.main.aspect * 2 * Camera.main.orthographicSize;
        InvokeRepeating("GenerateRoomIfRequired", 0, 0.25f);
    }

    private void GenerateRoomIfRequired()
    {
        bool addRooms = true;
        float posRemoveRoomX = player.transform.position.x - screenWidth;
        float posAddRoomX = player.transform.position.x + screenWidth;

        foreach (var room in currentRooms)
        {
            float roomWidth = floor.transform.localScale.x;
            float roomPosX = room.transform.position.x;
            if (roomPosX > posAddRoomX)
                addRooms = false;
            else if (roomPosX + roomWidth < posRemoveRoomX)
                roomsToRemove.Add(room);

            posSpawnRoom = roomPosX + roomWidth;
        }

        RemoveRoom();
        if (addRooms)
            AddRoom(posSpawnRoom);
    }
    void AddRoom(float posRoom)
    {
        GameObject room = Instantiate(roomPrefab);
        room.transform.position = new Vector3(posRoom, 0, 0);
        currentRooms.Add(room);
    }
    public void RemoveRoom()
    {
        foreach (var item in roomsToRemove)
        {
            currentRooms.Remove(item);
            Destroy(item);
        }
    }
}
