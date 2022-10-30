using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vector3 firingPoint;

    [SerializeField]
    private float projectileSpeed;

    [SerializeField]
    private float maxProjectileDistance;


    
    private void Start()
    {
        firingPoint = transform.position;
    }

    private void Update()
    {
        MoveProjectile();
    }

    private void MoveProjectile() {
        if (Vector3.Distance(firingPoint, transform.position) > maxProjectileDistance){
            Destroy(this.gameObject);
        } else {
            transform.Translate(Vector3.forward * projectileSpeed * Time.deltaTime);
        }
        transform.Translate(Vector3.forward * projectileSpeed * Time.deltaTime);
    }
}
