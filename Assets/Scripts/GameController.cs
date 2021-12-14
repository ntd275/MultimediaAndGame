using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void UnPauseGame()
    {
        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GoToLevel(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void StartGame()
    {
        PlayerPrefs.SetInt("Health", 3);
        PlayerPrefs.SetInt("Coin", 0);
        SceneManager.LoadScene("Level1");
    }

    public void ContinuePlay()
    {
        SceneManager.LoadScene(PlayerPrefs.GetString("Level"));
    }

    public void SetContiueLevel(string name)
    {
        PlayerPrefs.SetString("Level", name);
    }

    public void SetContiueLevelCurrent()
    {
        PlayerPrefs.SetString("Level", SceneManager.GetActiveScene().name);
    }
}
