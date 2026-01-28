using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastAnomaly : MonoBehaviour
{
    carCollector takeBool;
    InterractableObjects levelCount;
    public GameObject lastAnomaly;
    Vector3 startPos;
    public float frequence;
    public float waveLen;

    void Awake()
    {
        takeBool = GetComponent<carCollector>();
        levelCount = GetComponent<InterractableObjects>();
    }
    void Start()
    {
        startPos = lastAnomaly.transform.position;
        lastAnomaly.SetActive(false);
    }

    void Update()
    {
        if (takeBool.inCarControl == true && levelCount.collectedAnomaly == 6)
        {
            lastAnomaly.SetActive(true);
            float x = Mathf.Sin(2 * Mathf.PI * Time.time * frequence) * waveLen;
            float z = Mathf.Cos(2 * Mathf.PI * Time.time * frequence) * waveLen;
            lastAnomaly.transform.localPosition = startPos + new Vector3(x, 0, z);
        }
    }

}
