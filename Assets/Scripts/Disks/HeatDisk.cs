using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class HeatDisk : MonoBehaviour
{
    PostProcessVolume volume;
    ColorGrading colorGrading;
    CCTVandCAM takeBool;

    public bool thermalActive;


    Vector4 lockedLift = new Vector4(-0.2f, -0.2f, 0.4f, -0.6f);

    void Awake()
    {
        volume = GameObject.FindWithTag("Settings")
            .GetComponentInChildren<PostProcessVolume>();

        volume.profile = Instantiate(volume.profile);
        volume.profile.TryGetSettings(out colorGrading);

        takeBool = GetComponent<CCTVandCAM>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T) && takeBool.onCCTV)
        {
            thermalActive = !thermalActive;
        }

        if (thermalActive)
        {
            colorGrading.lift.overrideState = true;
            colorGrading.lift.value = lockedLift;
        }
        else
        {
            colorGrading.lift.overrideState = false;
        }
    }

}
