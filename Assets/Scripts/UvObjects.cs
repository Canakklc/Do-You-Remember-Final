using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UvObjects : MonoBehaviour
{
    HeatDisk diskBools;
    public List<GameObject> Objects = new List<GameObject>();
    public float timerForSpawn = 0;


    void Start()
    {
        for (int i = 0; i < Objects.Count; i++)
        {
            Objects[i].SetActive(false);
        }
    }
    void Awake()
    {
        diskBools = GetComponent<HeatDisk>();
    }
    void Update()
    {
        timerForSpawn += Time.deltaTime;
        AppearObjectsOnBool();
    }
    void AppearObjectsOnBool()//blood marks
    {
        if (diskBools.uvActive)
        {
            for (int i = 0; i < Objects.Count; i++)
            {
                if (timerForSpawn >= 20f)
                {
                    Objects[0].SetActive(true);
                }
                if (timerForSpawn >= 35)
                {
                    Objects[1].SetActive(true);
                }
                if (timerForSpawn >= 42)
                {
                    Objects[2].SetActive(true);
                }
            }
        }

        else
        {
            for (int i = 0; i < Objects.Count; i++)
            {
                Objects[i].SetActive(false);
            }
        }
    }
}
