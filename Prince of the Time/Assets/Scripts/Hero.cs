using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
  

    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;

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
    public Animator _anim;

    public Vector3 respawnPoint;
    public GameObject fallDetector;

    [SerializeField] public GameObject lightInHandLeft;
    [SerializeField] public GameObject lightInHandRight;

    //Health
    public int maxHealth;
    public int currentHealth;
    public HealthBar healthBar;

    //Laser
    public LineRenderer lineRenderer;
    public Transform firePoint;
    public Transform maxShotDistance;
    public GameObject laserParticle;
    public GameObject laserParticleEnd;



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
        DisableLaser();

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







        if (Input.GetKeyDown(KeyCode.V))
        {
            _anim.SetTrigger("startAttack");
            EnableLaser();
            
        }

        if (Input.GetKey(KeyCode.V))
        {
            _anim.SetBool("isAttacking", true);
            UpdateLaser();
            

        }

        if (Input.GetKeyUp(KeyCode.V))
        {
            DisableLaser();
            _anim.SetBool("isAttacking", false);

        }




    }
    private void EnableLaser()
    {
        lineRenderer.enabled = true;
    }
    private void UpdateLaser()
    {
        laserParticle.SetActive(true);
        laserParticleEnd.SetActive(true);
        laserParticleEnd.transform.position = lineRenderer.GetPosition(1);

        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, maxShotDistance.position);
        Vector2 direction = maxShotDistance.position - firePoint.position;

         RaycastHit2D hit = Physics2D.Raycast(firePoint.position, direction.normalized, direction.magnitude );
         if (hit)
         {
             lineRenderer.SetPosition(1, hit.point);
         }
    }
    private void DisableLaser()
    {
        lineRenderer.enabled = false;
        laserParticleEnd.SetActive(false);
        laserParticle.SetActive(false);
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
        if(moveInput < 0.0f)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 180, transform.rotation.eulerAngles.z);
        } else transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 0, transform.rotation.eulerAngles.z);
        //_sprite.flipX = moveInput < 0.0f;
      
        if ((moveInput != 0) && _isGrounded) _anim.SetBool("isRunning", true);
        if ((moveInput == 0) && _isGrounded)  _anim.SetBool("isRunning", false);
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

        if (_isGrounded)
        {
            _anim.SetBool("isJumping", false);
        } else _anim.SetBool("isJumping",true);
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

