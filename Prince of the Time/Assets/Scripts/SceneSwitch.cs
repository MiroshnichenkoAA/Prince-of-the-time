using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    public static string prevScene;
    public static string currenScene;
  public virtual void Start()
    {
        currenScene = SceneManager.GetActiveScene().name;
    }

    public void SwitchScene(string sceneName)
    {
        prevScene = currenScene;
        SceneManager.LoadScene(sceneName);
    }
}
