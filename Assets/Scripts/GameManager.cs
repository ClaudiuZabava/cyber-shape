using System.Collections;
using Constants;
using UI;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;

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

    [Header("Level settings")]
    [SerializeField] private int numberOfWaves;

    private bool _spawning = false;
    private int _pause  = 0;
    private AudioSource _backgroundMusic;
    private int _waveCount = 0;
    private HudManager _ui;

    private void Awake()
    {
        _ui = GameObject.Find("HUD").GetComponent<HudManager>();
    }

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

        NextWave();
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
        if (GameObject.FindGameObjectsWithTag(Tags.Enemy).Length < percentToNextWave * constEnemies)
        {
            NextWave();
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
                if (Physics.CheckSphere(randomPosition, 0.7f, (int) Layers.Floor))
                {
                    continue;
                }
            }

            return randomPosition;
        }
    }

    private void NextWave()
    {
        _waveCount++;
        _ui.WavesUI.UpdateWaves(numberOfWaves - _waveCount + 1);
        if (_waveCount > numberOfWaves)
        {
            ProgressToNextLevel();
        }
        else
        {
            StartCoroutine(SpawnEnemies());
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

    private void ProgressToNextLevel()
    {
        var activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (activeSceneIndex < (int) Scenes.Level2) // TODO: Replace Level2 with whatever will be the last level
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    private void PauseGame()
    {
        _pause = 1;
        PlayerPrefs.SetInt(PlayerPrefsKeys.GamePause, _pause);
        Time.timeScale = 0f;
        panel.SetActive(true);
    }
    
    public void ResumeGame()
    {
        _pause = 0;
        PlayerPrefs.SetInt(PlayerPrefsKeys.GamePause, _pause);
        Time.timeScale = 1f;
        panel.SetActive(false);
    }

    public void GoBackMain()
    {
        _pause = 0;
        PlayerPrefs.SetInt(PlayerPrefsKeys.GamePause, _pause);
        Time.timeScale = 1f;
        panel.SetActive(false);
        SceneManager.LoadScene((int) Scenes.MainMenuScene);
    }
}
