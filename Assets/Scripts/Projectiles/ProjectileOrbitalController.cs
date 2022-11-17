using System;
using System.Collections.Generic;
using UnityEngine;

namespace Projectiles
{
    public class ProjectileOrbitalController : MonoBehaviour
    {
        [SerializeField] private int projectileCount = 5;
        [SerializeField] private float radius = 0.3f;
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private float orbitSpeed = 35f;
        [SerializeField] private float orbitShootingSpeed = 70f;
        [SerializeField] private float launchDistanceThreshold = 0.5f;

        private Vector3 _prevPlayerPos;
        private readonly List<Projectile> _projectiles = new();
        private readonly List<ShootingInfo> _shootingQueue = new();


        private void Start()
        {
            _prevPlayerPos = transform.position;

            for (var i = 0; i < projectileCount; i++)
            {
                var projectileObj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                // projectile.transform.parent = transform;

                var projectile = projectileObj.GetComponent<Projectile>();
                projectile.Init(
                    new Vector3(
                        radius * Mathf.Cos(i * 2 * Mathf.PI / projectileCount),
                        Projectile.ProjectileHeight,
                        radius * Mathf.Sin(i * 2 * Mathf.PI / projectileCount)
                    ),
                    Quaternion.Euler(90, -i * 360 / projectileCount, 0)
                );
                _projectiles.Add(projectile);
            }
        }

        private void FixedUpdate()
        {
            var speed = _shootingQueue.Count > 0 ? orbitShootingSpeed : orbitSpeed;
            // // Get all projectiles and rotate them around the parent
            foreach (var projectile in _projectiles)
            {
                projectile.OrbitAround(transform.position, speed);
            }

            // Move the projectiles along with the player
            var playerPos = transform.position;
            var deltaPos = playerPos - _prevPlayerPos;
            foreach (var projectile in _projectiles)
            {
                projectile.UpdateProjectilePosition(deltaPos);
            }

            _prevPlayerPos = playerPos;
        }

        private void Update()
        {
            var removeList = new List<ShootingInfo>();
            foreach (var shootingInfo in _shootingQueue)
            {
                if (CanShoot(shootingInfo))
                {
                    shootingInfo.Projectile.Shoot(shootingInfo.TargetPosition);
                    removeList.Add(shootingInfo);
                }
            }

            foreach (var shootingInfo in removeList)
            {
                _shootingQueue.Remove(shootingInfo);
            }
        }

        public void EnqueueShoot(Vector3 target)
        {
            var projectileIndex = GetProjectileClosestToPoint(target);
            if (projectileIndex == -1)
            {
                return;
            }

            _shootingQueue.Add(new ShootingInfo(target, _projectiles[projectileIndex]));
            _projectiles[projectileIndex].QueuedForShooting = true;
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
                if (distance < minDistance && _projectiles[i].CanShoot())
                {
                    minDistance = distance;
                    projectileIndex = i;
                }
            }

            return projectileIndex;
        }

        private bool CanShoot(ShootingInfo shootingInfo)
        {
            var center = transform.position;
            var bulletPosition = shootingInfo.Projectile.GetBulletPosition();
            var slope = (shootingInfo.TargetPosition.z - bulletPosition.z) /
                        (shootingInfo.TargetPosition.x - bulletPosition.x);
            var run = -slope * bulletPosition.x +
                      bulletPosition.z;
            var distance = MathF.Abs(center.x * slope - center.y + run) / MathF.Sqrt(1 + slope * slope);
            
            return distance > radius - launchDistanceThreshold;
        }
    }
}