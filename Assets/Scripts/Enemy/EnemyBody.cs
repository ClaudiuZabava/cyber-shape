using UnityEngine;

public class EnemyBody : MonoBehaviour
{
    private Enemy enemy;

    private void Start()
    {
        enemy = GetComponentInParent<Enemy>();
    }

    public void TakeDamage(int damage)
    {
        enemy.TakeDamage(damage);
    }
}