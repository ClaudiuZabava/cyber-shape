using UnityEngine;

namespace UI
{
    public class HudManager : MonoBehaviour
    {
        public PlayerHealth Hp { get; private set; }
        public RhythmUI Rh { get; private set; }

        private void Awake()
        {
            Hp = GetComponentInChildren<PlayerHealth>();
            Rh = GetComponentInChildren<RhythmUI>();
        }
    }
}
