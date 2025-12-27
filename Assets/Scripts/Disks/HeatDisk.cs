using System.Collections;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class HeatDisk : MonoBehaviour
{
    PostProcessVolume volume;
    ColorGrading colorGrading;
    CCTVandCAM takeBool;
    DiskGather condForDisk;

    public bool thermalActive;
    public bool uvActive;
    public bool motionSensor;
    public GameObject motionSensorImage;


    Vector4 lockedLift = new Vector4(-0.2f, -0.2f, 0.4f, -0.6f);
    Vector4 lockedLiftUv = new Vector4(0.36f, -0.35f, 0.5f, -0.6f);
    Vector4 lockedLiftMotionSensor = new Vector4(0.36f, 1.0f, 0.1f, -0.18f);

    void Awake()
    {
        volume = GameObject.FindWithTag("Settings")
            .GetComponentInChildren<PostProcessVolume>();

        volume.profile = Instantiate(volume.profile);
        volume.profile.TryGetSettings(out colorGrading);

        takeBool = GetComponent<CCTVandCAM>();
        condForDisk = GetComponent<DiskGather>();
    }
    void Start()
    {
        motionSensorImage.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T) && takeBool.onCCTV && !uvActive && !motionSensor && condForDisk.ActivateCamFunc[0] == true)
        {
            StartPostExposure();
            thermalActive = !thermalActive;
            uvActive = false;
            motionSensor = false;
        }
        if (Input.GetKeyDown(KeyCode.U) && takeBool.onCCTV == true && !thermalActive && !motionSensor && condForDisk.ActivateCamFunc[1] == true)
        {
            StartPostExposure();
            uvActive = !uvActive;
            thermalActive = false;
            motionSensor = false;
        }
        if (Input.GetKeyDown(KeyCode.V) && takeBool.onCCTV == true && !thermalActive && !uvActive && condForDisk.ActivateCamFunc[2] == true)
        {
            StartPostExposure();
            motionSensor = !motionSensor;
            uvActive = false;
            thermalActive = false;
        }

        if (thermalActive)//thermalDisk
        {
            colorGrading.lift.overrideState = true;
            colorGrading.lift.value = lockedLift;
        }

        else if (uvActive)//uvDisk
        {
            colorGrading.lift.overrideState = true;
            colorGrading.lift.value = lockedLiftUv;
        }
        else if (motionSensor)//motionSensorActive
        {
            colorGrading.lift.overrideState = true;
            colorGrading.lift.value = lockedLiftMotionSensor;
            motionSensorImage.SetActive(true);
        }

        else
        {
            colorGrading.lift.overrideState = false;
            motionSensorImage.SetActive(false);
        }
    }

    IEnumerator PostExposureLevel()
    {
        float makeItBright = colorGrading.postExposure.value = 0;
        float makeItDark = colorGrading.postExposure.value = -10;
        float Elapsed = 0;
        float Duration = 1;
        while (Elapsed < Duration)
        {
            Elapsed += Time.deltaTime;
            float t = Elapsed / Duration;
            t = Mathf.SmoothStep(0f, 1f, t);
            colorGrading.postExposure.value = Mathf.Lerp(makeItDark, makeItBright, t);
            yield return null;
        }
    }
    void StartPostExposure()
    {
        StartCoroutine(PostExposureLevel());
    }

}
