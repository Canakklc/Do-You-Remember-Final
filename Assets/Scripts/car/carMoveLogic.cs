using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carMoveLogic : MonoBehaviour
{
    public float speed = 500f;
    public float turnForce = 50f;

    Rigidbody rb;
    carCollector takeBool;

    [Header("Audio")]
    public AudioSource engineSource;
    public AudioClip engineClip;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        takeBool = GameObject.FindWithTag("Settings").GetComponent<carCollector>();

        if (engineSource != null)
        {
            engineSource.clip = engineClip;
            engineSource.loop = true;
            engineSource.playOnAwake = false;
        }
    }

    void FixedUpdate()
    {
        if (!takeBool.inCarControl)
        {
            StopEngineSound();
            return;
        }

        float move = Input.GetAxis("Vertical");
        float turn = Input.GetAxis("Horizontal");

        rb.AddForce(-transform.right * move * speed * Time.fixedDeltaTime);
        rb.AddTorque(Vector3.up * turn * turnForce * Time.fixedDeltaTime);

        HandleEngineSound(move);

        if (Input.GetKey(KeyCode.Space))
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

    void HandleEngineSound(float moveInput)
    {
        if (Mathf.Abs(moveInput) > 0.1f)
        {
            if (!engineSource.isPlaying)
                engineSource.Play();

            engineSource.pitch = 1f + Mathf.Abs(moveInput) * 0.5f;
        }
        else
        {
            StopEngineSound();
        }
    }

    void StopEngineSound()
    {
        if (engineSource != null && engineSource.isPlaying)
        {
            engineSource.Stop();
        }
    }
}
