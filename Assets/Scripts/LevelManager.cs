using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    

    public void LoadGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void LoadEndMenu()
    {
        SceneManager.LoadScene("End Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("quit");
    }

}
