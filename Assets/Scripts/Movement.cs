using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private float speed = 5.0f;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        _rigidbody.AddTorque(new Vector3(vertical, 0, -horizontal) * speed);        
    }
}
