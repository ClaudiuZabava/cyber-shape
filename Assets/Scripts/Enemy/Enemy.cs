using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

public class Enemy : MonoBehaviour
{
    public int rank = 1;
    private EnemyHealthBar _healthBar;

    private int _health;

    // Start is called before the first frame update
    private void Start()
    {
        _healthBar = GetComponentInChildren<EnemyHealthBar>();
        _health = ScalingUtils.GetFibonacci(rank + 1);
        _healthBar.SetMaxHealth(_health);
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        _healthBar.SetHealth(_health);
    }
}