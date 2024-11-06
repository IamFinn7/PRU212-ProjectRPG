using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveManage: MonoBehaviour
{
    void Start()
    {
        Debug.Log(Application.persistentDataPath);
    }

    // void Update()
    // {
    //     if(Input.GetKeyDown(KeyCode.G))
    //     {
    //         Save();
    //     }
    //     if(Input.GetKeyDown(KeyCode.H))
    //     {
    //         Load();
    //     }
    // }
    public void Save()
    {
        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + "saveTest.dat", FileMode.OpenOrCreate);
            
            SaveData data = new SaveData();

            SavePlayer(data);

            bf.Serialize(file, data);
            file.Close();
            Debug.Log("Game saved successfully.");
        }
        catch (Exception ex)
        {
            Debug.LogError("Error saving game: " + ex.Message);
            throw;
        }   
    }

    private void SavePlayer(SaveData data)
    {
        data.MyPlayerData = new PlayerData(DataPlayer.MyInstance.CountDie,
                                            DataPlayer.MyInstance.CountHealth,
                                            DataPlayer.MyInstance.Map);
        Debug.Log(data.MyPlayerData);
    }

    public void Load()
    {
        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + "saveTest.dat", FileMode.Open);
            
            SaveData data = (SaveData)bf.Deserialize(file);

            file.Close();

            LoadPlayer(data);
            Debug.Log("Game loaded successfully.");
        }
        catch (Exception ex)
        {
            Debug.LogError("Error loading game: " + ex.Message);
            throw;
        }
    }

    private void LoadPlayer(SaveData data)
    {
        DataPlayer.MyInstance.CountDie = data.MyPlayerData.myCountDie;
        DataPlayer.MyInstance.CountHealth = data.MyPlayerData.myHealth;
        DataPlayer.MyInstance.Map = data.MyPlayerData.nameMap;

        // DataPlayer.MyInstance.PlayerX = data.MyPlayerData.myX;
        // DataPlayer.MyInstance.PlayerY = data.MyPlayerData.myY;

        DataPlayer.MyInstance.UpdateCountDie();
        DataPlayer.MyInstance.UpdateCountHealth();
        // DataPlayer.MyInstance.UpdatePlayerXY();
    }
}