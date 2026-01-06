using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InterractableObjects : MonoBehaviour
{
    CamRaycast rayOfCam;
    [SerializeField] int collectedAnomaly = 0;
    public List<GameObject> interractibleObjects = new List<GameObject>();
    public List<GameObject> complicatedInterractibles = new List<GameObject>();





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
        //currentState = canCollect.Uncollectible;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CollectAnomaly();
        }
    }


    void CollectAnomaly()
    {
        for (int i = 0; i < interractibleObjects.Count; i++)
        {
            if (rayOfCam.rayCastInfo.collider == null) return;
            bool hit = rayOfCam.rayCastInfo.collider.gameObject == interractibleObjects[i];
            if (hit)
            {
                collectedAnomaly += 1;
                interractibleObjects.Remove(interractibleObjects[i]);
                Debug.Log("Anomally Collected");
            }

        }
    }

}
