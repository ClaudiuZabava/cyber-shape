using UnityEngine;
using UnityEngine.UI;

namespace Enemy
{
    public class EnemyHealthBar : MonoBehaviour
    {
        private int _maxHealth;
        private Slider _slider;

        private void Awake()
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
}