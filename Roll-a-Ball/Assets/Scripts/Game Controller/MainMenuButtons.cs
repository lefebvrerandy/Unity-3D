using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    public void PlayGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Level 1");
    }

    public void Resume()
    {
        SceneManager.UnloadSceneAsync("Pause Menu");
        Time.timeScale = 1;
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void LevelList()
    {
        SceneManager.LoadScene("Level List");
    }

    public void LoadLevel()
    {
        string button = EventSystem.current.currentSelectedGameObject.name;
        if (button == "Level 1")
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("level 1");
        }
        else if (button == "Level 2")
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("level 2");
        }
        else if (button == "Level 3")
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("level 3");
        }
    }

    public void HomeButton()
    {
        SceneManager.LoadScene(0);
    }
}
