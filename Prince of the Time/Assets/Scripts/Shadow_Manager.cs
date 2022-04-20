using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow_Manager : MonoBehaviour
{
    [SerializeField] public Hero hero_script;

    

    [SerializeField] public Transform hero;
   
    [SerializeField] public Transform shadow;
    [SerializeField] public GameObject shadow_prefab;
    
    [SerializeField] public GameObject shadowClone;
    [SerializeField] public GameObject shadowSlider;
    [SerializeField] private GameObject mainHero;
    [SerializeField] private float shadowLength;
    [SerializeField] private float shadowCounter;
    [SerializeField] private float shadowCDLength;
    [SerializeField] private float shadowCDCounter;
    [SerializeField] public Shadow_Timer shadowTimer;
    [SerializeField] public CD_Shadow shadowCDTimer;
   



    public bool shadowExist=false;
    public bool shadowCancel = false;
    [SerializeField] public Rigidbody2D heroRB;
    [SerializeField] public RigidbodyConstraints rigidbodyConstraints;
    [SerializeField] public BoxCollider2D heroCollider;
   

    void Start()
    {
        shadowCDCounter = shadowCDLength;
        shadowTimer.SetMaxShadowTime(shadowLength);
        shadowCDTimer.SetMaxShadowCDTime(shadowCDLength);

    }

    
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.F) && shadowExist == true)
        {
            hero_script.enabled = true;
            heroRB.bodyType = RigidbodyType2D.Dynamic;

        
            mainHero.transform.position = shadowClone.transform.position;
            shadowExist = false;
            shadowCounter = shadowLength;
            
            Destroy(shadowClone);
            shadowCancel = true;

        }else shadowCancel = false; 
       
        if (Input.GetKeyDown(KeyCode.F)&&shadowExist==false&&shadowCancel==false&&shadowCDCounter==shadowCDLength&&hero_script._isGrounded&&!hero_script.isShooting)
        {
            hero_script._anim.SetBool("isRunning", false);
            hero_script.enabled = false;
            Physics2D.IgnoreLayerCollision(7, 13);
            heroRB.bodyType = RigidbodyType2D.Static;
        

            shadowClone = Instantiate(shadow_prefab, mainHero.transform.position, mainHero.transform.rotation) ;
            
          
            shadowExist = true;
            
        }

        if (shadowExist == true)
        {
            shadowCDCounter = 0; shadowCDTimer.SetShadowCDTime(shadowCDCounter);
        }
        else
        {
            shadowCDCounter += Time.deltaTime;
            shadowCDTimer.SetShadowCDTime(shadowCDCounter);
        }
        if (shadowCDCounter > shadowCDLength) { shadowCDCounter = shadowCDLength; shadowCDTimer.SetShadowCDTime(shadowCDCounter); }


        if (hero_script.currentHealth <= 0) {
            hero_script.enabled = true;
            Destroy(shadowClone);
            shadowExist = false;
            shadowCounter = shadowLength;
        }

        if (shadowExist != true)
        {
            shadowCounter = shadowLength;

        }
        else if(shadowCounter>0)
        {

        shadowCounter-=Time.deltaTime;
            shadowTimer.SetShadowTime(shadowCounter);
        }



        if (shadowCounter < 0)
        {
            hero_script.enabled = true;
            heroRB.bodyType = RigidbodyType2D.Dynamic;
            heroCollider.enabled = true;

            mainHero.transform.position = shadowClone.transform.position;

            
            shadowExist = false;
            shadowCounter = shadowLength;
            
            Destroy(shadowClone);

        }
        if (shadowExist != true)
        {
            shadowSlider.SetActive(false);
        } else
        {
            shadowSlider.SetActive(true);
        }


       

    }

}
