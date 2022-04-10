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

    private bool movingRight = true;
    public Transform groundDetection;
    public float distance;

    [SerializeField] private Transform _player;
    // Start is called before the first frame update
    void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
        timeBtwShots = startTimeBtwShots;
    }

    // Update is called once per frame
    void Update()
    {
        if (isOnRoboArea)
        {
            Chasing();
            Flipping();
            Shooting();
        }
        else
            Patroling();

       


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
                movingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }
       

    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Main Character"))
        {
            isOnRoboArea = true;
        }
        else isOnRoboArea = false;
    }



}
