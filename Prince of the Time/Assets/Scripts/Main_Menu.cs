using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_Menu : MonoBehaviour
{
    
    public GameObject mainMenuUI;
    
    public void StartGame(int lastOpenedLevel)
    {
        SceneManager.LoadScene(lastOpenedLevel, LoadSceneMode.Single);

    }

    public void LoadLevelsMenu()
    {
        Debug.Log("LoadLevelsMenu");
        
        SceneManager.LoadScene("Level Menu");
    }

    public void Exit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }







}
