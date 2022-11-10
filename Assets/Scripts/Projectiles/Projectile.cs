using UnityEngine;

namespace Projectiles
{
    public class Projectile : MonoBehaviour
    {
        public const float ProjectileHeight = 0.1f;
        
        public int Damage { get; private set; } = 10;
        
        public Transform gapTransform;

        private Bullet _bullet;

        private void Awake()
        {
            _bullet = GetComponentInChildren<Bullet>();
        }

        public bool ShouldOrbit()
        {
            return _bullet.ShouldOrbit();
        }

        public void Shoot(Vector3 target)
        {
            _bullet.Shoot(target);
        }

        public void OrbitAround(Vector3 point)
        {
            gapTransform.RotateAround(point, Vector3.up, 20 * Time.deltaTime);
            if (ShouldOrbit())
            {
                _bullet.transform.RotateAround(point, Vector3.up, 20 * Time.deltaTime);
            }
        }
        
        public void UpdateProjectilePosition(Vector3 deltaPos)
        {
            gapTransform.position += deltaPos;
            if(ShouldOrbit())
            {
                _bullet.transform.position += deltaPos;
            }
        }

        public void Init(Vector3 position, Quaternion rotation, float yAngle)
        {
            _bullet.transform.localPosition = position;
            _bullet.transform.localRotation = rotation;
            gapTransform.localPosition = position;
            gapTransform.localRotation = rotation;
        }

        public Vector3 GetBulletPosition()
        {
            return _bullet.transform.position;
        }
    }
}
