using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow_Hero : MonoBehaviour
{


    [SerializeField] private float speed = 3f;
    [SerializeField] private float jumpForce = 15f;
    

    [SerializeField] public Hero hero_script;
    [SerializeField] public Shadow_Manager shadow_Manager;



    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator anim;

    public Vector3 respawnPoint;
  

    //Health
    public int maxHealth;
    public int currentHealth;
    public HealthBar healthBar;
    public int damageTurret;

    //Tile jump time
    public float handTime = 2;
    public float handCounter;

    //Jump Buffer
    public float jumpBufferLength;
    public float jumpBufferCount;

    private StatesA State
    {
        get { return (StatesA)anim.GetInteger("state"); }
        set { anim.SetInteger("state", (int)value); }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();

    }

    private void Start()
    {
        respawnPoint = transform.position;
        currentHealth = maxHealth;
        

    }

    

    private void Update()
    {


        if (rb.velocity.y == 0)
            State = StatesA.idle;


        if (rb.velocity.y == 0)
        {
            handCounter = handTime;
        }
        else
        {
            handCounter -= Time.deltaTime;
        }

        if (Input.GetButton("Horizontal"))
        
            Run();

           


        



        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferCount = jumpBufferLength;
        }
        else
        {
            jumpBufferCount -= Time.deltaTime;
        }


        if (handCounter > 0.0f && jumpBufferCount >= 0)
            Jump();
        if (rb.velocity.y > 0 && Input.GetButtonUp("Jump"))
            SmallJump();


        


    }



    private void Run()
    {
        if (rb.velocity.y == 0) State = StatesA.run;
        Vector3 dir = transform.right * Input.GetAxis("Horizontal");

        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);

        sprite.flipX = dir.x < 0.0f;
    }

    private void Jump()
    {
        State = StatesA.jump;
        rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        jumpBufferCount = 0;
    }
    private void SmallJump()
    {
        State = StatesA.jump;
        rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.25f);
    }

}

public enum Statesa
{
    idle,
    run,
    jump,
    dash,
    wallslide
}