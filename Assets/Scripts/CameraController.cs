using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 tOffset;
    [SerializeField] private float cSpeed;

    private void Update()
    {
        MoveCamera();
    }

    private void MoveCamera()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + tOffset, cSpeed * Time.deltaTime);
    }
}
