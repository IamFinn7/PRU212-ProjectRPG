using System.Collections;
using TMPro;
using UnityEngine;

public class KeyboardController : MonoBehaviour
{
    [SerializeField] private InventoryBGController inventoryBG;
    [SerializeField] private PauseController pauseGame;
    [SerializeField] private TMP_Text StatusTxt;
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

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (inventoryBG != null)
            {
                int temp = inventoryBG.FindItemByName("BottleBlood");
                if (temp >= 0)
                {
                    bool sus = Heal();
                    if (!sus)
                    {
                        StartCoroutine(ShowTemporaryMessage("CANT HEAL", 2f));
                    }
                    else
                    {
                        inventoryBG.ReduceQuantityOfItem(temp);
                    }
                }
                else
                {
                    StartCoroutine(ShowTemporaryMessage("ITEM NOT FOUND", 2f));
                }
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Q))
        {
            PlayerController player = GetComponent<PlayerController>();
            player.ChangeWeapon();
        }
    }

    private IEnumerator ShowTemporaryMessage(string message, float duration)
    {
        StatusTxt.text = message;
        yield return new WaitForSeconds(duration);
        StatusTxt.text = ""; // Clear the message after the duration
    }

    // Heal 
    public bool Heal()
    {
        IDamageAble player_damage_able = GetComponent<IDamageAble>();
        if (player_damage_able.Health < player_damage_able.MaxHealth)
        {
            player_damage_able.AddHealth(100);
            return true;
        }
        else
        {
            return false;
        }

    }
}
