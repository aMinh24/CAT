using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class DataManager : BaseManager<DataManager>
{
    public DataConfig Config;
    public DataPlayerSO dataPlayerSO;
    public ScriptNPC npc;
    private string dataFilePath;
    private bool firstFrame = true;
    private void Start()
    {
        dataFilePath = Application.persistentDataPath + "/playerdata.json";
        LoadGame();
    }
    //private void Update()
    //{
    //    if (firstFrame)
    //    {
    //        this.Broadcast(EventID.LoadData);
    //        firstFrame = false;
    //    }
    //}
    private void OnApplicationQuit()
    {
        SaveGame();
    }
    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            SaveGame();
        }
    }
    private void LoadGame()
    {
        string content = ReadDataSO(dataFilePath);
        if (content == null)
        {
            WriteDataSO(dataPlayerSO, dataFilePath);
            content = ReadDataSO(dataFilePath);
        }
        JsonUtility.FromJsonOverwrite(content, dataPlayerSO);
    }
    public void SaveGame()
    {
        Debug.Log("SaveGame");
        this.Broadcast(EventID.saveData);
        WriteDataSO(dataPlayerSO, dataFilePath);
    }
    //public void ForceSave
    private void WriteDataSO(object data, string path)
    {
        string contents = JsonUtility.ToJson(data);
        if (!File.Exists(path))
        {
            FileStream f = File.Create(path);
            f.Close();
        }

        File.WriteAllText(path, contents);
    }
    private string ReadDataSO(string path)
    {
        if (File.Exists(path))
        {
            return File.ReadAllText(path);
        }
        return null;
    }
}
