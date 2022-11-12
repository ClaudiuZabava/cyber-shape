using Constants;
using Projectiles;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public int current_health = 4, max_health = 4;

    [SerializeField] private float speed = 5.0f;

    private Rigidbody _rigidbody;
    private Camera _mainCamera;
    private ProjectileOrbitalController _orbitalController;
    private RhythmTimer _rTimer;
    private HudManager _UI;

    private void Awake()
    {
        _UI = GameObject.Find("HudManager").GetComponent<HudManager>();
        _rigidbody = GetComponent<Rigidbody>();
        _rTimer = GetComponentInParent<RhythmTimer>();
        _orbitalController = GetComponent<ProjectileOrbitalController>();
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
        current_health = current_health - dmg;
    }

    public void UpdateMaxHealth(int max)
    {
        if (current_health + (max - max_health) > 0)
            current_health = current_health + (max - max_health);

        max_health = max;
        _UI.hp.DrawHealth();
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
        if (Input.GetButtonDown("Fire1") && _rTimer.CheckTime(0.10f))
        {
            // Cast a ray from the camera to see where the click intersects with the floor
            var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit))
            {
                _orbitalController.Shoot(hit.point);
            }
        }
    }

    private void CheckStatus()
    {
        if (current_health <= 0)
        {
            Destroy(gameObject);
            SceneManager.LoadScene(0);
        }
    }
}