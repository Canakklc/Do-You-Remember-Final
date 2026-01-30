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
    bool hit;
    public bool inCarControl = false;

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
        if (Input.GetMouseButton(0) && takeRay.rayCastInfo.collider.CompareTag("CarActive") && inCarControl == false)
        {
            takeEffects.StartEffects();
            StartGenerateText();
            CarCamActive();
            inCarControl = true;
        }
        if (Input.GetKeyDown(KeyCode.G) && inCarControl == true)
        {
            ExitTheCar();
            inCarControl = false;
        }
    }

    void CarCamActive()
    {
        Debug.Log("Car cam active");
        playerCam.depth = -2;
        carCam.depth = 0;
        inCarControl = true;
    }
    void ExitTheCar()
    {
        Debug.Log("Car Exit");
        playerCam.depth = 0;
        carCam.depth = -2;
        inCarControl = false;
    }
    IEnumerator GenerateText()//car canva
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


}
