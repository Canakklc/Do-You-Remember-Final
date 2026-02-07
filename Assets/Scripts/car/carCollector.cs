using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class carCollector : MonoBehaviour
{
    public Camera playerCam;
    public Camera carCam;
    public GameObject textFirst;
    public GameObject textSec;

    Raycast takeRay;
    carPostProcess takeEffects;

    public bool inCarControl = false;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip enterCarSound;
    public AudioClip exitCarSound; // opsiyonel

    void Start()
    {
        textFirst.SetActive(false);
        textSec.SetActive(false);
    }

    void Awake()
    {
        takeRay = GetComponent<Raycast>();
        takeEffects = GetComponent<carPostProcess>();
    }

    void Update()
    {
        if (takeRay.rayCastInfo.collider == null) return;

        if (Input.GetMouseButtonDown(0) &&
            takeRay.rayCastInfo.collider.CompareTag("CarActive") &&
            inCarControl == false)
        {
            PlayEnterSound();
            takeEffects.StartEffects();
            StartGenerateText();
            CarCamActive();
            inCarControl = true;
        }

        if (Input.GetKeyDown(KeyCode.G) && inCarControl == true)
        {
            PlayExitSound();
            ExitTheCar();
            inCarControl = false;
        }
    }

    void CarCamActive()
    {
        playerCam.depth = -2;
        carCam.depth = 0;
    }

    void ExitTheCar()
    {
        playerCam.depth = 0;
        carCam.depth = -2;
    }

    IEnumerator GenerateText()
    {
        textFirst.SetActive(true);
        yield return new WaitForSeconds(1f);
        textSec.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        textFirst.SetActive(false);
        textSec.SetActive(false);
    }

    void StartGenerateText()
    {
        StartCoroutine(GenerateText());
    }

    void PlayEnterSound()
    {
        if (audioSource != null && enterCarSound != null)
        {
            audioSource.PlayOneShot(enterCarSound);
        }
    }

    void PlayExitSound()
    {
        if (audioSource != null && exitCarSound != null)
        {
            audioSource.PlayOneShot(exitCarSound);
        }
    }
}
