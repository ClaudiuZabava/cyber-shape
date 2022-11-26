using Constants;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class GameOver : MonoBehaviour
    {
        private void Start()
        {
            // Aici o sa vina o mica afisare a scorului. Nu HighScore ci scorul de dinainte sa moare
            // Daca am salvat in PlayerPrefs putem folosi urmatoarea linie pt a afisa scorul:
            // scoreText.text = "Your HighScore\n" + PlayerPrefs.GetFloat("highScore");
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
        
        public void ExitGame()
        {
            Application.Quit();
        }
    }
}
