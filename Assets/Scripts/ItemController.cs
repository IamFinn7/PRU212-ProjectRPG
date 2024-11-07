using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    [SerializeField] public Sprite ImageItem;
    [SerializeField] public string ItemName;
    [SerializeField] public int DropChange;
    [SerializeField] public int DropValue = 1;
    [SerializeField] public bool RandomDropValue = false;

    void Start()
    {
        if (RandomDropValue)
        {
            DropValue = Random.Range(5, 10);
        }
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            col.GetComponent<PlayerController>().AddItem(gameObject.GetComponent<ItemController>());
        }

    }
}
