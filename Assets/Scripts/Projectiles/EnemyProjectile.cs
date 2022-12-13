using Constants;
using UnityEngine;

namespace Projectiles
{
    public class EnemyProjectile : MonoBehaviour
    {
        [SerializeField] private float projectileSpeed;
        [SerializeField] private float maxProjectileDistance;
        [SerializeField] private float damage;

        private GameObject _triggeringPlayer;
        private Vector3 _firingPoint;

        private void Start()
        {
            transform.Rotate(90, 0, 0);
            _firingPoint = transform.position;
            Physics.IgnoreLayerCollision(8, 9, true);
        }

        private void Update()
        {
            MoveProjectile();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Tags.Player))
            {
                _triggeringPlayer = other.gameObject;
                _triggeringPlayer.GetComponent<Player>().TakeDamage(0);
                Destroy(gameObject);
            }
            else if (other.CompareTag(Tags.Wall)) // Obiectele puse de Daria pot avea tag-ul Wall 
            {
                Destroy(gameObject);
            }
        }

        private void MoveProjectile()
        {
            if (Vector3.Distance(_firingPoint, transform.position) > maxProjectileDistance)
            {
                Destroy(gameObject);
            }
            else
            {
                transform.Translate(Vector3.up * projectileSpeed * Time.deltaTime);
            }
            transform.Translate(Vector3.up * projectileSpeed * Time.deltaTime);
        }
    }
}