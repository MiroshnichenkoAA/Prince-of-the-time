using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Hero_Data
{
 

    public int health;
    public float[] position;

    public Hero_Data(Hero hero)
    {
        health = hero.currentHealth;
        position = new float[3];
        position[0] = hero.transform.position.x;
        position[1] = hero.transform.position.y;
        position[2] = hero.transform.position.z;
    }
}
