using Constants;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Scene = Constants.Scene;

namespace UI
{
    public class GameOver : MonoBehaviour
    {
        [SerializeField] private TMP_Text scoreText;

        private void Start()
        {
            if (PlayerPrefs.GetInt(PlayerPrefsKeys.GameMode) == (int) GameMode.Classic)
            {
                if (PlayerPrefs.HasKey(PlayerPrefsKeys.HighScore))
                {
                    scoreText.text = "High Score: " + PlayerPrefs.GetInt(PlayerPrefsKeys.HighScore);
                }
                else
                {
                    scoreText.text = "High Score: 0";
                }
            }
            else
            {
                if (PlayerPrefs.HasKey(PlayerPrefsKeys.SprintScore))
                {
                    scoreText.text = "Round Score: " + PlayerPrefs.GetInt(PlayerPrefsKeys.SprintScore);
                }
                else
                {
                    scoreText.text = "Round Score: 0";
                }
            }
        }

        public void RestartLevel()
        {
            if (PlayerPrefs.GetInt(PlayerPrefsKeys.GameMode) == (int) GameMode.Classic)
            {
                if (PlayerPrefs.HasKey(PlayerPrefsKeys.CurrentScene))
                {
                    SceneManager.LoadScene(PlayerPrefs.GetInt(PlayerPrefsKeys.CurrentScene));
                }
                else
                {
                    SceneManager.LoadScene((int) Scene.Level1);
                }
            }
            else // Endless
            {
                if (PlayerPrefs.HasKey(PlayerPrefsKeys.LastLevel))
                {
                    SceneManager.LoadScene(PlayerPrefs.GetInt(PlayerPrefsKeys.LastLevel));
                }
                else
                {
                    SceneManager.LoadScene((int) Scene.Level1);
                }
            }
        }

        public void MainMenu()
        {
            SceneManager.LoadScene((int) Scene.MainMenuScene);
        }

        public void ExitGame()
        {
            Application.Quit();
        }
    }
}