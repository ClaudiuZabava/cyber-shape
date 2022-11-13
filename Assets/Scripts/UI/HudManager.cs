using UnityEngine;

namespace UI
{
    public class HudManager : MonoBehaviour
    {
        public PlayerHealth hp;

        private void Awake()
        {
            hp = GameObject.Find("PlayerHealth").GetComponent<PlayerHealth>();
        }
    }
}
