using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Projectiles
{
    public class Projectile : MonoBehaviour
    {
        private Vector3 _velocity;
        private Transform _playerTransform;
        public float speed;
        public float respawnTime;
        public Transform bulletTransform;

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
            bulletTransform.position += _velocity * Time.deltaTime;
        }

        private void Start()
        {
            _playerTransform = GameObject.FindWithTag(Tags.Player).transform;
        }

        public IEnumerator Reload()
        {
            yield return new WaitForSeconds(respawnTime);

            bulletTransform.localPosition = Vector3.zero;
            bulletTransform.localRotation = Quaternion.identity;
            _velocity = Vector3.zero;
        }
    }
}
