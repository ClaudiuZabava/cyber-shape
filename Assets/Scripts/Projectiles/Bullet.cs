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
        [SerializeField] private BulletType type;

        public BulletType Type
        {
            get => type;
            set
            {
                type = value;
                _meshCollider.sharedMesh = value.Mesh;
                _meshFilter.mesh = value.Mesh;
            }
        }

        public bool ReadyForShooting { get; private set; } = true;

        private Vector3 _velocity;
        private Rigidbody _rigidbody;
        private Renderer _renderer;
        private MeshFilter _meshFilter;
        private MeshCollider _meshCollider;
        private float _damageMultiplier = 1.0f;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _renderer = GetComponent<Renderer>();
            _meshFilter = GetComponent<MeshFilter>();
            _meshCollider = GetComponent<MeshCollider>();
        }

        private void OnTriggerEnter(Collider other) 
        {
            if (other.gameObject.CompareTag(Tags.Enemy))
            {
                var enemyController = other.gameObject.GetComponentInParent<EnemyController>();
                enemyController.TakeDamage(Type.DamageInfo.ContactDamage * _damageMultiplier);
            }

            var explosion = ExplosionPool.Instance.Get();
            explosion.transform.position = transform.position;
            explosion.transform.rotation = Quaternion.identity;
            StartCoroutine(Reload());
        }

        public void Shoot(Vector3 target, float damageMultiplier)
        {
            _damageMultiplier = damageMultiplier;
            var relativePos = target - transform.position;
            relativePos.y = 0;
            var direction = relativePos.normalized;

            transform.LookAt(target);
            // Keep the projectile oriented parallel to the ground
            transform.Rotate(90, 0, 0);
            _velocity = direction * Type.Speed;
            _rigidbody.velocity = _velocity;
        }

        public bool ShouldOrbit => _velocity.magnitude == 0;

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
            GetComponent<MeshRenderer>().material.SetColor("_BaseColor", Colors.BlueBase);
            GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", Colors.BlueEmission);
        }
    }
}
