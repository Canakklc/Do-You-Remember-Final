using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterractableObjects : MonoBehaviour
{
    CamRaycast rayOfCam;
    [SerializeField] int collectedAnomaly = 0;
    public List<GameObject> interractibleObjects = new List<GameObject>();

    void Awake()
    {
        rayOfCam = GetComponent<CamRaycast>();
    }
    void Start()
    {
        for (int i = 0; i < interractibleObjects.Count; i++)
        {
            if (interractibleObjects[i] == null) continue;
        }
    }
    void Update()
    {
        CollectAnomaly();
    }


    void CollectAnomaly()
    {
        if (Input.GetMouseButtonDown(0))
        {
            for (int i = 0; i < interractibleObjects.Count; i++)
            {
                if (rayOfCam.rayCastInfo.collider == null) return;
                bool hit = rayOfCam.rayCastInfo.collider.gameObject == interractibleObjects[i];
                if (hit)
                {
                    collectedAnomaly += 1;
                    Debug.Log("Anomally Collected");
                }
            }
        }
    }

}
