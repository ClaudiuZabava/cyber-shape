using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Projectiles
{
    public class Bullet : MonoBehaviour
    {
        private Vector3 _velocity;
        public float speed;
        public float respawnTime;
        public Transform gapTransform;
        private Rigidbody _rigidbody;
        public float initialYAngle;
        private Projectile _parentProjectile;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _parentProjectile = GetComponentInParent<Projectile>();
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag(Tags.Enemy))
            {
                other.gameObject.GetComponentInParent<EnemyBody>().TakeDamage(_parentProjectile.damage);
                StartCoroutine(Reload());
            }
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
        }

        public bool ShouldOrbit()
        {
            return _velocity.magnitude == 0;
        }

        private void FixedUpdate()
        {
            transform.localPosition += _velocity * Time.deltaTime;
        }

        private IEnumerator Reload()
        {
            yield return new WaitForSeconds(respawnTime);

            _velocity = Vector3.zero;
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
            transform.localPosition = gapTransform.localPosition;
            transform.localRotation = gapTransform.localRotation;
        }
    }
}