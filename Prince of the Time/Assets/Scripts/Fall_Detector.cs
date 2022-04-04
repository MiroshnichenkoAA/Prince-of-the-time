using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fall_Detector : MonoBehaviour
{
    [SerializeField] private Shadow_Manager shadowManager;
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Shadow")
        {
            shadowManager.hero_script.enabled = true;
            shadowManager.shadowExist = false;
            shadowManager.heroRB.bodyType = RigidbodyType2D.Dynamic;
            shadowManager.heroCollider.enabled = true;
            Destroy(shadowManager.shadowClone);
        }
        
    }
}
