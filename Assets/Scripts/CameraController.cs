using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float sensitivity = 0.1f;


    void Update()
    {
        float deltaX = Input.GetAxis("Mouse X");
        float deltaY = Input.GetAxis("Mouse Y");

        Vector3 newPosition = transform.position;
        newPosition.x += deltaX * sensitivity;
        newPosition.y += deltaY * sensitivity;

        transform.position = newPosition;
    }
}
