using UnityEngine;

namespace Projectiles
{
    [CreateAssetMenu(fileName = "BulletType", menuName = "BulletType")]
    public class BulletType : ScriptableObject
    {
        public Sprite sprite;
        public Mesh mesh;
    }
}