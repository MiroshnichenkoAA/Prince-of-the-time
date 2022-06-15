using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die : MonoBehaviour
{
    [SerializeField] Hero hero;
    

    public void DieTrigger()
    {
        hero.Die();
    }
}
