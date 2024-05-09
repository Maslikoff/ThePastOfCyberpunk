using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public string menuSceneName = "MenuScene";
    public string gameSceneName = "GameScene";

    public void LoadGameScene()
    {
        SceneManager.LoadScene(gameSceneName);
        Time.timeScale = 1f;
    }

    public void LoadMenuScene()
    {
        SceneManager.LoadScene(menuSceneName);
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
    }

    public void ContinuoeGame()
    {
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
