using System.Collections.Generic;
using Constants;
using Enemy;
using Projectiles;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using Evolution;

public class Player : MonoBehaviour
{
    [field: SerializeField] public float CurrentHealth { get; private set; } = 4;
    [field: SerializeField] public float MaxHealth { get; private set; } = 4;
    [field: SerializeField] public List<BulletType> UnlockedBulletTypes { get; private set; } = new();
    [field: SerializeField] public List<BulletType> AvailableBulletTypes { get; private set; } = new();
    [field: SerializeField] public BulletType CurrentBullet { get; set; }
    [field: SerializeField] public bool CanShoot { get; set; }

    [SerializeField] private HudManager ui;
    [SerializeField] private int scoreEvolve = 5;

    private PlayerStageData StageData => _evolution.Stage.PlayerData;

    private int _tempScore;
    private Evolvable _evolution;
    private ProjectileOrbitalController _orbitalController;
    private Coroutine _changeBulletCoroutine;

    private void Awake()
    {
        _orbitalController = GetComponent<ProjectileOrbitalController>();
        _evolution = GetComponent<Evolvable>();
        ui = GameObject.Find("HUD").GetComponent<HudManager>();
    }

    private void Start()
    {
        CheckHighScore();
        MaxHealth = StageData.Health;
        CurrentHealth = MaxHealth;

        _evolution.OnEvolution.AddListener(() => UpdateMaxHealth(StageData.Health));
    }

    private void Update()
    {
        CheckStatus();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.Enemy))
        {
            var enemy = other.GetComponent<AbstractEnemyController>();
            if(enemy != null)
            {
                TakeDamage(enemy.CollisionDamage);
            }
        }
    }

    private void CheckHighScore()
    {
        if (!PlayerPrefs.HasKey(PlayerPrefsKeys.HighScore))
        {
            PlayerPrefs.SetInt(PlayerPrefsKeys.HighScore, 0);
        }
    }

    public void AddScore(int val)
    {
        _tempScore += val;
        if (PlayerPrefs.GetInt(PlayerPrefsKeys.HighScore) < _tempScore)
        {
            PlayerPrefs.SetInt(PlayerPrefsKeys.HighScore, _tempScore);
        }

        ui.ScoreUI.UpdateScore(_tempScore, PlayerPrefs.GetInt(PlayerPrefsKeys.HighScore));
        if (_tempScore % scoreEvolve == 0)
        {
            _evolution.Evolve();
        }
    }

    public void TakeDamage(float dmg)
    {
        CurrentHealth -= dmg;
    }

    private void UpdateMaxHealth(float max)
    {
        if (CurrentHealth + (max - MaxHealth) > 0)
            CurrentHealth += max - MaxHealth;

        MaxHealth = max;
        ui.Hp.DrawHealth();
    }

    private void CheckStatus()
    {
        if (CurrentHealth <= 0)
        {
            PlayerPrefs.SetInt(PlayerPrefsKeys.CurrentScene, SceneManager.GetActiveScene().buildIndex);
            PlayerPrefs.Save();
            Destroy(gameObject);
            SceneManager.LoadScene((int)Scenes.GameOverMenu);
        }
    }

    public void SetBullet(BulletType bulletType)
    {
        CurrentBullet = bulletType;
        CanShoot = false;
        if (_changeBulletCoroutine != null)
        {
            StopCoroutine(_changeBulletCoroutine);
        }

        _changeBulletCoroutine =
            StartCoroutine(_orbitalController.ChangeBullet(
                bulletType,
                () => CanShoot = true)
            );
    }

    public void UnlockBulletsForLevel(int upToIndex)
    {
        for (var i = 0; i < upToIndex; i++)
        {
            UnlockedBulletTypes.Add(AvailableBulletTypes[i]);
        }
    }
}
