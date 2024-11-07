using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveManage : MonoBehaviour
{
    void Start()
    {
        Debug.Log(Application.persistentDataPath); // Kiểm tra đường dẫn lưu file
    }

    public void Save()
    {
        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            string filePath = Application.persistentDataPath + "/saveTest.dat";
            
            using (FileStream file = File.Open(filePath, FileMode.OpenOrCreate))
            {
                SaveData data = new SaveData();
                SavePlayer(data);

                bf.Serialize(file, data);
                Debug.Log("Game saved successfully.");
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Error saving game: " + ex.Message);
        }
    }

    private void SavePlayer(SaveData data)
    {
        data.MyPlayerData = new PlayerData(
            DataPlayer.MyInstance.CountDie,
            DataPlayer.MyInstance.CountHealth,
            DataPlayer.MyInstance.Map
        );
        Debug.Log("Player data saved: " + data.MyPlayerData);
    }

    public void Load()
    {
        try
        {
            string filePath = Application.persistentDataPath + "/saveTest.dat";
            if (File.Exists(filePath))
            {
                BinaryFormatter bf = new BinaryFormatter();
                using (FileStream file = File.Open(filePath, FileMode.Open))
                {
                    SaveData data = (SaveData)bf.Deserialize(file);
                    LoadPlayer(data);
                    Debug.Log("Game loaded successfully.");
                }
            }
            else
            {
                Debug.LogError("Save file not found!");
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Error loading game: " + ex.Message);
        }
    }

    private void LoadPlayer(SaveData data)
    {
        DataPlayer.MyInstance.CountDie = data.MyPlayerData.myCountDie;
        DataPlayer.MyInstance.CountHealth = data.MyPlayerData.myHealth;
        DataPlayer.MyInstance.Map = data.MyPlayerData.nameMap;

        DataPlayer.MyInstance.UpdateCountDie();
        DataPlayer.MyInstance.UpdateCountHealth();
    }
}
