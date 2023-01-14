using System.Collections.Generic;
using Constants;
using Enemy;
using Projectiles;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using Evolution;
using static UnityEditor.Experimental.GraphView.GraphView;
using System.Collections;
using Scene = Constants.Scene;

public class Player : MonoBehaviour
{
    [field: SerializeField] public float CurrentHealth { get; set; } = 4;
    [field: SerializeField] public float MaxHealth { get; private set; } = 4;
    [field: SerializeField] public List<BulletType> UnlockedBulletTypes { get; private set; } = new();
    [field: SerializeField] public List<BulletType> AvailableBulletTypes { get; private set; } = new();
    [field: SerializeField] public BulletType CurrentBullet { get; set; }
    [field: SerializeField] public bool CanShoot { get; set; }
    [field: SerializeField] public bool CanTakeDamage { get; set; }
    [field: SerializeField] public bool DamageBuff { get; set; }

    [SerializeField] private HudManager ui;
    [SerializeField] private int scoreEvolve = 5;

    public int Score => _tempScore;

    private PlayerStageData StageData => _evolution.Stage.PlayerData;

    private int _tempScore;
    private Evolvable _evolution;
    private ProjectileOrbitalController _orbitalController;
    private Coroutine _changeBulletCoroutine;
    private Coroutine _damageBuffCoroutine;
    private Coroutine _shieldBuffCoroutine;
    private MeshRenderer _rend;

    private void Awake()
    {
        _orbitalController = GetComponent<ProjectileOrbitalController>();
        _evolution = GetComponent<Evolvable>();
        ui = GameObject.Find("HUD").GetComponent<HudManager>();
        _rend = GetComponent<MeshRenderer>();
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
            if (enemy != null)
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

        if (PlayerPrefs.GetInt(PlayerPrefsKeys.GameMode) == (int) GameMode.Endless)
        {
            PlayerPrefs.SetInt(PlayerPrefsKeys.SprintScore, _tempScore);
        }

        ui.ScoreUI.UpdateScore(_tempScore, PlayerPrefs.GetInt(PlayerPrefsKeys.HighScore));
        if (_tempScore % scoreEvolve == 0)
        {
            _evolution.Evolve();
        }
    }

    public void TakeDamage(float dmg)
    {
        if (CanTakeDamage)
        {
            CurrentHealth -= dmg;
        }
    }

    public void Heal(float hp)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth + hp, 0, MaxHealth);
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
            if (PlayerPrefs.GetInt(PlayerPrefsKeys.GameMode) == (int) GameMode.Classic)
            {
                PlayerPrefs.SetInt(PlayerPrefsKeys.CurrentScene, SceneManager.GetActiveScene().buildIndex);
            }
            else // Endless
            {
                PlayerPrefs.SetInt(PlayerPrefsKeys.LastLevel, SceneManager.GetActiveScene().buildIndex);
                EndlessState.Clear();
            }

            PlayerPrefs.Save();
            Destroy(gameObject);
            SceneManager.LoadScene((int) Scene.GameOverMenu);
        }
    }

    public void DamageBuffEffect()
    {
        DamageBuff = true;
        _rend.material.SetColor("_EmissionColor", Colors.PlayerBullet);

        if (_damageBuffCoroutine != null)
        {
            StopCoroutine(_damageBuffCoroutine);
        }

        _damageBuffCoroutine = StartCoroutine(CancelEffectDamage());
    }

    public void ShieldBuffEffect()
    {
        CanTakeDamage = false;
        _rend.material.SetColor("_EmissionColor", Colors.PlayerShield);
        if (_shieldBuffCoroutine != null)
        {
            StopCoroutine(_shieldBuffCoroutine);
        }

        _shieldBuffCoroutine = StartCoroutine(CancelEffectShield());
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

    private IEnumerator CancelEffectDamage()
    {
        yield return new WaitForSeconds(2.5f);
        DamageBuff = false;
        _rend.material.SetColor("_EmissionColor", Colors.PlayerBlue);
        _damageBuffCoroutine = null;
        yield return null;
    }

    private IEnumerator CancelEffectShield()
    {
        yield return new WaitForSeconds(5f);
        CanTakeDamage = true;
        _rend.material.SetColor("_EmissionColor", Colors.PlayerBlue);
        _shieldBuffCoroutine = null;
        yield return null;
    }

    public void UnlockBulletsForLevel(int upToIndex)
    {
        for (var i = 0; i < upToIndex; i++)
        {
            UnlockedBulletTypes.Add(AvailableBulletTypes[i]);
        }
    }
}