using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;
using Animations = Constants.Animations;

namespace Projectiles
{
    public class ProjectileOrbitalController : MonoBehaviour
    {
        [SerializeField] private int projectileCount = 5;
        [SerializeField] private float radius = 0.3f;
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private float orbitSpeed = 10f;
        [SerializeField] private float orbitShootingSpeed = 25f;
        [SerializeField] private float launchDistanceThreshold = 0.5f;

        private Vector3 _prevPlayerPos;
        private readonly List<Projectile> _projectiles = new();
        private readonly List<ShootingInfo> _shootingQueue = new();
        private readonly List<MeshFilter> _projectileMeshFilters = new();
        private readonly List<Animator> _projectileAnimators = new();
        private readonly List<MeshCollider> _projectileColliders = new();

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
                _projectileMeshFilters.Add(
                    projectileObj.GetComponentInChildren<MeshFilter>()
                );
                _projectileAnimators.Add(projectileObj.GetComponent<Animator>());
                _projectileColliders.Add(projectileObj.GetComponentInChildren<MeshCollider>());
            }
        }

        private void Update()
        {
            var removeList = new List<ShootingInfo>();
            foreach (var shootingInfo in _shootingQueue.Where(CanShoot))
            {
                shootingInfo.Projectile.Shoot(shootingInfo.TargetPosition);
                removeList.Add(shootingInfo);
            }

            foreach (var shootingInfo in removeList)
            {
                _shootingQueue.Remove(shootingInfo);
            }
        }

        private void FixedUpdate()
        {
            var playerPos = transform.position;
            var deltaPos = playerPos - _prevPlayerPos;
            foreach (var projectile in _projectiles)
            {
                projectile.UpdateProjectilePosition(deltaPos);
            }

            _prevPlayerPos = playerPos;
            var speed = _shootingQueue.Count > 0 ? orbitShootingSpeed : orbitSpeed;
            // Get all projectiles and rotate them around the parent
            foreach (var projectile in _projectiles)
            {
                projectile.OrbitAround(playerPos, speed);
            }
        }

        public IEnumerator ChangeBullet(BulletType bulletType, Action then)
        {
            foreach (var animator in _projectileAnimators)
            {
                animator.SetTrigger(Animations.Bullets.Triggers.ChangeType);
            }

            var animationLength = _projectileAnimators.First().GetCurrentAnimatorStateInfo(0).length;
            yield return new WaitForSeconds(animationLength / 2);

            for (var i = 0; i < _projectiles.Count; i++)
            {
                _projectileMeshFilters[i].mesh = bulletType.mesh;
                _projectileColliders[i].sharedMesh = bulletType.mesh;
            }

            then?.Invoke();

            yield return null;
        }

        public void EnqueueShoot(Vector3 target, int damage)
        {
            var projectileIndex = GetProjectileClosestToPoint(target);
            if (projectileIndex == -1)
            {
                return;
            }

            _shootingQueue.Add(new ShootingInfo(target, _projectiles[projectileIndex]));
            switch(damage)
            {
                case 25: _projectiles[projectileIndex].ChangeBulletColor(new Color(0.0037f, 0.0352f, 0f), new Color(0, 1, 0.094f)); break;
                case 50: _projectiles[projectileIndex].ChangeBulletColor(new Color(0.0342f, 0.0352f, 0f), new Color(16, 15.56f, 0)); break;
            }

            _projectiles[projectileIndex].QueuedForShooting = true;
            _projectiles[projectileIndex].Damage = damage;
        }

        private int GetProjectileClosestToPoint(Vector3 hitInfoPoint)
        {
            // Get the children that is farthest away from the mouse
            var projectileIndex = -1;
            var minDistance = Mathf.Infinity;
            for (var i = 0; i < projectileCount; i++)
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
            var center2 = new Vector2(center.x, center.z);
            var bulletPosition = shootingInfo.Projectile.GetBulletPosition();
            var bulletPosition2 = new Vector2(bulletPosition.x, bulletPosition.z);
            var targetPosition2 = new Vector2(shootingInfo.TargetPosition.x, shootingInfo.TargetPosition.z);

            var normalizedDirection = (targetPosition2 - bulletPosition2).normalized;
            var lineToCenter = center2 - bulletPosition2;

            return Mathf.Abs(normalizedDirection.x * lineToCenter.y - normalizedDirection.y * lineToCenter.x) >
                   radius - launchDistanceThreshold;
        }
    }
}