using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public float dumping = 1.5f;
    public Vector2 offset = new Vector2(2f, 1f);
    public bool isLeft;
    private Transform player;
    
    private int LastX;
    [SerializeField] public Shadow_Manager shadowManager;
    [SerializeField] public Hero hero;
    private Animator _anim;


    [SerializeField]
    float leftLimit;
    [SerializeField]
    float rightLimit;
    [SerializeField]
    float bottomLimit;
    [SerializeField]
    float upperLimit;



    


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        offset = new Vector2(Mathf.Abs(offset.x), offset.y);

        _anim = GetComponent<Animator>();
            FindPlayer(isLeft);
        
    }


    public void FindPlayer(bool playerIsLeft)
    {
        
            
        
        LastX = Mathf.RoundToInt(player.position.x);
        if (playerIsLeft)
        {
            transform.position = new Vector3(player.position.x - offset.x, player.position.y - offset.y, transform.position.z);

        }
        else
        {
            transform.position = new Vector3(player.position.x + offset.x, player.position.y + offset.y, transform.position.z);
        }





    }

    





    
    void Update()
    {

        if (hero._isGrounded)
        {
            if (hero.jumpLanded)
            {
                _anim.SetTrigger("jumpShake");
                hero.jumpLanded = false;
            }
        }
        else hero.jumpLanded = true;


        if (shadowManager.shadowExist != true) { 
        player = GameObject.FindGameObjectWithTag("Player").transform;
        } else player = GameObject.FindGameObjectWithTag("Shadow").transform;


        if (player)
        {
            int currentX = Mathf.RoundToInt(player.position.x);
            if (currentX > LastX) isLeft = false; else if (currentX < LastX) isLeft = true;
            LastX = Mathf.RoundToInt(player.position.x);

            Vector3 target;
            if (isLeft)
            {
                target = new Vector3(player.position.x - offset.x, player.position.y + offset.y, transform.position.z);
            }
            else
            {
                target = new Vector3(player.position.x + offset.x, player.position.y + offset.y, transform.position.z);
            }

            Vector3 currentPosition = Vector3.Lerp(transform.position, target, dumping * Time.deltaTime);
            transform.position = currentPosition;

        } 

        transform.position = new Vector3
            (
            Mathf.Clamp(transform.position.x, leftLimit, rightLimit),
            Mathf.Clamp(transform.position.y, bottomLimit, upperLimit),
            transform.position.z


            ); 



    }



}