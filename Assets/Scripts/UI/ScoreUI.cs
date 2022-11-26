using Constants;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ScoreUI : MonoBehaviour
    {
        private Text _scoreText;

        private void Awake()
        {
            _scoreText = GetComponent<Text>();
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
            _scoreText.text = $"Score: {newScore}\nHigh score: {highScore}";
        }
    }
}
