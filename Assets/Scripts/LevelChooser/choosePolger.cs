using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class choosePolger : MonoBehaviour
{
    objectAnomalies secLevelPolger;

    void Awake()
    {
        secLevelPolger = GetComponent<objectAnomalies>();
    }
    void Start()
    {
        secLevelPolger.setCreatureType[1] = true;
    }


}
