using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    private Vector3 _firingPoint;
    [SerializeField] private float _projectileSpeed;

    [SerializeField] private float _maxProjectileDistance;

    [SerializeField] private float _damage;

    private GameObject _triggeringPlayer;

    private void Start()
    {
        _firingPoint = transform.position;
    }

    private void Update()
    {
        
        MoveProjectile();
    }

    void MoveProjectile() {
        if (Vector3.Distance(_firingPoint, transform.position) > _maxProjectileDistance)
        {
            Destroy(this.gameObject);
        } 
        else 
        {
            transform.Translate(Vector3.forward * _projectileSpeed * Time.deltaTime);
        }
        transform.Translate(Vector3.forward * _projectileSpeed * Time.deltaTime);
    }
    
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player")
        {
            _triggeringPlayer = other.gameObject;
            _triggeringPlayer.GetComponent<PyraController>()._phealth -= _damage;
            Destroy(this.gameObject);
        }
        else if(other.tag == "Wall") // Obiectele puse de Daria pot avea tag-ul Wall 
        {
            Destroy(this.gameObject);
        }
    }
}
