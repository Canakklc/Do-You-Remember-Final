using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coldGhostMove : MonoBehaviour
{
    public float frequence;
    public float waveLen;
    public float timerForColdGhost;
    public GameObject coldGhost;
    HeatDisk takeBool;
    objectAnomalies creatureType;
    Vector3 startPos;
    void Start()
    {
        startPos = coldGhost.transform.position;
        coldGhost.SetActive(false);
    }
    void Awake()
    {
        takeBool = GetComponent<HeatDisk>();
        creatureType = GetComponent<objectAnomalies>();
    }
    void Update()
    {
        timerForColdGhost += Time.deltaTime;
        bool cond = takeBool.thermalActive;
        if (coldGhost == null) return;
        if (cond)
        {
            if (timerForColdGhost >= 40 && creatureType.setCreatureType[0] == true)
            {
                coldGhost.SetActive(true);
                float x = Mathf.Sin(2 * Mathf.PI * Time.time * frequence) * waveLen;
                float z = Mathf.Cos(2 * Mathf.PI * Time.time * frequence) * waveLen;
                coldGhost.transform.localPosition = startPos + new Vector3(0, 0, z);
            }
        }
        else
        {
            coldGhost.SetActive(false);
        }
    }
}
