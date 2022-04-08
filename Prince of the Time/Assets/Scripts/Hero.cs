using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
  

    [SerializeField] private float speed = 3f;
    [SerializeField] private float jumpForce = 15f;

    [SerializeField] public Turret turret;
    public float jumpTime;
    public float jumpTimeCounter;
    private bool isJumping;

    public bool _isGrounded;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;

    //Links to component
    private Rigidbody2D _rb;
    private SpriteRenderer _sprite;
    private Animator _anim;

    public Vector3 respawnPoint;
    public GameObject fallDetector;

    [SerializeField] public GameObject lightInHandLeft;
    [SerializeField] public GameObject lightInHandRight;

    //Health
    public int maxHealth;
    public int currentHealth;
    public HealthBar healthBar;
    


    public StatesA State
    {
        get { return (StatesA)_anim.GetInteger("state"); }
        set { _anim.SetInteger("state",(int)value); }
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sprite = GetComponentInChildren<SpriteRenderer>();
        _anim = GetComponentInChildren<Animator>();
        
    }

    private void Start()
    {
        respawnPoint = transform.position;
        currentHealth= maxHealth;
        healthBar.SetMaxHealth(maxHealth);

    }
    private void FixedUpdate()
    {
        MovementLogic();
        
        FallDetectorChasingThePlayer();
        DieCheck();
    }

    private void Update()
    {
        IsGroundedCheck();

        JumpLogic();


        if (_sprite.flipX == true) {
            lightInHandLeft.SetActive(true);
            lightInHandRight.SetActive(false);
        }
        else {
            lightInHandLeft.SetActive(false);
            lightInHandRight.SetActive(true);
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
            
        }
    }
    




    
    private void MovementLogic()
    {
        
        float moveInput = Input.GetAxis("Horizontal");
       _rb.velocity = new Vector2(moveInput * speed,_rb.velocity.y);
        _sprite.flipX = moveInput < 0.0f;
      
        if ((moveInput != 0) && _isGrounded) State = StatesA.run;
        if ((moveInput == 0) && _isGrounded) State = StatesA.idle;
    }

    private void JumpLogic()
    {
        if (_isGrounded && Input.GetButtonDown("Jump"))
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            _rb.velocity = Vector2.up * jumpForce;
        }
        if (Input.GetButton("Jump")&&isJumping)
        {
            if (jumpTimeCounter > 0)
            {
                _rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;

            }
            else
                isJumping = false;
        }

        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
        }

        if (isJumping) State = StatesA.jump;
    }

    
    private void IsGroundedCheck()
    {
        _isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);
    }
   
    private void FallDetectorChasingThePlayer() 
    {
        if(_isGrounded)
        fallDetector.transform.position = new Vector3(transform.position.x, transform.position.y - 10, 0);
    }

    private void DieCheck()
    {
        if (currentHealth <= 0)
        {
            transform.position = respawnPoint;
            currentHealth = maxHealth;
            healthBar.SetMaxHealth(maxHealth);

        }

    }







    private void TakeDamageTurret()
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