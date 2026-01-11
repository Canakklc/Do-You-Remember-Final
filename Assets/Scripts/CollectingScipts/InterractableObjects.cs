using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class InterractableObjects : MonoBehaviour
{
    GameObject hitObj;
    CamRaycast rayOfCam;
    justGhost takeRoomLights;
    objectAnomalies pickObjectCheck;
    [SerializeField] int collectedAnomaly = 0;
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

    }
    void Start()
    {
        for (int i = 0; i < interractibleObjects.Count; i++)
        {
            if (interractibleObjects[i] == null) continue;
        }
        //currentState = canCollect.Uncollectible;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CollectAnomaly();
        }
        printCollectedAnomalies.text = collectedAnomaly.ToString();
        timerForHeatAnoms += Time.deltaTime;
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
                collectedAnomaly++;
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
                        collectedAnomaly++;

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
                    collectedAnomaly++;
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
                        secondLevelCOllectiblesStatic.Remove(hitObj);
                    }
                }
            }

        }
    }


}
