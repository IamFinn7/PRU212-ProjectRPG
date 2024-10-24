using System.Collections.Generic;
using UnityEngine;
public class LootBag : MonoBehaviour
{
    [SerializeField]
    public List<Loot> LootLists = new List<Loot>();
    [SerializeField]
    public GameObject droppedItemPrefab;

    Loot GetDroppedItem()
    {
        int randomNumber = Random.Range(1,101);
        List<Loot> possibleItems = new List<Loot>();

        foreach (Loot item in LootLists)
        {
            if (randomNumber < item.dropChange)
            {
                possibleItems.Add(item);
            }
        }

        //random vật phẩm rớt
        if (possibleItems.Count > 0)
        {
            randomNumber = Random.Range(0, possibleItems.Count);
            Loot itemDrop = possibleItems[randomNumber];
            return itemDrop;
        }

        return null;
    }

    public void InstantiateLoot (Vector3 spawnPosition)
    {
        Loot dropItem = GetDroppedItem();

        if (dropItem != null)
        {
            //Instantiate: hàm tạo ra 1 obj trên màn hình
            GameObject lootGameObject = Instantiate(droppedItemPrefab, spawnPosition, Quaternion.identity);
            lootGameObject.GetComponent<SpriteRenderer>().sprite = dropItem.lootSprite;
        }
    }
}