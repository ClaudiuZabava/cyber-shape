using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Projectiles
{
    public class Projectile : MonoBehaviour
    {
        private Vector3 _velocity;
        public float speed;

        public void Shoot(Vector3 target)
        {
            transform.LookAt(target);
            // Still rotate it 90 deg to keep the orientation to the camera
            transform.Rotate(-90, 0, 0);
            var relativePos = target - transform.position;
            var direction = relativePos.normalized;
            _velocity = direction * speed;
        }

        public bool ShouldOrbit()
        {
            return _velocity.magnitude == 0;
        }

        private void Update()
        {
            transform.position += _velocity * Time.deltaTime;
        }
    }
}
