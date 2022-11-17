using UnityEngine;

namespace Projectiles
{
    public class ShootingInfo
    {
        public Vector3 TargetPosition { get; set; }
        public Projectile Projectile { get; set; }
        
        public ShootingInfo(Vector3 targetPosition, Projectile projectile)
        {
            TargetPosition = targetPosition;
            Projectile = projectile;
        }
    }
}