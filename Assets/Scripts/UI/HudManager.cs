using UnityEngine;

namespace UI
{
    public class HudManager : MonoBehaviour
    {
        public PlayerHealth Hp { get; private set; }

        private void Awake()
        {
            Hp = GetComponentInChildren<PlayerHealth>();
        }
    }
}
