using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class InterractableObjects : MonoBehaviour
{
    public GameObject hitObj;

    CamRaycast rayOfCam;
    justGhost takeRoomLights;
    objectAnomalies pickObjectCheck;
    anomalyCollectEffect anomalyCollectEffect;

    public int collectedAnomaly = 0; // For level
    public static int memoryCollect; // for disk static one

    public TextMeshProUGUI memoryText;
    public TextMeshProUGUI printCollectedAnomalies;

    public List<GameObject> interractibleObjects = new List<GameObject>();
    public List<GameObject> complicatedInterractibles = new List<GameObject>();
    public List<bool> LampBools = new List<bool>();

    [Header("Second level object list")]
    public List<GameObject> secondLevelCollectibles = new List<GameObject>();
    public List<GameObject> secondLevelCOllectiblesStatic = new List<GameObject>();

    public float timerForHeatAnoms;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip collectAnomalySound;

    void Awake()
    {
        rayOfCam = GetComponent<CamRaycast>();
        takeRoomLights = GetComponent<justGhost>();
        pickObjectCheck = GetComponent<objectAnomalies>();
        anomalyCollectEffect = GetComponent<anomalyCollectEffect>();
    }

    void Start()
    {
        if (memoryCollect == 0)
        {
            memoryCollect = 1;
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CollectAnomaly();
        }

        printCollectedAnomalies.text = collectedAnomaly.ToString();
        timerForHeatAnoms += Time.deltaTime;

        // Memory UI
        memoryText.text = " = " + memoryCollect.ToString();
    }

    void CollectAnomaly()
    {
        if (rayOfCam.rayCastInfo.collider == null) return;

        hitObj = rayOfCam.rayCastInfo.collider.gameObject;

        // LEVEL 1
        if (pickObjectCheck.setCreatureType[0] == true)
        {
            // NORMAL ANOMALY
            if (interractibleObjects.Contains(hitObj))
            {
                PlayCollectSound();
                anomalyCollectEffect.CallChromaticEffect();

                collectedAnomaly++;
                memoryCollect += 1;

                interractibleObjects.Remove(hitObj);
                Debug.Log("Anomaly Collected");
                return;
            }

            // COMPLICATED (LAMPS)
            if (complicatedInterractibles.Contains(hitObj))
            {
                for (int i = 0; i < LampBools.Count; i++)
                {
                    if (LampBools[i])
                    {
                        LampBools[i] = false;

                        PlayCollectSound();
                        anomalyCollectEffect.CallChromaticEffect();

                        collectedAnomaly++;
                        memoryCollect += 1;

                        Debug.Log("Lamp anomaly collected: " + i);
                        return;
                    }
                }
            }
        }

        // LEVEL 2
        if (pickObjectCheck.setCreatureType[1] == true)
        {
            if (secondLevelCollectibles.Contains(hitObj))
            {
                if (timerForHeatAnoms > 60f)
                {
                    PlayCollectSound();
                    anomalyCollectEffect.CallChromaticEffect();

                    collectedAnomaly++;
                    memoryCollect += 1;

                    secondLevelCollectibles.Remove(hitObj);
                    Debug.Log("Second collected");
                }
            }

            if (secondLevelCOllectiblesStatic.Contains(hitObj))
            {
                bool canCut =
                    pickObjectCheck.boolsToCutAction[0] ||
                    pickObjectCheck.boolsToCutAction[1] ||
                    pickObjectCheck.boolsToCutAction[2];

                if (canCut)
                {
                    PlayCollectSound();
                    anomalyCollectEffect.CallChromaticEffect();

                    collectedAnomaly++;
                    memoryCollect += 1;

                    secondLevelCOllectiblesStatic.Remove(hitObj);

                    pickObjectCheck.checkerForMotionAnimation[0] = false;
                    pickObjectCheck.checkerForMotionAnimation[1] = false;
                    pickObjectCheck.checkerForMotionAnimation[2] = false;

                    pickObjectCheck.canTransformPos = true;
                }
            }
        }
    }

    void PlayCollectSound()
    {
        if (audioSource != null && collectAnomalySound != null)
        {
            audioSource.PlayOneShot(collectAnomalySound);
        }
    }
}
