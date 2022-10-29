using System;
using UnityEngine;

namespace Projectiles
{
    public class Bullet: MonoBehaviour
    {
        Projectile _projectile;

        private void Start()
        {
            _projectile = GetComponentInParent<Projectile>();
        }
        
        private void OnCollisionEnter(Collision other)
        {
            if(other.gameObject.CompareTag(Tags.Enemy))
            {
                // TODO: Hit the enemy
                
                StartCoroutine(_projectile.Reload());
            }
        }
    }
}