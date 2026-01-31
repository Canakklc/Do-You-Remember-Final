using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class raycastCar : MonoBehaviour
{
    Ray rayCast;
    RaycastHit raycastInfo;
    carCollector takeBool;
    carPostProcess takeEffect;
    public RectTransform cursorCar;

    void Awake()
    {
        takeBool = GetComponent<carCollector>();
        takeEffect = GetComponent<carPostProcess>();
    }

    void Update()
    {
        if (takeBool.inCarControl == true)
        {
            if (Input.GetMouseButton(0))
            {
                rayCast = new Ray(cursorCar.position, cursorCar.forward);

                if (Physics.Raycast(rayCast, out raycastInfo, 100f))
                {
                    Debug.Log(raycastInfo.collider.name);
                    if (raycastInfo.collider.name == "LastAnomaly")
                    {
                        takeEffect.StartEffects();
                    }

                }
            }
        }

    }
}
