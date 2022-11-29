using System;
using System.Collections;
using Constants;
using Enemy;
using UnityEngine;

namespace Projectiles
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float respawnTime;
        [SerializeField] private Transform gapTransform;
        [SerializeField] private float speed;
        [SerializeField] private GameObject explosionParticles;

        public bool ReadyForShooting { get; private set; } = true;
        
        private Vector3 _velocity;
        private Rigidbody _rigidbody;
        private Renderer _renderer;
        private Projectile _parentProjectile;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _renderer = GetComponent<Renderer>();
            _parentProjectile = GetComponentInParent<Projectile>();
        }

        private void OnTriggerEnter(Collider other) 
        {
            if (other.gameObject.CompareTag(Tags.Enemy))
            {
                other.gameObject.GetComponentInParent<EnemyController>().TakeDamage(_parentProjectile.Damage);
            }

            var explosion = ExplosionPool.Instance.Get();
            explosion.transform.position = transform.position;
            explosion.transform.rotation = Quaternion.identity;
            StartCoroutine(Reload());
        }

        public void Shoot(Vector3 target)
        {
            var relativePos = target - transform.position;
            relativePos.y = 0;
            var direction = relativePos.normalized;

            transform.LookAt(target);
            // Keep the projectile oriented parallel to the ground
            transform.Rotate(90, 0, 0);
            _velocity = direction * speed;
            _rigidbody.velocity = _velocity;
        }

        public bool ShouldOrbit()
        {
            return _velocity.magnitude == 0;
        }

        private IEnumerator Reload()
        {
            ReadyForShooting = false;
            _renderer.enabled = false;
            _velocity = Vector3.zero;
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
            transform.localPosition = gapTransform.localPosition;
            transform.localRotation = gapTransform.localRotation;
            
            yield return new WaitForSeconds(respawnTime);
            
            _renderer.enabled = true;
            ReadyForShooting = true;
        }
    }
}
