using Constants;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class PlayMenu : MonoBehaviour
    {
        public void PlayNormal()
        {
            PlayerPrefs.SetInt(PlayerPrefsKeys.GameMode, 0);
            if (PlayerPrefs.HasKey(PlayerPrefsKeys.CurrentScene) 
                && PlayerPrefs.GetInt(PlayerPrefsKeys.CurrentScene) != (int) Scenes.MainMenuScene 
                && PlayerPrefs.GetInt(PlayerPrefsKeys.CurrentScene) != (int) Scenes.OptionsMenu 
                && PlayerPrefs.GetInt(PlayerPrefsKeys.CurrentScene) != (int) Scenes.GameOverMenu
                && PlayerPrefs.GetInt(PlayerPrefsKeys.CurrentScene) != (int) Scenes.PlayMenu
                && PlayerPrefs.GetInt(PlayerPrefsKeys.CurrentScene) != (int) Scenes.LevelSelector)
            {
                SceneManager.LoadScene(PlayerPrefs.GetInt(PlayerPrefsKeys.CurrentScene));
            } 
            else 
            {
                PlayerPrefs.SetInt(PlayerPrefsKeys.CurrentScene, (int) Scenes.Level1);
                SceneManager.LoadScene((int) Scenes.Level1);
            }
        }

        public void PlayEndless()
        {
            PlayerPrefs.SetInt(PlayerPrefsKeys.GameMode, 1);
            SceneManager.LoadScene((int) Scenes.LevelSelector);
        }

        public void Back()
        {
            PlayerPrefs.SetInt(PlayerPrefsKeys.GameMode, 0);
            SceneManager.LoadScene((int) Scenes.MainMenuScene);
        }
    }
    
}

