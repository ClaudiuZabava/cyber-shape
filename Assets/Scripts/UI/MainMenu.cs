using Constants;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            SceneManager.LoadScene((int) Scenes.PlayMenu);
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

