using Evolution;
using Projectiles;
using UnityEngine;


public class InputController : MonoBehaviour
{
    [field: SerializeField] public bool IsRhythmActive { get; private set; } = true;
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float beatLeeway = 0.5f;

    private const KeyCode SPrevBulletKey = KeyCode.Q;
    private const KeyCode SNextBulletKey = KeyCode.E;
    private Rigidbody _rigidbody;
    private Camera _mainCamera;
    private ProjectileOrbitalController _orbitalController;
    private RhythmTimer _rTimer;
    private Player _player;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rTimer = GetComponentInParent<RhythmTimer>();
        _orbitalController = GetComponent<ProjectileOrbitalController>();
        _player = GetComponentInParent<Player>();
    }

    private void Start()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        HandleShootInput();
        MovementControl();
        HandleChangeBullet();
    }

    private void MovementControl()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        _rigidbody.AddTorque(new Vector3(vertical / 3, 0, -horizontal / 3) * speed);
    }

    private void HandleShootInput()
    {
        var damage = 5;
        if (_player.CanShoot == false)
        {
            return;
        }

        if (_rTimer.CheckTime(beatLeeway) || !IsRhythmActive)
        {
            damage *= 2;
        }

        if (_rTimer.CheckTime(beatLeeway / 5) || !IsRhythmActive)
        {
            damage *= 5;
        }

        if (_rTimer.CheckTime(beatLeeway / 10) || !IsRhythmActive)
        {
            damage *= 10;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            // Cast a ray from the camera to see where the click intersects with the floor
            var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit))
            {
                _orbitalController.EnqueueShoot(hit.point, damage);
            }
        }
    }

    private void HandleChangeBullet()
    {
        BulletType newBullet = null;
        if (Input.GetKeyDown(SPrevBulletKey))
        {
            var currentBulletIndex = _player.AvailableBullets.FindIndex(
                bullet => bullet == _player.CurrentBullet
            );

            var nextIndex = currentBulletIndex - 1;
            if (nextIndex < 0)
            {
                nextIndex = _player.AvailableBullets.Count - 1;
            }
            newBullet = _player.AvailableBullets[nextIndex];
        }

        if (Input.GetKeyDown(SNextBulletKey))
        {
            var currentBulletIndex = _player.AvailableBullets.FindIndex(
                bullet => bullet == _player.CurrentBullet
            );

            var nextIndex = currentBulletIndex + 1;
            if (nextIndex >= _player.AvailableBullets.Count)
            {
                nextIndex = 0;
            }

            newBullet = _player.AvailableBullets[nextIndex];
        }
        
        if (newBullet != null)
        {
            _player.SetBullet(newBullet);
        }
    }
}