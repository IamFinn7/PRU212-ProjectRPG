using UnityEngine;

public class KeyboardController : MonoBehaviour
{
    [SerializeField] private InventoryBGController inventoryBG;
    [SerializeField] private PauseController pauseGame;
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

        if (Input.GetKeyDown(KeyCode.Escape))
        {
             if (pauseGame.isActive)
            {
                pauseGame.Hide();
                Time.timeScale = 1f;
            }
            else
            {
                Time.timeScale = 0f;
                pauseGame.Show();
                inventoryBG.Hide();
            }
        }
    }

}
