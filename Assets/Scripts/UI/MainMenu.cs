using Constants;
using UnityEngine;
using UnityEngine.SceneManagement;
using Scene = Constants.Scene;

namespace UI
{
    public class MainMenu : MonoBehaviour
    {
        private void Start()
        {
            PlayerPrefs.SetInt(PlayerPrefsKeys.GameMode, (int) GameMode.Classic);
            if(!PlayerPrefs.HasKey(PlayerPrefsKeys.GameProgress))
            {
                PlayerPrefs.SetInt(PlayerPrefsKeys.GameProgress, 0);
            }
        }
        public void Play()
        {
            SceneManager.LoadScene((int) Scene.PlayMenu);
        }

        public void GoToOptions()
        {
            SceneManager.LoadScene((int) Scene.OptionsMenu);
        }
        
        public void ExitGame()
        {
            Application.Quit();
        }
    }
}

