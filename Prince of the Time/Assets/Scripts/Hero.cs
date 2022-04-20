using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour,ITakeDamage
{

    
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    public LayerMask enemyLayer;

    [SerializeField] public Turret turret;
    public float jumpTime;
    public float jumpTimeCounter;
    public bool isJumping;

    public bool _isGrounded;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;
    public bool jumpLanded;

    //Links to component
    private Rigidbody2D _rb;
    private SpriteRenderer _sprite;
    public Animator _anim;
    private AudioSource audioSource;

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
    public bool isShooting;
    public RaycastHit2D hit;
    public bool isLookingLeft;
    public bool isShootingLeft;
  


    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sprite = GetComponentInChildren<SpriteRenderer>();
        _anim = GetComponentInChildren<Animator>();
        audioSource = GetComponent<AudioSource>();
        
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
        IsGroundedCheck();
       

    }

    private void Update()
    {
      
        JumpLogic();
        AttackLogic();



        Debug.Log("ISSHOOTINGLEFT" + isShootingLeft);
    }





    private void EnableLaser()
    { 
       
            _anim.SetTrigger("startAttack");
        lineRenderer.enabled = true;
        audioSource.Play();
    }


    private void UpdateLaser()
    {
        _rb.bodyType = RigidbodyType2D.Static;
        _anim.SetBool("isAttacking", true);
        isShooting = true;
      
 
         lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, maxShotDistance.position);
        laserParticle.SetActive(true);
        laserParticleEnd.SetActive(true);
        laserParticle.transform.position = lineRenderer.GetPosition(0);
     
        Vector2 direction = maxShotDistance.position - firePoint.position;

         RaycastHit2D hit = Physics2D.Raycast(firePoint.position, direction.normalized, direction.magnitude );
         if (hit)
         {
             lineRenderer.SetPosition(1, hit.point);
            
         }
        laserParticleEnd.transform.position = lineRenderer.GetPosition(1);
    }


    private void DisableLaser()
    {
        lineRenderer.enabled = false;
       laserParticleEnd.SetActive(false);
        laserParticle.SetActive(false);
        audioSource.Stop();
        isShooting = false;
        _rb.bodyType = RigidbodyType2D.Dynamic;
        _anim.SetBool("isAttacking", false);
        lineRenderer.SetPosition(1, firePoint.position);
    }


    private void AttackLogic()
    {
       if (Input.GetKey(KeyCode.V) && _isGrounded && !isShooting)
        {
          
            EnableLaser();
        }
        

        if (Input.GetKey(KeyCode.V) && _isGrounded)
        {
            
            UpdateLaser();
        }

       

        if (Input.GetKeyUp(KeyCode.V))
        {
            DisableLaser();
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
        if (collision.tag == "Laser")
        {
            Die();

        }
    }
    

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }


    private void MovementLogic()
    {
        
        float moveInput = Input.GetAxis("Horizontal");
        if (isShooting == false)
         
       _rb.velocity = new Vector2(moveInput * speed,_rb.velocity.y);
        if (moveInput < 0.0f)
        {
            isLookingLeft = true;
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 180, transform.rotation.eulerAngles.z);
        }
        if(moveInput>0.0f) {
            isLookingLeft = false;
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 0, transform.rotation.eulerAngles.z); 
        }

       


      
        




        if((moveInput < 0.0f)&&isShooting&&!isShootingLeft)
        {
            isShootingLeft = true;
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 180, transform.rotation.eulerAngles.z);
        }

        if (isShootingLeft&& isShooting)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 180, transform.rotation.eulerAngles.z);

        }
       
        if (moveInput > 0)
        {
            isShootingLeft = false;
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 0, transform.rotation.eulerAngles.z);
        }
     




        if ((moveInput != 0) && _isGrounded) _anim.SetBool("isRunning", true);
        if ((moveInput == 0) && _isGrounded)  _anim.SetBool("isRunning", false);
    }

    private void JumpLogic()
    {
        if (_isGrounded && Input.GetButtonDown("Jump")&& isShooting == false)
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
            Die();
            
        }
        

    }
    private void Die()
    {
        transform.position = respawnPoint;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

   

    





    private void TakeDamageTurret()
    {     
        currentHealth -= turret.damageTurret;
        healthBar.SetHealth(currentHealth);
        
    }
}

