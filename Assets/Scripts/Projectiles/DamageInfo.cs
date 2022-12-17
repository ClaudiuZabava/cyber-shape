using System;
using UnityEngine;

namespace Projectiles
{
    [Serializable]
    public class DamageInfo
    {
        [field: SerializeField] public float ContactDamage { get; private set; }
        [field: SerializeField] public float SplashDamage { get; private set; }
        [field: SerializeField] public float SplashArea { get; private set; }
        [field: SerializeField] public bool Piercing { get; private set; }

        public bool Splashes => SplashArea != 0 && SplashDamage != 0;
    }
}