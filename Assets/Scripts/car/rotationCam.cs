using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotationCam : MonoBehaviour
{
    public float sensivity = 1.5f;

    float xRotation = 0;
    float yRotation = 0;
    Quaternion baseRot;
    void Update()
    {
        Looking();
    }
    void Start()
    {
        baseRot = transform.localRotation;
    }

    void Looking()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensivity;


        xRotation -= mouseY;
        yRotation += mouseX;
        xRotation = Mathf.Clamp(xRotation, -50f, 50f);
        yRotation = Mathf.Clamp(yRotation, -50f, 50f);
        transform.localRotation = baseRot * Quaternion.Euler(xRotation, yRotation, 0f);
    }
}
