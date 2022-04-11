using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Robot : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _stoppingDistance;
    [SerializeField] private float _retrearDistance;
    private SpriteRenderer _sprite;
    private float timeBtwShots;
    public float startTimeBtwShots;
    public GameObject roboBulletPrefab;
    public Transform shotPoint;
    public bool isOnRoboArea;
    public bool boundToFall;
    public float checkDistance;
    Vector3 leftPoint;
    Vector3 rightPoint;
    private bool movingRight = true;
    public Transform groundDetection;
    public float distance;
    [SerializeField] private Transform _feetPos;
    [SerializeField] private LayerMask _whatIsGround;
    [SerializeField] private bool _isGrounded;
    [SerializeField] private float _checkRadius;
    private AudioSource _audioSource;
    

    [SerializeField] private Transform _player;
    // Start is called before the first frame update
    void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
        timeBtwShots = startTimeBtwShots;
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        IsNearRoboCheck();
        GroundCheckUnderFeet();
        if (isOnRoboArea && _isGrounded)
        {
            NoFallWhileChasing();
            Chasing();
            Flipping();
            Shooting();

        }
        else if (_isGrounded)
        {
            Patroling();
        } 
       


    }

   private void Chasing()
    {
        if (Vector2.Distance(transform.position, _player.position) > _stoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, _player.position, _speed * Time.deltaTime);
        }
        else if (Vector2.Distance(transform.position, _player.position) < _stoppingDistance && Vector2.Distance(transform.position, _player.position) > _retrearDistance)
        {
            transform.position = this.transform.position;
        }
        else if (Vector2.Distance(transform.position, _player.position) < _retrearDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, _player.position, -_speed * Time.deltaTime);
        }
    }
    private void Flipping()
    {
        if (_player.position.x > transform.position.x)
        {
            this.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 180, transform.rotation.eulerAngles.z);



        }
        else this.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 0, transform.rotation.eulerAngles.z);
    }
    private void Shooting()
    {
        if (timeBtwShots <= 0)
        {
            _audioSource.Play();
            Instantiate(roboBulletPrefab, shotPoint.position, shotPoint.rotation);


            timeBtwShots = startTimeBtwShots;
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }
    private void Patroling()
    {
        transform.Translate(Vector2.right * -_speed * Time.deltaTime);
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance);
        
        if (groundInfo.collider == false)
        {
           
            if (movingRight == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                rightPoint = transform.position;
               
                movingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
                leftPoint = transform.position;
              
            }
        }
       

    }

    private void IsNearRoboCheck()
    {
        if ((Vector2.Distance(transform.position, _player.position) < checkDistance))
        {
            
            isOnRoboArea = true;
        }
        else
        {
            
            isOnRoboArea = false;
        }
    }
    
    private void NoFallWhileChasing()
    {
        if(transform.position==rightPoint)
        {
            transform.position = rightPoint;
        }
        if (transform.position == leftPoint)
        {
            transform.position = leftPoint;
        }
    }
    private void GroundCheckUnderFeet()
    {
        _isGrounded = Physics2D.OverlapCircle(_feetPos.position, _checkRadius, _whatIsGround);
    }

}
