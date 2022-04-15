using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour, ITakeDamage
{
    [SerializeField] public Transform firepoint;
    [SerializeField] public Transform directionalPoint;
    private Animator anim;
    public GameObject bullet;
    [SerializeField] public int damageTurret;
    float timebetween;
    [SerializeField] public float starttimebetween;
    bool isOnTurretArea=false;
    [SerializeField] private float _health;
    
    private AudioSource audioSource;
    
    private StatesT StateT
    {
        get { return (StatesT)anim.GetInteger("state"); }
        set { anim.SetInteger("state", (int)value); }
    } 

    void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        timebetween = starttimebetween;

        
    }

    // Update is called once per frame
    void Update()
    {
        CheckOnTurretArea();

        if (isOnTurretArea)
        {
            if (timebetween <= 0)
            {
                StateT = StatesT.shooting;
                audioSource.Play();
                Instantiate(bullet, firepoint.position, firepoint.rotation);
                timebetween = starttimebetween;
            }
            else
            {
                timebetween -= Time.deltaTime;
                StateT = StatesT.idle;
            }
        }

        Debug.Log(isOnTurretArea);
    }

    public void TakeDamage(int damage)
    {
        _health -= damage*Time.deltaTime;
        if (_health <= 0) Destroy(gameObject);
    }

    public void CheckOnTurretArea()
    {
        Vector2 direction = directionalPoint.position - firepoint.position;
        RaycastHit2D hit = Physics2D.Raycast(firepoint.position, direction.normalized, direction.magnitude);
        if (hit)
        {
            ITakeDamage damagable = hit.collider.GetComponent<ITakeDamage>();
            if (damagable != null)
            {
                isOnTurretArea = true;
            }
            else isOnTurretArea = false;
        }
        else isOnTurretArea = false;
    }
}
public enum StatesT
{
    idle,
    shooting
  
}
