using System.Collections.Generic;
using UnityEngine;
public class LootBag : MonoBehaviour
{
    [SerializeField]
    public List<ItemController> LootListGameObj = new List<ItemController>();

    ItemController GetDroppedItem()
    {
        int randomNumber = Random.Range(1,101);
        List<ItemController> possibleItems = new List<ItemController>();

        foreach (ItemController  item in LootListGameObj)
        {
            //nếu số tỉ lệ ra đồ của vật lớn hơn số random ra
            if (randomNumber < item.DropChange)
            {
                //thêm tất cả các vật đó vào possibleItem
                possibleItems.Add(item);
            }
        }

        //random vật phẩm rớt
        if (possibleItems.Count > 0)
        {
            randomNumber = Random.Range(0, possibleItems.Count);
            ItemController itemDrop = possibleItems[randomNumber];
            return itemDrop;
        }

        return null;
    }

    public void InstantiateLoot (Vector3 spawnPosition)
    {
        ItemController dropItem = GetDroppedItem();

        //sẽ có trường hợp dropItem rỗng nên phải check
        if (dropItem != null)
        {
            //Instantiate: hàm tạo ra 1 obj trên màn hình
           Instantiate(dropItem, spawnPosition, Quaternion.identity);
        }
    }
}