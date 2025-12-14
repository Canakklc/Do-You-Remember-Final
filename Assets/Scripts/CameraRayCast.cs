using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRayCast : MonoBehaviour
{
    CCTVandCAM takeCamList;
    public Ray raycast;
    public RaycastHit rayCastInfo;
    void Awake()
    {
        takeCamList = GetComponent<CCTVandCAM>();
    }

    void Update()
    {
        for (int i = 0; i < takeCamList.allCams.Count; i++)
        {
            raycast = takeCamList.allCams[i].ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(raycast, out rayCastInfo))
            {
                if (Input.GetMouseButton(0) && takeCamList.onCCTV == true)
                {
                    Debug.Log(rayCastInfo.collider.name);
                }
            }
        }
    }
}
