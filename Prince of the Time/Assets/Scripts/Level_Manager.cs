using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level_Manager : MonoBehaviour
{
    public int iLevelToLoad;

    public bool isAvailibleToLoad =false;


   
    void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collisionGameObject = collision.gameObject;
        if(collisionGameObject.tag == "Player")
        {
            LoadScene();
        }
        
    }

  void LoadScene()
    {
        if (isAvailibleToLoad)
        {
            SceneManager.LoadScene(iLevelToLoad);
        }
    }

}
