using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject continueButton;

    void Start()
    {
        if (File.Exists(Application.persistentDataPath + "/saveTest.dat"))
        {
            continueButton.SetActive(true);
        }
        else
        {
            continueButton.SetActive(false);
        }
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }

    public void ReSpawn()
    {
        if (Player != null)
        {
            // Lấy PlayerController từ GameObject Player
            DataPlayer dataPlayer = Player.GetComponent<DataPlayer>();

            if (dataPlayer != null)
            {
                dataPlayer.Respawn(); // Gọi hàm Respawn từ PlayerController
            }
        }
    }

    public void OutToMenu()
    {
        SceneManager.LoadScene("StartMenu");
        Time.timeScale = 1; 
    }

     public void ContinueGame()
    {
        // Tạo một đối tượng SaveManage và gọi hàm Load để tải dữ liệu
        SaveManage saveManage = FindObjectOfType<SaveManage>();
        if (saveManage != null)
        {
            saveManage.Load();

            // Sau khi load dữ liệu, lấy tên map từ DataPlayer và chuyển đến map đó
            string savedMap = DataPlayer.MyInstance.Map;
            if (!string.IsNullOrEmpty(savedMap))
            {
                SceneManager.LoadScene(savedMap); // Chuyển tới map đã lưu
            }
            else
            {
                Debug.LogError("Không có tên map được lưu!");
            }
        }
        else
        {
            Debug.LogError("SaveManage không được tìm thấy!");
        }
    }
}
