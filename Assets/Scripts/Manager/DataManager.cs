using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class DataManager : BaseManager<DataManager>
{
    public DataConfig Config;
    public DataPlayerSO dataPlayerSO;
    private string dataFilePath;
    private void Start()
    {
        dataFilePath = Application.persistentDataPath + "/playerdata.json";
        LoadGame();
        this.Broadcast(EventID.LoadData);
        this.Register(EventID.debug,DebugGame);
    }
    public void DebugGame(object? data)
    {
        Debug.Log("dataPath:  " + dataFilePath);
        Debug.Log(JsonUtility.ToJson(dataPlayerSO));
    }
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
        string content =  ReadDataSO(dataFilePath);
        if (content == null)
        {
            WriteDataSO(dataPlayerSO, dataFilePath);
            content = ReadDataSO(dataFilePath);
        }
        JsonUtility.FromJsonOverwrite(content, dataPlayerSO);
    }
    public void SaveGame()
    {
        this.Broadcast(EventID.saveData);
        WriteDataSO(dataPlayerSO, dataFilePath);
    }
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
