using UnityEngine;

namespace Enemy
{
    public abstract class AbstractEnemyController : MonoBehaviour
    {
        public abstract void TakeDamage(float damage);
    }
}