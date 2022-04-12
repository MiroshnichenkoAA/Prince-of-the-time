using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow_Hero : MonoBehaviour
{


   
  

    [SerializeField] private float speed = 3f;
    [SerializeField] private float jumpForce = 15f;

    [SerializeField] public Turret turret;
    public float jumpTime;
    public float jumpTimeCounter;
    private bool isJumping;

    private bool _isGrounded;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;

    //Links to component
    private Rigidbody2D _rb;
    private SpriteRenderer _sprite;
    private Animator _anim;

    public Vector3 respawnPoint;
   

  

   


   

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sprite = GetComponentInChildren<SpriteRenderer>();
        _anim = GetComponentInChildren<Animator>();

    }

    private void Start()
    {
        respawnPoint = transform.position;
       

    }
    private void FixedUpdate()
    {
        MovementLogic();

       
        
    }

    private void Update()
    {
        IsGroundedCheck();

        JumpLogic();


        














    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "FallDetector")
        {
            transform.position = respawnPoint;
     
        }
        
    }






    private void MovementLogic()
    {

        float moveInput = Input.GetAxis("Horizontal");
        _rb.velocity = new Vector2(moveInput * speed, _rb.velocity.y);
        if (moveInput < 0.0f)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 180, transform.rotation.eulerAngles.z);
        }
        else transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 0, transform.rotation.eulerAngles.z);

        if ((moveInput != 0) && _isGrounded) _anim.SetBool("isRunning", true);
        if ((moveInput == 0) && _isGrounded) _anim.SetBool("isRunning", false);
    }

    private void JumpLogic()
    {
        if (_isGrounded && Input.GetButtonDown("Jump"))
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            _rb.velocity = Vector2.up * jumpForce;
        }
        if (Input.GetButton("Jump") && isJumping)
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
        }
        else _anim.SetBool("isJumping", true);
    }


    private void IsGroundedCheck()
    {
        _isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);
    }

   
 







  
}

