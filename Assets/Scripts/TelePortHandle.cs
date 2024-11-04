using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TelePortHandl : MonoBehaviour
{
    [SerializeField] private int SceneId;
    [SerializeField] private Sprite itemNeeded;
    [SerializeField] private InventoryBGController inventory;
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            if (inventory.CheckItemInInventory(itemNeeded))
            {
                SceneManager.LoadScene(SceneId, LoadSceneMode.Single);
            }
            else
            {
                print("Need key");
            }

        }
    }
}
