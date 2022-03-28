using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow_Hero : MonoBehaviour
{


    [SerializeField] private float speed = 3f;
    [SerializeField] private float sprintSpeed = 7f;
    [SerializeField] private float staminaSpeed = 2f;
    [SerializeField] private float stamina = 5f;
    [SerializeField] private float dashCD = 1f;
    [SerializeField] private float jumpForce = 15f;
    [SerializeField] private float DashForce = 15f;
    [SerializeField] private float StartDashTimer = 15f;
    [SerializeField] private float CurrentDashTimer;
    [SerializeField] private float DashDirection;

    [SerializeField] public Hero hero_script;
    [SerializeField] public Shadow_Manager shadow_Manager;


    //WallSliding
    [SerializeField] private float wallSlideSpeed = 0;
    [SerializeField] private LayerMask wallLayer;
   
    [SerializeField] Vector2 wallCheckSize;
    [SerializeField] public bool isTouchingWall;
    [SerializeField] private bool isWallSliding;

    [SerializeField] bool isDashing;

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

        if (Input.GetButton("Horizontal") && !Input.GetKey(KeyCode.LeftShift))
        {

            Run();

            stamina += staminaSpeed * Time.deltaTime;
            if (stamina > 5) stamina = 5;
        }


        if (Input.GetKey(KeyCode.LeftShift) && rb.velocity.y == 0 && stamina > 0)
        {

            Sprint();
            stamina -= 2 * staminaSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.LeftShift) && rb.velocity.y == 0)
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


        float movX = Input.GetAxis("Horizontal");


        if ((Input.GetMouseButtonDown(0)) && movX != 0 && dashCD == 1)
        {
            isDashing = true;
            CurrentDashTimer = StartDashTimer;
            rb.velocity = Vector2.zero;
            DashDirection = (int)movX;
        }


        if (isDashing)
        {
            State = StatesA.dash;
            rb.velocity = transform.right * DashDirection * DashForce;
            dashCD = 0;
            CurrentDashTimer -= Time.deltaTime;
            if (CurrentDashTimer <= 0)
                isDashing = false;

        }

        WallSlide();
      
        
        


    }


    
    private void WallSlide()
    {
        if (isTouchingWall)
        {
            State = StatesA.wallslide;
        }


        if (isTouchingWall /* && !grounded */ && rb.velocity.y < 0)
        {
            isWallSliding = true;

        }
        else
        {
            if (rb.velocity.y != 0 && rb.IsSleeping() == false)
                State = StatesA.jump;
            isWallSliding = false;
        }
        if (isWallSliding)
        {

            rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeed);
        }



    }




    private void Run()
    {
        if (rb.velocity.y == 0) State = StatesA.run;
        Vector3 dir = transform.right * Input.GetAxis("Horizontal");

        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);

        sprite.flipX = dir.x < 0.0f;
    }




   




    private void Sprint()
    {
        if (rb.velocity.y == 0) State = StatesA.run;
        Vector3 dir = transform.right * Input.GetAxis("Horizontal");

        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, sprintSpeed * Time.deltaTime);

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

    public void TakeDamageTurret()
    {
        currentHealth -= damageTurret;
        healthBar.SetHealth(currentHealth);

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