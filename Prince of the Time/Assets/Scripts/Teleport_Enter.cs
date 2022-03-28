using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport_Enter : MonoBehaviour
{
    [SerializeField] private GameObject hero;
    [SerializeField] private Transform exit;
    public bool isTeleporting;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            hero.transform.position = exit.position;
            isTeleporting = true;
        }
        else isTeleporting = false;

    }

}
