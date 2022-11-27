using UnityEngine;

namespace Projectiles
{
    [CreateAssetMenu(fileName = "BulletType", menuName = "Scriptable Objects/Bullet Type")]
    public class BulletType : ScriptableObject
    {
        public Sprite sprite;
        public Mesh mesh;
    }
}