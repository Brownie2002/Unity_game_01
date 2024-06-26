using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playercam : MonoBehaviour
{
    [SerializeField] Transform target;
    Vector3 offsetCamera;

    [Range(0.01f, 1.0f)]
    [SerializeField]
    float smooth;

    void Start()
    {
        offsetCamera = transform.position - target.position; 
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 cameraPosition = target.position + offsetCamera; 
        Vector3 smoothPosition = Vector3.Lerp(transform.position,cameraPosition, smooth);
        transform.position = smoothPosition;
        transform.LookAt(target);
    }
}
