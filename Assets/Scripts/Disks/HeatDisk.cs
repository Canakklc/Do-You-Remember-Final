using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class HeatDisk : MonoBehaviour
{
    PostProcessVolume effectsForTermal;
    ColorGrading colorGrading;
    //Script
    CCTVandCAM takeBool;

    //BOOLS
    public bool thermalActive = false;
    void Awake()
    {
        effectsForTermal = GameObject.FindWithTag("Settings").GetComponentInChildren<PostProcessVolume>();
        effectsForTermal.profile.TryGetSettings<ColorGrading>(out colorGrading);
        takeBool = GetComponent<CCTVandCAM>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            ActivateThermalDisk();
        }
    }

    void ActivateThermalDisk()
    {
        if (thermalActive == false && takeBool.onCCTV == true)
        {
            colorGrading.lift.overrideState = true;
            colorGrading.lift.value = new Vector4(-0.2f, -0.2f, 0.4f, -0.5f);


            thermalActive = true;
            Debug.Log("Thermal active");
        }
        else if (thermalActive == true && takeBool.onCCTV == true)
        {
            colorGrading.lift.overrideState = false;
            colorGrading.gain.overrideState = false;
            thermalActive = false;
        }
    }
}
