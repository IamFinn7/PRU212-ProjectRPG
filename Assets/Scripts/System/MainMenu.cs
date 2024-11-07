using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject continueButton;

    public void PlayGame()
    {
        // Xóa file lưu trước khi bắt đầu trò chơi mới
        string saveFilePath = Application.persistentDataPath + "/saveTest.dat";
        if (File.Exists(saveFilePath))
        {
            File.Delete(saveFilePath);
            Debug.Log("Dữ liệu đã bị xóa để bắt đầu trò chơi mới.");
        }

        // Chuyển đến scene tiếp theo (màn đầu tiên của trò chơi)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Time.timeScale = 1f;
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
