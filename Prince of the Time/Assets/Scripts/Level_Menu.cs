using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Level_Menu : MonoBehaviour
{   public Button[] levelButtons;

    public void LevelLoad(int i)
    {
        
        SceneManager.LoadScene(i+1, LoadSceneMode.Single);

    }
    public void Back()
    {
        SceneManager.LoadScene("Main Menu");

    }
}
