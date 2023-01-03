using Constants;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

namespace UI
{
    public class LevelSelectorMenu : MonoBehaviour
    {
        [SerializeField]
        private GameObject err;
        public void LoadLevel1()
        {
            if(PlayerPrefs.GetInt(PlayerPrefsKeys.GameProgress) < 20)
            {
                StartCoroutine(ShowMessage());
            }
            else
            {
                PlayerPrefs.SetInt(PlayerPrefsKeys.LastLevel, (int) Scenes.Level1);
                SceneManager.LoadScene((int) Scenes.Level1);
            }
        }

        public void LoadLevel2()
        {
            if(PlayerPrefs.GetInt(PlayerPrefsKeys.GameProgress) < 40)
            {
                StartCoroutine(ShowMessage());
            }
            else
            {
                PlayerPrefs.SetInt(PlayerPrefsKeys.LastLevel, (int) Scenes.Level2);
                SceneManager.LoadScene((int) Scenes.Level2);
            }
        }

        public void LoadLevel3()
        {
            if(PlayerPrefs.GetInt(PlayerPrefsKeys.GameProgress) < 60)
            {
                StartCoroutine(ShowMessage());
            }
            else
            {
                PlayerPrefs.SetInt(PlayerPrefsKeys.LastLevel, (int) Scenes.Level3);
                SceneManager.LoadScene((int) Scenes.Level3);
            }
        }

        public void LoadLevel4()
        {
            if(PlayerPrefs.GetInt(PlayerPrefsKeys.GameProgress) < 80)
            {
                StartCoroutine(ShowMessage());
            }
            else
            {
                PlayerPrefs.SetInt(PlayerPrefsKeys.LastLevel, (int) Scenes.Level4);
                SceneManager.LoadScene((int) Scenes.Level4);
            }
        }

        public void Back()
        {
            SceneManager.LoadScene((int) Scenes.PlayMenu);
        }

        IEnumerator ShowMessage () 
        {
            err.SetActive(true);
            yield return new WaitForSeconds(3.0f);
            err.SetActive(false);
        }
    }


}
