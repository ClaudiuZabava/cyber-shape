using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Projectiles
{
    public class ProjectileOrbitalController : MonoBehaviour
    {
        public int projectileCount = 5;
        public float radius = 0.3f;
        public GameObject projectilePrefab;

        private readonly List<Projectile> _projectiles = new();

        private void Start()
        {
            for (int i = 0; i < projectileCount; i++)
            {
                var projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                projectile.transform.parent = transform;

                var projectileScript = projectile.GetComponent<Projectile>();
                projectileScript.Init(
                    new Vector3(
                        radius * Mathf.Cos(i * 2 * Mathf.PI / projectileCount),
                        Projectile.ProjectileHeight,
                        radius * Mathf.Sin(i * 2 * Mathf.PI / projectileCount)
                    ),
                    Quaternion.Euler(90, -i * 360 / projectileCount, 0),
                    -i * 360 / projectileCount
                );
                _projectiles.Add(projectileScript);
            }
        }

        public void Shoot(Vector3 target)
        {
            var projectileIndex = GetProjectileClosestToPoint(target);
            if (projectileIndex == -1)
            {
                return;
            }

            _projectiles[projectileIndex].Shoot(target);
        }

        private void FixedUpdate()
        {
            // // Get all projectiles and rotate them around the parent
            foreach (Projectile projectile in _projectiles)
            {
                projectile.OrbitAround(transform.position);
            }
        }

        private int GetProjectileClosestToPoint(Vector3 hitInfoPoint)
        {
            // Get the children that is farthest away from the mouse
            var projectileIndex = -1;
            var minDistance = Mathf.Infinity;
            for (int i = 0; i < projectileCount; i++)
            {
                var bulletPosition = _projectiles[i].GetBulletPosition();
                var distance = Vector3.Distance(bulletPosition, hitInfoPoint);
                // Ignore projectiles that have already been fired
                if (distance < minDistance && _projectiles[i].ShouldOrbit())
                {
                    minDistance = distance;
                    projectileIndex = i;
                }
            }

            return projectileIndex;
        }
    }
}