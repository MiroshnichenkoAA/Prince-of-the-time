using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Level_Menu : MonoBehaviour
{   public Button[] levelButtons;

    public void LevelLoad()
    { 
        SceneManager.LoadScene(2, LoadSceneMode.Single);

    }
    public void Back()
    {
        SceneManager.LoadScene("Main Menu");

    }
}
