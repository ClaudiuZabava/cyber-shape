using Constants;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

namespace UI
{
    public class GameOver : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text scoreText;
        [SerializeField]
        private GameObject backgroundMusic;

        private void Start()
        {
            scoreText.text = "High Score: " + PlayerPrefs.GetInt(PlayerPrefsKeys.HighScore);
            if(!PlayerPrefs.HasKey(PlayerPrefsKeys.MusicVolume))
            {
                PlayerPrefs.SetFloat(PlayerPrefsKeys.MusicVolume, 0.5f);
            }

            if (PlayerPrefs.HasKey(PlayerPrefsKeys.MusicState))
            {
                if(PlayerPrefs.GetInt(PlayerPrefsKeys.MusicState) == 1)
                {
                    backgroundMusic.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat(PlayerPrefsKeys.MusicVolume);
                }
                else
                {
                    backgroundMusic.GetComponent<AudioSource>().volume = 0.0f;
                }
            }
            else
            {
                PlayerPrefs.SetInt(PlayerPrefsKeys.MusicState, 1);
                backgroundMusic.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat(PlayerPrefsKeys.MusicVolume);
            }
        }

        public void RestartLevel() 
        {
            if (PlayerPrefs.HasKey(PlayerPrefsKeys.CurrentScene)) 
            {
                SceneManager.LoadScene(PlayerPrefs.GetInt(PlayerPrefsKeys.CurrentScene));
            } 
            else 
            {
                SceneManager.LoadScene((int) Scenes.Level1);
            }
        }

        public void MainMenu()
        {
            SceneManager.LoadScene((int) Scenes.MainMenuScene);
        }
        
        public void ExitGame()
        {
            Application.Quit();
        }
    }
}
