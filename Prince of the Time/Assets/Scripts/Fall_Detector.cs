using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fall_Detector : MonoBehaviour
{
    [SerializeField] private Shadow_Manager shadowManager;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
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
