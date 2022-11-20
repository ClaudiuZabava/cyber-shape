using Constants;
using Projectiles;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using Evolution;

public class Player : MonoBehaviour
{
    [field: SerializeField] public int CurrentHealth { get; private set; } = 4;
    [field: SerializeField] public int MaxHealth { get; private set; } = 4;

    [field: SerializeField] public bool IsRhythmActive { get; private set; } = true;

    [field: SerializeField] public int LevelScore { get; private set; } = 0;

    [SerializeField] private float speed = 5.0f;
    [SerializeField] private HudManager ui;
    [SerializeField] private float pityTime = 0.13f;
    [SerializeField] private int scoreEvolve = 5;

    private Rigidbody _rigidbody;
    private Camera _mainCamera;
    private ProjectileOrbitalController _orbitalController;
    private RhythmTimer _rTimer;
    private int _tempScore = 0;
    private Evolvable _evolution;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rTimer = GetComponentInParent<RhythmTimer>();
        _orbitalController = GetComponent<ProjectileOrbitalController>();
        PlayerPrefs.SetInt("TempScore", 0);
        _evolution = GetComponent<Evolvable>();
    }

    public void AddScore(int val)
    {
        LevelScore += val;
        _tempScore = LevelScore;
        ui.Sc.UpdateScore(_tempScore);
        if(LevelScore %scoreEvolve == 0)
        {
            _evolution.Evolve();
        }
    }

    private void Start()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        HandleShootInput();
        MovementControl();
        CheckStatus();
    }

    public void TakeDamage(int dmg)
    {
        CurrentHealth -= dmg;
    }

    public void UpdateMaxHealth(int max)
    {
        if (CurrentHealth + (max - MaxHealth) > 0)
            CurrentHealth += max - MaxHealth;

        MaxHealth = max;
        ui.Hp.DrawHealth();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.Enemy))
        {
            TakeDamage(1);
        }
    }

    private void MovementControl()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        _rigidbody.AddTorque(new Vector3(vertical / 3, 0, -horizontal / 3) * speed);
    }

    private void HandleShootInput()
    {
        if (Input.GetButtonDown("Fire1") && (_rTimer.CheckTime(pityTime) || !IsRhythmActive))
        {
            // Cast a ray from the camera to see where the click intersects with the floor
            var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit))
            {
                _orbitalController.EnqueueShoot(hit.point);
            }
        }
    }

    private void CheckStatus()
    {
        if (CurrentHealth <= 0)
        {
            Destroy(gameObject);
            SceneManager.LoadScene(0);
        }
    }
}
