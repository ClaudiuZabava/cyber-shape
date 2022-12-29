using System.Collections;
using Constants;
using Evolution;
using UI;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private bool withEnemyWaves = true;
    [SerializeField] private Texture2D crosshairImg;
    [SerializeField] private GameObject panel;

    [Header("Enemy settings")]
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float maxWidth;
    [SerializeField] private float secondsTillSpawn = 3f;
    [SerializeField] private float percentToNextWave = .75f;

    [Header("Level settings")]
    [SerializeField] private int waves = 3;
    [SerializeField] private int enemiesPerWave = 3;
    [SerializeField]
    private int level = 1;

    private bool _spawning = false;
    private int _pause = 0;
    private int _waveCount = 0;
    private HudManager _ui;
    private Player _player;
    private Vector3 _playerPosition;
    private Vector3 _floorSize;
    private Camera _camera;
    private int _levelIndex;

    private void Awake()
    {
        _levelIndex = SceneManager.GetActiveScene().buildIndex - (int) Scenes.Level1 + 1;

        _ui = GameObject.Find("HUD").GetComponent<HudManager>();
        _player = GetComponentInChildren<Player>();
        _player.UnlockBulletsForLevel(_levelIndex);
        _floorSize = GameObject.FindWithTag("Floor").GetComponent<MeshRenderer>().bounds.size;
        maxWidth = _floorSize.x / 3; 
        _camera = Camera.main;
    }

    private void Start()
    {
        PlayerPrefs.SetInt(PlayerPrefsKeys.GamePause, _pause);

        var hotSpot = new Vector2(crosshairImg.width / 2f, crosshairImg.height / 2f);
        Cursor.SetCursor(crosshairImg, hotSpot, CursorMode.Auto);

        if (withEnemyWaves)
        {
            NextWave();
        }
    }

    private void Update()
    {
        _playerPosition = _player.transform.position;
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

        if (!_spawning && withEnemyWaves)
        {
            CheckNoEnemies();
        }
    }

    private void CheckNoEnemies()
    {
        if (GameObject.FindGameObjectsWithTag(Tags.Enemy).Length < percentToNextWave * enemiesPerWave)
        {
            NextWave();
        }
    }

    private Vector3 GetRandomPosition()
    {
        
        while (true)
        {
            // get random point around the player
            var randomPosition =
                new Vector3(Random.Range(_playerPosition.x - maxWidth, _playerPosition.x + maxWidth), 0.5f, 
                    Random.Range(_playerPosition.z - maxWidth, _playerPosition.z + maxWidth));
            
            // check if the point is inside the map
            if (randomPosition.x < -_floorSize.x / 2 || randomPosition.x > _floorSize.x / 2 ||
                randomPosition.z < -_floorSize.z / 2 || randomPosition.z > _floorSize.z / 2)
            {
                continue;
            }
            
            // check if the point is outside camera view
            if (randomPosition.x - _playerPosition.x < _camera.orthographicSize ||
                randomPosition.z - _playerPosition.z < _camera.orthographicSize * _camera.aspect)
            {
                continue;
            }

            if (Physics.CheckSphere(randomPosition, 0.7f, 7))
            {
                continue;
            }

            return randomPosition;
        }
    }

    private void NextWave()
    {
        if (_waveCount + 1 > waves)
        {
            ProgressToNextLevel();
        }
        else
        {
            _waveCount++;
            _ui.WavesUI.UpdateWaves(waves - _waveCount + 1);
            StartCoroutine(SpawnEnemies());
        }
    }

    private IEnumerator SpawnEnemies()
    {
        _spawning = true;
        yield return new WaitForSeconds(secondsTillSpawn);
        for (var i = 0; i < enemiesPerWave; i++)
        {
            var enemyObject = Instantiate(enemyPrefab, GetRandomPosition(), Quaternion.identity);
            var enemyEvolvable = enemyObject.GetComponentInChildren<Evolvable>();
            var stageIndex = Random.Range(0, _levelIndex);
            enemyEvolvable.Evolve(stageIndex);
        }

        _spawning = false;
    }

    private void ProgressToNextLevel()
    {
        var activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (activeSceneIndex < (int) Scenes.Level5)
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
        SceneManager.LoadScene((int)Scenes.MainMenuScene);
    }
}