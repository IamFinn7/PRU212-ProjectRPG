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
    [SerializeField] private GameObject deathPanel;
    public bool isDead = false;
    private DamageAble damageAble;
    private SaveManage saveManage;
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

    void Awake()
    {
        if (MyInstance == null)
        {
            MyInstance = this;
        }
        
        damageAble = GetComponent<DamageAble>();
        saveManage = FindObjectOfType<SaveManage>();

        if (saveManage == null)
        {
            Debug.LogError("SaveManage is not found in the scene!");
        }

        SceneManager.sceneLoaded += OnSceneLoaded;  // Đăng ký sự kiện sceneLoaded để tự động load data
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Kiểm tra nếu màn hiện tại không phải là "Level 1"
        if (scene.name != "Level 1" && saveManage != null)
        {
            saveManage.Load();  // Chỉ tải dữ liệu khi không vào "Level 1"
            UpdateCountDie();
            UpdateCountHealth();
            Debug.Log("Data loaded for scene: " + scene.name);
        }
        else
        {
            Debug.Log("Skipped loading data for Level 1.");
        }
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;  // Hủy đăng ký sự kiện để tránh lỗi
    }

    public void Die()
    {
        CountDie++;
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
    }

    // Gọi hàm này trước khi chuyển màn mới để lưu tên map hiện tại
    public void SaveCurrentMap()
    {
        Map = SceneManager.GetActiveScene().name;
        if (saveManage != null)
        {
            saveManage.Save();
        }
    }
}
