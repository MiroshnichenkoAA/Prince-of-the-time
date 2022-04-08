using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vertical_Platform : MonoBehaviour
{
    private PlatformEffector2D _effector;
    public float waitTime;
    public float waitTimeCounter;
    void Start()
    {
        _effector = GetComponent<PlatformEffector2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.S))
        {
            waitTimeCounter = waitTime;
            _effector.rotationalOffset = 0;
        }
        if (Input.GetKey(KeyCode.S))
        {
            if (waitTimeCounter <= 0)
            {
                _effector.rotationalOffset = 180f;
                waitTimeCounter = waitTime;
            }
            else
            {
                waitTimeCounter -= Time.deltaTime;
            }
        }
       
    }
}
