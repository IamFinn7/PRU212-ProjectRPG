using System;
using UnityEngine;

[Serializable]
public class SaveData 
{
    public PlayerData MyPlayerData {get; set; }

    public SaveData()
    {

    } 
}

[Serializable]
public class PlayerData
{
    public int myCountDie {get; set; }
    public float myHealth {get; set; }
    public string nameMap {get; set; }
    public PlayerData(int countDie, float health, string nameMap)
    {
        this.myCountDie = countDie;
        this.myHealth = health;
        this.nameMap = nameMap;
    }

}