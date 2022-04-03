using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport_Enter : MonoBehaviour
{
    [SerializeField] private Transform hero;
    [SerializeField] private Transform exit;
    private Vector3 offset = new Vector3(1f, 0, 0);
    public bool isTeleporting;
    private void Start()
    {
        hero = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            hero.position = exit.position + offset;
            isTeleporting = true;
        }
        else isTeleporting = false;

    }

}
