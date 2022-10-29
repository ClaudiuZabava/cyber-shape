using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Projectiles
{
    public class ProjectileOrbitalController : MonoBehaviour
    {
        [SerializeField] public int projectileCount = 5;
        [SerializeField] public float radius = 0.3f;
        [SerializeField] public GameObject projectilePrefab;

        private List<Projectile> _projectiles = new List<Projectile>();

        private void Start()
        {
            for (int i = 0; i < projectileCount; i++)
            {
                var projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                projectile.transform.parent = transform;
                projectile.transform.localPosition = new Vector3(radius * Mathf.Cos(i * 2 * Mathf.PI / projectileCount),
                    0.1f,
                    radius * Mathf.Sin(i * 2 * Mathf.PI / projectileCount));
                // Orient the projectile tangent to the circle
                projectile.transform.rotation =
                    Quaternion.Euler(90, -i * 360 / projectileCount - 30, 0);

                _projectiles.Add(projectile.GetComponent<Projectile>());
            }
        }

        public void Shoot(Vector3 target)
        {
            var projectileIndex = GetProjectileClosestToPoint(target);
            _projectiles[projectileIndex].Shoot(target);
        }

        void Update()
        {
            // // Get all projectiles and rotate them around the parent
            foreach (Projectile projectile in _projectiles)
            {
                if (projectile.ShouldOrbit())
                {
                    projectile.transform.RotateAround(transform.position, Vector3.up, 20 * Time.deltaTime);
                }
            }
        }

        private int GetProjectileClosestToPoint(Vector3 hitInfoPoint)
        {
            // Get the children that is farthest away from the mouse
            var projectile = transform.GetChild(0);
            var projectileIndex = 0;
            var minDistance = Vector3.Distance(projectile.position, hitInfoPoint);
            for (int i = 0; i < transform.childCount; i++)
            {
                var child = transform.GetChild(i);
                var distance = Vector3.Distance(child.position, hitInfoPoint);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    projectile = child;
                    projectileIndex = i;
                }
            }

            return projectileIndex;
        }
    }
}