using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Robo : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;
    

   private Rigidbody2D rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
       
            rb.velocity = -transform.right * bulletSpeed;
       
        Destroy(gameObject, 4f);
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Main Character"))
        {
            Destroy(gameObject);
        }
    }
}
