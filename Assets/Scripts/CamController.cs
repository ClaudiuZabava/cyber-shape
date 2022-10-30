using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    [SerializeField]
    private Vector3 tOffset;

    [SerializeField]
    private float cSpeed;

    // Update is called once per frame
    private void Update()
    {
        MoveCam();
    }


    private void MoveCam()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + tOffset, cSpeed * Time.deltaTime);
    }
}
