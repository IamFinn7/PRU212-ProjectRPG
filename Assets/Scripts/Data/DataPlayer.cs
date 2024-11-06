using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataPlayer : MonoBehaviour
{
    public static DataPlayer MyInstance;
    [SerializeField]
    private int countDie = 0;
    [SerializeField]
    private float countHealth = 0f;
    [SerializeField] private TMP_Text countDieText;
    [SerializeField] private TMP_Text countHealthText;
    [SerializeField] private GameObject deathPanel;
    public bool isDead = false;
    private DamageAble damageAble;
    private SaveManage saveManage;
    // private GameObject Player;
    // private float playerX;
    // private float playerY;
    private string nameMap;
    public string Map {
        get { return nameMap; } 
        set { nameMap = value; }
    }
    public int CountDie { 
        get { return countDie; } 
        set { countDie = value; }
    }

    public float CountHealth { 
        get { return countHealth; } 
        set { countHealth = value; }
    }
    // public float PlayerX { 
    //     get { return playerX; }
    //     set { playerX = value; }
    // }

    // public float PlayerY { 
    //     get { return playerY; }
    //     set { playerY = value; }
    // }
    void Awake()
    {
        if (MyInstance == null)
        {
            MyInstance = this;
        }
        damageAble = GetComponent<DamageAble>();
        // Player = GameObject.FindWithTag("Player"); 
        Map = SceneManager.GetActiveScene().name;

        saveManage = FindObjectOfType<SaveManage>();
        if (saveManage == null)
        {
            Debug.LogError("SaveManage is not found in the scene!");
        }
    }

    //  void Update()
    // {
    //     if (Player != null)
    //     {
    //         PlayerX = Player.transform.position.x;
    //         PlayerY = Player.transform.position.y;
    //     }
    // }

    public void Die()
    {
        CountDie ++;
        ShowDeathPanel();
        Time.timeScale = 0;


        if (saveManage != null)
        {
            if (damageAble != null)
            {
                damageAble.Health = damageAble.MaxHealth;
            }
            saveManage.Save();
        }
        isDead = true;
    }

    private void ShowDeathPanel()
    {
        deathPanel.SetActive(true); 
    }

    public void Respawn()
    {
       
        Time.timeScale = 1; 
        deathPanel.SetActive(false);

         if (saveManage != null)
        {
            if (damageAble != null)
            {
                damageAble.SetIsAlive(true);
            }
           saveManage.Load();
        }

        isDead = false;
    }

    public void UpdateCountDie()
    {
        countDieText.text = "You died: " + CountDie.ToString();
    }

    public void UpdateCountHealth()
    {
        countHealth = damageAble.Health;  
        countHealthText.text = countHealth.ToString();  
    }

    // public void UpdatePlayerXY()
    // {
    //     if (Player != null)
    //     {
    //         Player.transform.position = new Vector3(playerX, playerY, Player.transform.position.z);
    //         Debug.Log("Player Position Updated: X = " + playerX + ", Y = " + playerY);
    //     }
    // }

    public void Continue()
    {
        if (saveManage != null)
        {
            // Kiểm tra file saveTest.dat có tồn tại trước khi tải
            string saveFilePath = Application.persistentDataPath + "/saveTest.dat";
            if (File.Exists(saveFilePath))
            {
                saveManage.Load();

                string savedMap = Map;
                if (!string.IsNullOrEmpty(savedMap))
                {
                    SceneManager.LoadScene(savedMap); // Tải scene dựa trên tên map đã lưu
                }
                else
                {
                    Debug.LogError("Không có tên map được lưu!");
                }
            }
            else
            {
                Debug.LogError("Không có file save để tiếp tục trò chơi!");
            }
        }
        else
        {
            Debug.LogError("SaveManage không được tìm thấy!");
        }
    }

}
