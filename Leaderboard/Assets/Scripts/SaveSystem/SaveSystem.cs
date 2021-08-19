using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveSystem : MonoBehaviour
{
    public static SaveSystem instance;

    [SerializeField]
    private SavaData saveData;

    void Start()
    {
        if (instance == null)
            instance = this;
        saveData = new SavaData();
    }
    
    public void LoadData()
    {
        if (PlayerPrefs.HasKey("SaveData"))
        {
            string json = PlayerPrefs.GetString("SaveData");
            saveData = JsonUtility.FromJson<SavaData>(json);
            Leaderboard.Instance.SetLeaderboardData(saveData.keys, saveData.values);
        }
    }

    public void SaveData()
    {
        saveData.keys.Clear();
        saveData.values.Clear();

        List<KeyValuePair<string, int>> tmpList = new List<KeyValuePair<string, int>>();
        tmpList = Leaderboard.Instance.GetLeaderboardList();

        for (int i = 0; i< tmpList.Count; i++)
        {
            saveData.keys.Add(tmpList[i].Key);
            saveData.values.Add (tmpList[i].Value);
        }

        var json =  JsonUtility.ToJson(saveData);

        PlayerPrefs.SetString("SaveData", json);
        PlayerPrefs.Save();        
    }
}

/*
public interface ISaveSystem
{
    void LoadGame();
    void SaveGame();
}
*/