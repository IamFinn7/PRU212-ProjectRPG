using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    public bool isActive = false;
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

    public void ResumeGame()
    {
        gameObject.SetActive(false);
        isActive = false;
        Time.timeScale = 1f;
    }
    
    public void QuitGame()
    {
        SceneManager.LoadScene("StartMenu");
    }
}
