using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class temperatureAnomaly : MonoBehaviour
{
    public Slider temperatureSlider;
    objectAnomalies ifSecondLevel;
    InterractableObjects anomalyRise;
    anomalyCollectEffect ChromEffect;
    float timerForTemperature;
    public bool canCollectTemperature = false;
    public Button TempButton;


    void Awake()
    {
        ifSecondLevel = GetComponent<objectAnomalies>();
        anomalyRise = GetComponent<InterractableObjects>();
        ChromEffect = GetComponent<anomalyCollectEffect>();
    }
    void Update()
    {
        timerForTemperature += Time.deltaTime;
        TemperatureRise();
    }
    void TemperatureRise()
    {
        if (ifSecondLevel.setCreatureType[1] == true)
        {
            if (timerForTemperature > 50f)
            {
                temperatureSlider.value = 0.900f;
                canCollectTemperature = true;
            }
        }
    }
    public void CollectTemperature()
    {
        if (canCollectTemperature == true)
        {
            canCollectTemperature = false;
            anomalyRise.collectedAnomaly += 1;
            TempButton.interactable = false;
            ChromEffect.CallChromaticEffect();
        }
    }
}
