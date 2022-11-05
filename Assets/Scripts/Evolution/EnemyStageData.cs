using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Evolution
{
    [Serializable]
    public class EnemyStageData
    {
        [field: SerializeField] public int Health { get; private set; }
    };
}