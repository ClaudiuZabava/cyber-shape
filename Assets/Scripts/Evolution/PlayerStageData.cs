using System;
using UnityEngine;

namespace Evolution
{
    [Serializable]
    public class PlayerStageData
    {
        [field: SerializeField] public int Health { get; private set; }
    }
}