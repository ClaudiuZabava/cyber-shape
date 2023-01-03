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
            if(PlayerPrefs.GetInt(PlayerPrefsKeys.GameMode) == 0)
            {
                _wavesText.text = $"Remaining Waves: {remainingWaves}";
            }
            else if(PlayerPrefs.GetInt(PlayerPrefsKeys.GameMode) == 1)
            {
                _wavesText.text = $"Endless Mode Active";
            }

        }
    }
}
