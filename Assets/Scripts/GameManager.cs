using System;
using System.Collections;
using Constants;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Texture2D crosshairImg;
    [SerializeField] private GameObject panel;
    
    [Header("Enemy settings")]
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int constEnemies = 7;
    [SerializeField] private int maxWidth = 10;
    [SerializeField] private int maxDistance = 5;
    [SerializeField] private float secondsTillSpawn = 3f;
    [SerializeField] private float percentToNextWave = .75f;

    private bool _spawning = false;
    private int _pause  = 0;
    private AudioSource _backgroundMusic;

    private void Start()
    {
        PlayerPrefs.SetInt(PlayerPrefsKeys.GamePause, _pause);

        var hotSpot = new Vector2(crosshairImg.width / 2f, crosshairImg.height / 2f);
        Cursor.SetCursor(crosshairImg, hotSpot, CursorMode.Auto);
        _backgroundMusic = GetComponent<AudioSource>();

        if (PlayerPrefs.HasKey(PlayerPrefsKeys.MusicState))
        {
            _backgroundMusic.volume = PlayerPrefs.GetFloat(PlayerPrefsKeys.MusicState);
        }
        else
        {
            PlayerPrefs.SetFloat(PlayerPrefsKeys.MusicState, 0.5f);
            _backgroundMusic.volume = 0.5f;
        }

        StartCoroutine(SpawnEnemies());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_pause == 0)
            {
                PauseGame();
            } 
            else
            {
                ResumeGame();
            }
        }

        if (!_spawning)
        {
            CheckNoEnemies();
        }
    }

    private void CheckNoEnemies()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length < percentToNextWave * constEnemies)
        {
            StartCoroutine(SpawnEnemies());
        }
    }
    
    private Vector3 GetRandomPosition()
    {
        while (true)
        {
            var randomPosition = new Vector3(Random.Range(-maxWidth, maxWidth), 0.5f, Random.Range(-maxWidth, maxWidth));
            var distance = Vector3.Distance(transform.position, randomPosition);
            if (distance < maxDistance)
            {
                continue;
            }

            return randomPosition;
        }
    }

    private IEnumerator SpawnEnemies()
    {
        _spawning = true;
        yield return new WaitForSeconds(secondsTillSpawn);
        for (var i = 0; i < constEnemies; i++)
        {
            Instantiate(enemyPrefab, GetRandomPosition(), Quaternion.identity);
        }

        _spawning = false;
    }

    private void PauseGame()
    {
        _pause = 1;
        PlayerPrefs.SetInt(PlayerPrefsKeys.GamePause,_pause);
        Time.timeScale = 0f;
        this.panel.SetActive(true);
    }
    
    public void ResumeGame()
    {
        _pause = 0;
        PlayerPrefs.SetInt(PlayerPrefsKeys.GamePause,_pause);
        Time.timeScale = 1f;
        panel.SetActive(false);
    }

    public void GoBackMain()
    {
        _pause = 0;
        PlayerPrefs.SetInt(PlayerPrefsKeys.GamePause,_pause);
        Time.timeScale = 1f;
        panel.SetActive(false);
        SceneManager.LoadScene((int) Scenes.MainMenuScene);
    }
}
