using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    [SerializeField] private float slowTime = 1f;
    [SerializeField] private float slowTimeCounter = 1f;

    [SerializeField] private float speed = 3f;
    [SerializeField] private float jumpForce = 15f;
    [SerializeField] public Turret turret;

    //Links to component
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator anim;

    public Vector3 respawnPoint;
    public GameObject fallDetector;

    [SerializeField] public GameObject lightInHandLeft;
    [SerializeField] public GameObject lightInHandRight;

    //Health
    public int maxHealth;
    public int currentHealth;
    public HealthBar healthBar;
    

    //Tile jump time
    public float handTime=2;
    public float handCounter;

    //Jump Buffer
    public float jumpBufferLength;
    public float jumpBufferCount;

    public StatesA State
    {
        get { return (StatesA)anim.GetInteger("state"); }
        set { anim.SetInteger("state",(int)value); }
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
        currentHealth= maxHealth;
        slowTimeCounter = slowTime;
        healthBar.SetMaxHealth(maxHealth);

    }
    
    private void Update()
    {
        
        if (rb.velocity.y == 0)
       State = StatesA.idle;


        if (sprite.flipX == true) {
            lightInHandLeft.SetActive(true);
            lightInHandRight.SetActive(false);
        }
        else {
            lightInHandLeft.SetActive(false);
            lightInHandRight.SetActive(true);
                }


        if (rb.velocity.y == 0)
        {
            handCounter = handTime;
        }
        else
        {
            handCounter-=Time.deltaTime;
        }


        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferCount = jumpBufferLength;
        }
        else
        {
            jumpBufferCount -=Time.deltaTime;
        }


            if (handCounter>0.0f && jumpBufferCount>=0)
                Jump();
        if (rb.velocity.y > 0 && Input.GetButtonUp("Jump"))
            SmallJump();

        if (Input.GetButton("Horizontal"))
      
            Run();



            if (rb.velocity.y==0)
            fallDetector.transform.position = new Vector3(transform.position.x, transform.position.y-10,0);

            if (currentHealth <= 0)
            {
                transform.position = respawnPoint;
                currentHealth = maxHealth;
                healthBar.SetMaxHealth(maxHealth);

            }
           

        
    }
  

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "FallDetector")
        {
            transform.position = respawnPoint;
            currentHealth = maxHealth;
            healthBar.SetMaxHealth(maxHealth);
        }
        if (collision.tag=="Bullet")
        {
            TakeDamageTurret();
            SlowTheGame();
        }
    }
    




    private void Run()
    {   if (rb.velocity.y == 0) State = StatesA.run;
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
        rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.25f) ;
    }

    public void SlowTheGame()
    {  if(slowTimeCounter<=0)
        Time.timeScale = 0.5f;
    else
        {
            Time.timeScale = 1f;
            slowTimeCounter -= Time.deltaTime;
        }


    }

    public void TakeDamageTurret()
    {
        currentHealth -= turret.damageTurret;
        healthBar.SetHealth(currentHealth);
        
    }
}

public enum StatesA
{
    idle,
    run,
    jump,
    dash,
    wallslide
}