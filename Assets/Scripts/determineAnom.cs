using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class determineAnom : MonoBehaviour
{
    Raycast takeRay;
    public List<RawImage> Circles = new List<RawImage>();
    public GameObject playerMesh;
    public GameObject board;
    float distance;
    Color colors;
    public bool Marked = false;
    bool isGhost;
    bool isPolger;
    bool isDemon;


    void Start()
    {
        for (int i = 0; i < Circles.Count; i++)
        {
            colors = Circles[i].color;
            colors.a = 0;
            Circles[i].color = colors;
        }


    }
    void Update()
    {
        getRaycast();
        if (!takeRay.rayCastInfo.collider) return;
        string n = takeRay.rayCastInfo.collider.name;

        isGhost = n == "GhostTrigger";
        isDemon = n == "DemonTrigger";
        isPolger = n == "PolterTrigger";
        if (Input.GetMouseButton(0))
        {
            if (isGhost || isDemon || isPolger)
            {
                Marked = true;
            }
        }
        distance = Vector3.Distance(playerMesh.transform.position, board.transform.position);

    }
    void Awake()
    {
        takeRay = GetComponent<Raycast>();
    }

    void getRaycast()
    {
        bool cond = distance < 2;
        if (takeRay.rayCastInfo.collider == null) return;
        if (takeRay.rayCastInfo.collider.name == "GhostTrigger" && Marked == false && cond)
        {
            Color colorFirst = Circles[0].color;
            colorFirst.a = 100;
            Circles[0].color = colorFirst;
        }
        else if (takeRay.rayCastInfo.collider.name == "PolterTrigger" && Marked == false && cond)
        {
            Color colorSec = Circles[1].color;
            colorSec.a = 100;
            Circles[1].color = colorSec;
        }
        else if (takeRay.rayCastInfo.collider.name == "DemonTrigger" && Marked == false && cond)
        {
            Color colorThird = Circles[2].color;
            colorThird.a = 100;
            Circles[2].color = colorThird;
        }
        else
        {
            if (Marked == false)
            {
                for (int i = 0; i < Circles.Count; i++)
                {
                    Color allReset = Circles[i].color;
                    allReset.a = 0;
                    Circles[i].color = allReset;
                }
            }
        }


    }
}
