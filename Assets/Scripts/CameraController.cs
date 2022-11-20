using Constants;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float smoothSpeed = 3.5f;

    private Camera _camera;
    private Transform _playerTransform;
    private Vector3 _offset;

    private void Awake()
    {
        _camera = GetComponent<Camera>();

        _playerTransform = GameObject.FindWithTag(Tags.Player).transform;
        _offset = transform.position - _playerTransform.position;
    }

    private void LateUpdate()
    {
        var newCameraPos = _playerTransform.position + _offset;

        // Check if the edges of the camera are seeing outside of the floor plane.
        // Left
        var ray = _camera.ViewportPointToRay(new Vector3(0.0f, 0.5f, 0.0f));
        if (!Physics.Raycast(ray, Mathf.Infinity, (int) Layers.Floor))
        {
            newCameraPos.x = Mathf.Max(_camera.transform.position.x, newCameraPos.x);
        }

        // Right
        ray = _camera.ViewportPointToRay(new Vector3(1.0f, 0.5f, 0.0f));
        if (!Physics.Raycast(ray, Mathf.Infinity, (int) Layers.Floor))
        {
            newCameraPos.x = Mathf.Min(_camera.transform.position.x, newCameraPos.x);
        }

        // Bottom
        ray = _camera.ViewportPointToRay(new Vector3(0.5f, 0.0f, 0.0f));
        if (!Physics.Raycast(ray, Mathf.Infinity, (int) Layers.Floor))
        {
            newCameraPos.z = Mathf.Max(_camera.transform.position.z, newCameraPos.z);
        }

        // Top
        ray = _camera.ViewportPointToRay(new Vector3(0.5f, 1.0f, 0.0f));
        if (!Physics.Raycast(ray, Mathf.Infinity, (int) Layers.Floor))
        {
            newCameraPos.z = Mathf.Min(_camera.transform.position.z, newCameraPos.z);
        }

        transform.position = Vector3.Lerp(_camera.transform.position, newCameraPos, smoothSpeed * Time.deltaTime);
    }
}
