using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public Transform firepoint;
    private Animator anim;
    public GameObject bullet;
    public int damageTurret;
    float timebetween;
    public float starttimebetween;
    bool isOnTurretArea=false;
    
    
    private AudioSource audioSource;
    
    private StatesT StateT
    {
        get { return (StatesT)anim.GetInteger("state"); }
        set { anim.SetInteger("state", (int)value); }
    } 

    void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        timebetween = starttimebetween;

        
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Main Character"))
        {
             StateT = StatesT.shooting; 
            
            isOnTurretArea = true;
        }
        else
        {
            isOnTurretArea = false;
            StateT = StatesT.idle;
        }

    }
    // Update is called once per frame
    void Update()
    {
         

        if (isOnTurretArea == true)
        {
            if (timebetween <= 0)
            {
                audioSource.Play();
                Instantiate(bullet, firepoint.position, firepoint.rotation);
                timebetween = starttimebetween;
            }
            else
            {
                timebetween -= Time.deltaTime;
            }
        }
        

    }

    

}
public enum StatesT
{
    idle,
    shooting
  
}
