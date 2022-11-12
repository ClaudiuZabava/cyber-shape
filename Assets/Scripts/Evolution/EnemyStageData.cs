using System;
using UnityEngine;

namespace Evolution
{
    [Serializable]
    public class EnemyStageData
    {
        [field: SerializeField] public int Health { get; private set; }
    };
}