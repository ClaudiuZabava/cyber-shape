using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControll : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    [SerializeField]
    private Vector3 tOffset;

    [SerializeField]
    private float cspeed;


    // Update is called once per frame
    void Update()
    {
        MoveCam();
        
    }

    void MoveCam()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + tOffset, cspeed * Time.deltaTime);
    }
}
