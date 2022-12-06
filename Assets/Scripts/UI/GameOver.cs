using Constants;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

namespace UI
{
    public class GameOver : MonoBehaviour
    {
        [SerializeField] private TMP_Text scoreText;

        private void Start()
        {
            scoreText.text = "High Score: " + PlayerPrefs.GetInt(PlayerPrefsKeys.HighScore);
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
