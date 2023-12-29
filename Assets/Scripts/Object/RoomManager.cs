using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public Dictionary<string, GameObject> rooms = new Dictionary<string, GameObject>();
    private void Start()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Room");
        foreach (GameObject obj in objs)
        {
            rooms.Add(obj.name, obj);
            //obj.SetActive(false);
        }
        this.Register(EventID.LoadData, loadGame);
    }
    public void loadGame(object? data)
    {
        string curRoom = DataManager.Instance.dataPlayerSO.curRoom;
        if (curRoom == null || curRoom.Equals(string.Empty))
        {
            curRoom = "FirstRoom";
        }
        foreach(GameObject obj in rooms.Values)
        {
            if (!obj.name.Equals(curRoom))
            {
                obj.SetActive(false);
            }
        }
    }
}
