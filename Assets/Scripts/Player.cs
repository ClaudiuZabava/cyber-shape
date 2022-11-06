using Projectiles;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f;

    private Rigidbody _rigidbody;
    private Camera _mainCamera;
    private ProjectileOrbitalController _orbitalController;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _mainCamera = Camera.main;
        _orbitalController = GetComponent<ProjectileOrbitalController>();
    }

    private void Update()
    {
        HandleShootInput();
        MovementControl();
    }

    private void MovementControl()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        _rigidbody.AddTorque(new Vector3(vertical / 3, 0, -horizontal / 3) * speed);
    }

    private void HandleShootInput()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            // Cast a ray from the camera to see where the click intersects with the floor
            var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit))
            {
                _orbitalController.Shoot(hit.point);
            }
        }
    }
}