using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TelePortHandl : MonoBehaviour
{
    // [SerializeField] private int SceneId;
    [SerializeField] private Sprite itemNeeded;
    [SerializeField] private InventoryBGController inventory;
    private SaveManage saveManage;
    void Awake()
    {
        saveManage = FindObjectOfType<SaveManage>();
        if (saveManage == null)
        {
            Debug.LogError("SaveManage is not found in the scene!");
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            if (inventory.CheckItemInInventory(itemNeeded))
            {
                // Save data before teleporting
                if (saveManage != null)
                {
                    Debug.Log("a");
                    saveManage.Save();
                }

                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
            }
            else
            {
                print("Need key");
            }

        }
    }
}
