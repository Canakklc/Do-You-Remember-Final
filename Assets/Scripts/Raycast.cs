using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Raycast : MonoBehaviour
{
    public Ray raycast;
    public RaycastHit rayCastInfo;
    public RectTransform cursorUI;
    carCollector takeCarBool;
    public bool canRayCast = true;
    void Awake()
    {
        takeCarBool = GetComponent<carCollector>();
    }



    void Update()
    {
        if (cursorUI == null) return;
        raycast = Camera.main.ScreenPointToRay(cursorUI.position);

        if (Physics.Raycast(raycast, out rayCastInfo) && canRayCast == true)
        {
            if (Input.GetMouseButton(0))
            {
                Debug.Log(rayCastInfo.collider.name);

            }
        }
        if (takeCarBool.inCarControl == true)
        {
            cursorUI.gameObject.SetActive(false);
            canRayCast = false;
        }
        else
        {
            cursorUI.gameObject.SetActive(true);
            canRayCast = true;
        }



    }


}
