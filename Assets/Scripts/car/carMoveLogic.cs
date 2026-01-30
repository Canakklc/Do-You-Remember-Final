using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carMoveLogic : MonoBehaviour
{
    public float speed = 500f;
    public float turnForce = 50f;
    Rigidbody rb;
    carCollector takeBool;
    float xRotation = 0;
    public float sensivity = 5f;
    public Transform carCam;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        takeBool = GameObject.FindWithTag("Settings").GetComponent<carCollector>();
    }
    void Update()
    {
        Looking();
    }

    void FixedUpdate()
    {
        if (takeBool.inCarControl == true)
        {
            float move = Input.GetAxis("Vertical");
            float turn = Input.GetAxis("Horizontal");

            rb.AddForce(-transform.right * move * speed * Time.fixedDeltaTime);
            rb.AddTorque(Vector3.up * turn * turnForce * Time.fixedDeltaTime);//hangi eksen etrafında döneceğin!
        }
        if (Input.GetKey(KeyCode.Space) && takeBool.inCarControl == true)
        {
            speed = 0;
            turnForce = 0;
        }
        else
        {
            speed = 100f;
            turnForce = 10f;
        }
    }
    void Looking()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensivity;

        transform.Rotate(Vector3.up * mouseX);
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        carCam.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

    }
}
