using Constants;
using Projectiles;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f;
    [SerializeField] public float health = 100.0f;

    private Rigidbody _rigidbody;
    private Camera _mainCamera;
    private ProjectileOrbitalController _orbitalController;
    private RhythmTimer _rTimer;

    private void Awake()
    {
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
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.Enemy))
        {
            health -= 5;
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
        if (health <= 0)
        {
            Destroy(gameObject);
            SceneManager.LoadScene(0);
        }
    }
}
