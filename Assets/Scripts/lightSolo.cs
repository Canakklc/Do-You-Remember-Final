using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightSolo : MonoBehaviour
{
    public GameObject lightBulb;
    public GameObject physicalBulb;

    objectAnomalies enemy;
    InterractableObjects takeRay;
    anomalyCollectEffect effect;

    float timeToStart = 0;
    public bool canPickUp = true;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip collectSound;

    void Awake()
    {
        enemy = GetComponent<objectAnomalies>();
        takeRay = GetComponent<InterractableObjects>();
        effect = GetComponent<anomalyCollectEffect>();
    }

    void Update()
    {
        timeToStart += Time.deltaTime;

        if (timeToStart > 40f && enemy.setCreatureType[0] == true)
        {
            lightBulb.SetActive(false);
        }

        if (Input.GetMouseButton(0) && takeRay.hitObj == physicalBulb && canPickUp == true)
        {
            collectIt();
            effect.CallChromaticEffect();
        }
    }

    void collectIt()
    {
        if (timeToStart > 40)
        {
            takeRay.collectedAnomaly += 1;
            InterractableObjects.memoryCollect += 1;
            canPickUp = false;

            // 🔊 COLLECT SOUND
            if (audioSource != null && collectSound != null)
            {
                audioSource.PlayOneShot(collectSound);
            }
        }
    }
}
