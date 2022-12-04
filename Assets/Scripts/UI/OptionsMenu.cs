using Constants;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class OptionsMenu : MonoBehaviour
    {
        [SerializeField]
        private GameObject backgroundMusic;
        [SerializeField]
        private Slider musicVolume;

        private void Start()
        {
            if(!PlayerPrefs.HasKey(PlayerPrefsKeys.MusicVolume))
            {
                PlayerPrefs.SetFloat(PlayerPrefsKeys.MusicVolume, 0.5f);
            }
            musicVolume.value = PlayerPrefs.GetFloat(PlayerPrefsKeys.MusicVolume);

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
        
        private void Update()
        {
            PlayerPrefs.SetFloat(PlayerPrefsKeys.MusicVolume, musicVolume.value);
            if(PlayerPrefs.GetInt(PlayerPrefsKeys.MusicState) == 1)
            {
                backgroundMusic.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat(PlayerPrefsKeys.MusicVolume);
            }
            else
            {
                backgroundMusic.GetComponent<AudioSource>().volume = 0.0f;
            }
        }

        public void OnMusic()
        {
            PlayerPrefs.SetInt(PlayerPrefsKeys.MusicState, 1);
            backgroundMusic.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat(PlayerPrefsKeys.MusicVolume);
        }

        public void OffMusic()
        {
            PlayerPrefs.SetFloat(PlayerPrefsKeys.MusicState, 0);
            PlayerPrefs.SetFloat(PlayerPrefsKeys.MusicVolume, 0.0f);
            backgroundMusic.GetComponent<AudioSource>().volume =0.0f;
        }

        public void Back()
        {
            SceneManager.LoadScene((int) Scenes.MainMenuScene);
        }

        /// Pentru cand vom implementa si sunetele gloantelor si ale transformarilor ( Al doilea buton de optiuni):
        // public void OnSfx()
        // public void OffSfx()
    }
}
