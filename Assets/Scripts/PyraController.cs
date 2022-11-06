using Projectiles;
using UnityEngine;

public class PyraController : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private GameObject enemyPrefab;

    private Rigidbody _rigidbody;
    private Camera _mainCamera;
    private ProjectileOrbitalController _orbitalController;
    
    private const int ConstEnemies = 7;
    private const int MaxWidth = 10;
    private const int MaxDistance = 5;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _mainCamera = Camera.main;
        _orbitalController = GetComponent<ProjectileOrbitalController>();
        
        SpawnEnemies();
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

    private Vector3 GetRandomPosition()
    {
        var randomPosition = new Vector3(Random.Range(-MaxWidth, MaxWidth), 0.95f, Random.Range(-MaxWidth, MaxWidth)); 
        var distance = Vector3.Distance(transform.position, randomPosition);
        if (distance < MaxDistance)
        {
            return GetRandomPosition();
        }

        return randomPosition;
    }

    private void SpawnEnemies()
    {
        for (var i = 0; i < ConstEnemies; i++)
        {
            Instantiate(enemyPrefab, GetRandomPosition(), Quaternion.identity);
        }
    }
}