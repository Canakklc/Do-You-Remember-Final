using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CamRaycast : MonoBehaviour
{
    [Header("other scripts")]
    CCTVandCAM reachCamList;
    //lists
    public Camera ActiveCam;
    [Header("Raycasting comp")]
    public Ray raycast;
    public RaycastHit rayCastInfo;


    void Awake()
    {
        reachCamList = GetComponent<CCTVandCAM>();
    }
    void Update()
    {
        SetWhichCamToTakeRay();
        RaycastingFromCameras();
    }

    void SetWhichCamToTakeRay()
    {
        if (reachCamList.chooseCamToRay[0] == true)
        {
            ActiveCam = reachCamList.allCams[0];
        }
        else if (reachCamList.chooseCamToRay[1] == true)
        {
            ActiveCam = reachCamList.allCams[1];
        }
        else if (reachCamList.chooseCamToRay[2] == true)
        {
            ActiveCam = reachCamList.allCams[2];
        }
        else
        {
            ActiveCam = reachCamList.allCams[3];
        }
    }

    void RaycastingFromCameras()
    {
        if (reachCamList.onCCTV == true)
        {
            raycast = ActiveCam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(raycast, out rayCastInfo))
            {
                if (Input.GetMouseButton(0))
                {
                    Debug.Log(rayCastInfo.collider.name + ActiveCam);

                }
            }
        }
    }
}
