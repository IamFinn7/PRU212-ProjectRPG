using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryBGController : MonoBehaviour
{
    private int inventorySize = 9;
    [SerializeField] private ItemBoxController itemBoxPrefab;
    [SerializeField] private RectTransform inventoryBackGround;
    List<ItemBoxController> listsItems = new List<ItemBoxController>();
    public bool isActive = false;
    public void StartInventory()
    {
        for(int i = 0; i < inventorySize; i++)
        {
            //Tạo ra các ô item trong kho
            ItemBoxController temp = Instantiate(itemBoxPrefab, Vector2.zero, Quaternion.identity);

            temp.transform.SetParent(inventoryBackGround);
            temp.transform.localScale = Vector3.one;

            listsItems.Add(temp);
        }
    }

    public void AddItem (ItemController item)
    {
        bool isAdded = false;

        string name = item.ItemName;
        foreach (var uiItem in listsItems)
        {
            if(uiItem._itemName == name)
            {
                uiItem.AddQuantity(1);
                isAdded = true;
                break;
            }
        }

        if(!isAdded)
        {
            int position = GetFirstEmptySlot();
            listsItems[position].SetData(item.ImageItem, 1, item.ItemName);
        }

        Destroy(item.gameObject);
    }

    public int GetFirstEmptySlot()
    {
        for(int i = 0; i <= listsItems.Count; i++)
        {
            if(listsItems[i].isEmpty)
            {
                return i;
            }
        }
        return -1;
    }

    public void Show()
    {
        gameObject.SetActive(true);
        isActive = true;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        isActive = false;
    }

    public bool CheckItemInInventory(Sprite sprite)
    {
        foreach (var uiItem in listsItems)
        {
            if(uiItem.ItemImage.sprite == sprite)
            {
                return true;
            }
        }
        return false;
    }

}
