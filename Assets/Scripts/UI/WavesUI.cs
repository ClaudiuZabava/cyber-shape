using Constants;
using TMPro;
using UnityEngine;

namespace UI
{
    public class WavesUI : MonoBehaviour
    {
        private TextMeshProUGUI _wavesText;

        private void Awake()
        {
            _wavesText = GetComponent<TextMeshProUGUI>();
        }

        public void UpdateWaves(int remainingWaves)
        {
            _wavesText.text = $"Remaining Waves: {remainingWaves}";
        }
    }
}
