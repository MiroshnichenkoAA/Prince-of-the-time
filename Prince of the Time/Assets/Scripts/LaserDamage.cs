using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserDamage : MonoBehaviour
{
    [SerializeField] public Transform maxShotDistance;
    [SerializeField] public Transform firePoint;
    [SerializeField] public int laserDamage;
    private LineRenderer _lineRenderer;
    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }
    void Update()
    {
        Vector2 direction = maxShotDistance.position - firePoint.position;
        RaycastHit2D hit = Physics2D.Raycast(firePoint.position, direction.normalized, direction.magnitude);
        if (hit&&_lineRenderer.enabled==true)
        {
            ITakeDamage damagable = hit.collider.GetComponent<ITakeDamage>();
            if (damagable != null)
                damagable.TakeDamage(laserDamage);

        }
    }
}
