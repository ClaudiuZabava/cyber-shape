using System;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    private int _maxHealth;
    private Slider _slider;

    private void Start()
    {
        _slider = GetComponentInChildren<Slider>();
    }

    public void SetMaxHealth(int maxHealth)
    {
        _maxHealth = maxHealth;
    }

    public void SetHealth(int health)
    {
        _slider.value = (float)health / _maxHealth;
    }
}