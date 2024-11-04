using UnityEngine;

public class KeyboardController : MonoBehaviour
{
    [SerializeField] private InventoryBGController inventoryBG;
    void Start()
    {
        inventoryBG.StartInventory();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (inventoryBG.isActive)
            {
                inventoryBG.Hide();
                Time.timeScale = 1f;
            }
            else
            {
                Time.timeScale = 0f;
                inventoryBG.Show();
            }
        }
    }
}
