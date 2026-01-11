using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkPolger : MonoBehaviour
{
    public float frequence;
    public float waveLen;
    public GameObject blackSpirit;
    Vector3 startPose;
    HeatDisk takeBool;
    objectAnomalies creatureType;
    public float timeToStart;
    void Start()
    {
        startPose = blackSpirit.transform.position;
        blackSpirit.SetActive(false);
    }
    void Awake()
    {
        takeBool = GetComponent<HeatDisk>();
        creatureType = GetComponent<objectAnomalies>();
    }
    void Update()
    {
        timeToStart += Time.deltaTime;
        bool cond = takeBool.uvActive;
        if (blackSpirit == null) return;
        if (cond)
        {
            if (timeToStart > 80 && creatureType.setCreatureType[1])
            {
                blackSpirit.SetActive(true);
                float x = Mathf.Sin(2 * Mathf.PI * Time.time * frequence) * waveLen;
                float z = Mathf.Cos(2 * Mathf.PI * Time.time * frequence) * waveLen;
                blackSpirit.transform.localPosition = startPose + new Vector3(x, 0, z);
            }
        }
        else
        {
            blackSpirit.SetActive(false);
        }
    }

}
