using Constants;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField]
        private GameObject backgroundMusic;

        private void Start()
        {
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

        public void Play() 
        {
            if (PlayerPrefs.HasKey(PlayerPrefsKeys.CurrentScene) 
                && PlayerPrefs.GetInt(PlayerPrefsKeys.CurrentScene) != (int) Scenes.MainMenuScene 
                && PlayerPrefs.GetInt(PlayerPrefsKeys.CurrentScene) != (int) Scenes.OptionsMenu 
                && PlayerPrefs.GetInt(PlayerPrefsKeys.CurrentScene) != (int) Scenes.GameOverMenu)
            {
                SceneManager.LoadScene(PlayerPrefs.GetInt(PlayerPrefsKeys.CurrentScene));
            } 
            else 
            {
                PlayerPrefs.SetInt(PlayerPrefsKeys.CurrentScene, (int) Scenes.Level1);
                SceneManager.LoadScene((int) Scenes.Level1);
            }
        }

        public void GoToOptions()
        {
            SceneManager.LoadScene((int) Scenes.OptionsMenu);
        }
        
        public void ExitGame()
        {
            Application.Quit();
        }
    }
}

