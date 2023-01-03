using Constants;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ScoreUI : MonoBehaviour
    {
        private TextMeshProUGUI _scoreText;

        private void Awake()
        {
            _scoreText = GetComponent<TextMeshProUGUI>();
        }

        private void Start()
        {
            if (PlayerPrefs.HasKey(PlayerPrefsKeys.HighScore))
            {
                UpdateScore(0, PlayerPrefs.GetInt(PlayerPrefsKeys.HighScore));
            }
            else
            {
                UpdateScore(0, 0);
            }
        }

        public void UpdateScore(int newScore, int highScore)
        {
            if( PlayerPrefs.GetInt(PlayerPrefsKeys.GameMode) == 0)
            {
                _scoreText.text = $"Score: {newScore}\nHighScore: {highScore}";
            }
            else if(PlayerPrefs.GetInt(PlayerPrefsKeys.GameMode) == 1)
            {
                _scoreText.text = $"Score: {newScore}";
            }
        }
    }
}
