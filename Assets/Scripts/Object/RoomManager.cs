using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public Dictionary<string, GameObject> rooms = new Dictionary<string, GameObject>();
    private void Awake()
    {
        this.Register(EventID.LoadData, loadGame);
    }
    private void Start()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Room");
        foreach (GameObject obj in objs)
        {
            rooms.Add(obj.name, obj);
            //obj.SetActive(false);
        }
        this.Broadcast(EventID.LoadData);
        if (string.IsNullOrEmpty(DataManager.Instance.dataPlayerSO.curGT))
        {
            AudioManager.Instance.PlayGT("Start");
        }
        else AudioManager.Instance.PlayGT(DataManager.Instance.dataPlayerSO.curGT);
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
