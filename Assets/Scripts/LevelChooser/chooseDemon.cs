using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chooseDemon : MonoBehaviour
{
    objectAnomalies levelThirdDemon;

    void Awake()
    {
        levelThirdDemon.GetComponent<objectAnomalies>();
    }
    void Start()
    {
        levelThirdDemon.setCreatureType[2] = true;
    }


}
