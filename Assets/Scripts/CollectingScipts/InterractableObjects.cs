using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class InterractableObjects : MonoBehaviour
{
    CamRaycast rayOfCam;
    justGhost takeRoomLights;
    [SerializeField] int collectedAnomaly = 0;
    public List<GameObject> interractibleObjects = new List<GameObject>();
    public List<GameObject> complicatedInterractibles = new List<GameObject>();
    public List<bool> LampBools = new List<bool>();
    public TextMeshProUGUI printCollectedAnomalies;

    void Awake()
    {
        rayOfCam = GetComponent<CamRaycast>();
        takeRoomLights = GetComponent<justGhost>();

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
    }


    void CollectAnomaly()
    {
        if (rayOfCam.rayCastInfo.collider == null) return;

        GameObject hitObj = rayOfCam.rayCastInfo.collider.gameObject;

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


}
