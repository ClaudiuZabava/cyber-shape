using Constants;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class MainMenu : MonoBehaviour
    {
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

