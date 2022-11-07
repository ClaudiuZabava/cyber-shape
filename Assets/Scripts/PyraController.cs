using Constants;
using Projectiles;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PyraController : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f;
    [SerializeField] public float _phealth = 100.0f;

    private Rigidbody _rigidbody;
    private Camera _mainCamera;
    private ProjectileOrbitalController _orbitalController;
    public float _OldPhealth;
    private RhythmTimer _rTimer;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rTimer = GetComponentInParent<RhythmTimer>();
    }

    private void Start()
    {
        _OldPhealth = _phealth;
        _mainCamera = Camera.main;
        _orbitalController = GetComponent<ProjectileOrbitalController>();
    }

    private void Update()
    {
        HandleShootInput();
        MovementControl();
        checkStatus();
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

    private void checkStatus()
    {
        if(_phealth <= 0)
        {
            Destroy(this.gameObject);
            SceneManager.LoadScene(0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Enemy")
        {
            _phealth -=5;
        }
    }
}
