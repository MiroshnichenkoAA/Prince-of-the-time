using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport_Enter : MonoBehaviour
{
    [SerializeField] private Transform hero;
    [SerializeField] private Transform exit;
    [SerializeField] private double teleportCD;
    [SerializeField] private double teleportCDCounter;
    private Vector3 offset = new Vector3(1f, 0, 0);
    
    public bool isRecentlyTeleported=false;
    private void Start()
    {
        hero = GameObject.FindGameObjectWithTag("Player").transform;
        teleportCDCounter = teleportCD;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.tag == "Player")&&isRecentlyTeleported==false )
        {
            isRecentlyTeleported = true;
            hero.position = exit.position + offset;
            
        }
       

    }
    private void Update()
    {
        if ((teleportCDCounter > 0) && isRecentlyTeleported == true)
        {
            teleportCDCounter -= Time.deltaTime;
        }
        else { 
            isRecentlyTeleported = false;
            teleportCDCounter = teleportCD;

        }

    }

}
