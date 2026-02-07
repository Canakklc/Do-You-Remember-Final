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

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip hoverSound;
    public AudioClip markedSound;

    bool hoverPlayed = false;

    void Awake()
    {
        takeRay = GetComponent<Raycast>();
    }

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
        distance = Vector3.Distance(playerMesh.transform.position, board.transform.position);

        getRaycast();

        if (!takeRay.rayCastInfo.collider)
        {
            hoverPlayed = false;
            return;
        }

        string n = takeRay.rayCastInfo.collider.name;

        isGhost = n == "GhostTrigger";
        isDemon = n == "DemonTrigger";
        isPolger = n == "PolterTrigger";

        // MARK
        if (Input.GetMouseButtonDown(0))
        {
            if ((isGhost || isDemon || isPolger) && !Marked && distance < 2)
            {
                Marked = true;
                audioSource.PlayOneShot(markedSound);
            }
        }
    }

    void getRaycast()
    {
        bool cond = distance < 2;

        if (takeRay.rayCastInfo.collider == null)
            return;

        if (Marked == false && cond)
        {
            if (!hoverPlayed &&
                (takeRay.rayCastInfo.collider.name == "GhostTrigger" ||
                 takeRay.rayCastInfo.collider.name == "PolterTrigger" ||
                 takeRay.rayCastInfo.collider.name == "DemonTrigger"))
            {
                audioSource.PlayOneShot(hoverSound);
                hoverPlayed = true;
            }
        }

        if (takeRay.rayCastInfo.collider.name == "GhostTrigger" && !Marked && cond)
        {
            SetAlpha(0, 100);
        }
        else if (takeRay.rayCastInfo.collider.name == "PolterTrigger" && !Marked && cond)
        {
            SetAlpha(1, 100);
        }
        else if (takeRay.rayCastInfo.collider.name == "DemonTrigger" && !Marked && cond)
        {
            SetAlpha(2, 100);
        }
        else
        {
            if (!Marked)
            {
                ResetAllAlpha();
                hoverPlayed = false;
            }
        }
    }

    void SetAlpha(int index, float alpha)
    {
        Color c = Circles[index].color;
        c.a = alpha;
        Circles[index].color = c;
    }

    void ResetAllAlpha()
    {
        for (int i = 0; i < Circles.Count; i++)
        {
            Color c = Circles[i].color;
            c.a = 0;
            Circles[i].color = c;
        }
    }
}
