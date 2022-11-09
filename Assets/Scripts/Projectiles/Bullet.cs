using System.Collections;
using Constants;
using UnityEngine;

namespace Projectiles
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float respawnTime;
        [SerializeField] private Transform gapTransform;
        [SerializeField] private float speed;
        
        private Vector3 _velocity;
        private Rigidbody _rigidbody;
        private Projectile _parentProjectile;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _parentProjectile = GetComponentInParent<Projectile>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(Tags.Enemy))
            {
                other.gameObject.GetComponentInParent<EnemyController>().TakeDamage(_parentProjectile.Damage);
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
