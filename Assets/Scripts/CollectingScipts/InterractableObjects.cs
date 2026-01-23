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
    public int collectedAnomaly = 0; //For level
    public static int memoryCollect; //for disk static one
    public TextMeshProUGUI memoryText;
    public List<GameObject> interractibleObjects = new List<GameObject>();
    public List<GameObject> complicatedInterractibles = new List<GameObject>();
    public List<bool> LampBools = new List<bool>();
    public TextMeshProUGUI printCollectedAnomalies;
    [Header("Second level object list")]
    public List<GameObject> secondLevelCollectibles = new List<GameObject>();
    public List<GameObject> secondLevelCOllectiblesStatic = new List<GameObject>();
    public float timerForHeatAnoms;

    void Awake()
    {
        rayOfCam = GetComponent<CamRaycast>();
        takeRoomLights = GetComponent<justGhost>();
        pickObjectCheck = GetComponent<objectAnomalies>();
        anomalyCollectEffect = GetComponent<anomalyCollectEffect>();

    }
    void Start()
    {
        for (int i = 0; i < interractibleObjects.Count; i++)
        {
            if (interractibleObjects[i] == null) continue;
        }
        //currentState = canCollect.Uncollectible;
        if (memoryCollect == 0)
        {
            memoryCollect = 3;
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

        //Memory collection 
        memoryText.text = " =" + " " + memoryCollect.ToString();
    }


    void CollectAnomaly()
    {
        if (rayOfCam.rayCastInfo.collider == null) return;
        hitObj = rayOfCam.rayCastInfo.collider.gameObject;
        if (pickObjectCheck.setCreatureType[0] == true)
        {
            // NORMAL ANOMALY
            if (interractibleObjects.Contains(hitObj))
            {
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
                        anomalyCollectEffect.CallChromaticEffect();
                        collectedAnomaly++;
                        memoryCollect += 1;

                        Debug.Log("Lamp anomaly collected: " + i);
                        return;
                    }
                }
            }
        }
        if (pickObjectCheck.setCreatureType[1] == true)//level 2 collect logic
        {
            if (secondLevelCollectibles.Contains(hitObj))
            {
                if (timerForHeatAnoms > 60f)
                {
                    secondLevelCollectibles.Remove(hitObj);
                    anomalyCollectEffect.CallChromaticEffect();
                    collectedAnomaly++;
                    memoryCollect += 1;
                    Debug.Log("Second collected");
                }
            }
            if (secondLevelCOllectiblesStatic.Contains(hitObj))
            {
                bool canCut = pickObjectCheck.boolsToCutAction[0] ||
                              pickObjectCheck.boolsToCutAction[1] ||
                              pickObjectCheck.boolsToCutAction[2];

                {
                    if (canCut)
                    {
                        collectedAnomaly++;
                        memoryCollect += 1;
                        anomalyCollectEffect.CallChromaticEffect();
                        secondLevelCOllectiblesStatic.Remove(hitObj);
                        pickObjectCheck.checkerForMotionAnimation[0] = false;
                        pickObjectCheck.checkerForMotionAnimation[1] = false; //those are for motion image
                        pickObjectCheck.checkerForMotionAnimation[2] = false;
                        pickObjectCheck.canTransformPos = true;
                    }
                }
            }

        }
    }


}
