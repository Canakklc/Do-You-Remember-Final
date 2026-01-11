using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chooseGhost : MonoBehaviour
{
    objectAnomalies levelOneGhost;
    void Awake()
    {
        levelOneGhost = GetComponent<objectAnomalies>();
    }
    void Start()
    {
        levelOneGhost.setCreatureType[0] = true;
    }

}
