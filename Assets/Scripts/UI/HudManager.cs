using UnityEngine;

namespace UI
{
    public class HudManager : MonoBehaviour
    {
        public PlayerHealth Hp { get; private set; }
        public ScoreUI ScoreUI { get; private set; }

        private void Awake()
        {
            Hp = GetComponentInChildren<PlayerHealth>();
            ScoreUI = GetComponentInChildren<ScoreUI>();
        }
    }
}
