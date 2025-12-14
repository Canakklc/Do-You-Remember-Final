using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class justGhost : MonoBehaviour
{
    public List<GameObject> roomLights = new List<GameObject>();
    objectAnomalies takeIndex;
    public bool canCreateRandom = true;

    [Header("ghost assigments")]
    public List<GameObject> Screens = new List<GameObject>();//tvScreenEtc
    public GameObject Ghost;
    GameObject spawnedGhost;
    public float timeForGhost;
    public Transform ghostStartPos;
    public Transform ghostEndPos;
    public bool onlyOnce = false;


    void Start()
    {
        for (int i = 0; i < Screens.Count; i++)
        {
            Screens[i].SetActive(false);
        }

    }
    void Update()
    {
        ChooseRandomLamp();
        ChooseRandomDevice();
        GhostSpawn();

        timeForGhost += Time.deltaTime;
    }

    void Awake()
    {
        takeIndex = GetComponent<objectAnomalies>();
    }

    void ChooseRandomLamp()//ghostLamp
    {
        if (takeIndex.setCreatureType[0] == true)
        {

            if (takeIndex.Index >= 0 && takeIndex.Index <= 10)
            {
                if (takeIndex.Index < 3)
                {
                    roomLights[0].SetActive(false);
                    Debug.Log("LightShouldOFF" + takeIndex.Index);
                }
                else if (takeIndex.Index >= 3 && takeIndex.Index > 7)
                {
                    roomLights[1].SetActive(false);
                    Debug.Log("SecShouldOff");
                }
                else
                {
                    roomLights[2].SetActive(false);
                    Debug.Log("thirthShouldOff");
                }
            }
        }
    }
    void ChooseRandomDevice()//reaching electric devices
    {
        if (takeIndex.setCreatureType[0] == true)
        {
            if (takeIndex.Index >= 10 && takeIndex.Index <= 20)
            {
                if (takeIndex.Index < 15)
                {
                    Screens[0].SetActive(true);
                }
                else
                {
                    Screens[1].SetActive(true);
                }
            }
        }
    }
    void GhostSpawn()//ghostMove
    {
        if (takeIndex.setCreatureType[0] == true && onlyOnce == false)
        {
            if (timeForGhost >= 20f)
            {
                spawnedGhost = Instantiate(Ghost, ghostStartPos.position, ghostStartPos.rotation);
                onlyOnce = true;
                StartCoroutine(MoveGhost(spawnedGhost));
            }
        }
    }
    IEnumerator MoveGhost(GameObject Ghost)
    {
        float Duration = 5;
        float Elapsed = 0;
        var startPos = ghostStartPos.transform.position;
        var endPos = ghostEndPos.transform.position;

        while (Elapsed < Duration)
        {
            Elapsed += Time.deltaTime;
            float t = Elapsed / Duration;
            t = Mathf.SmoothStep(0f, 1f, t);
            spawnedGhost.transform.position = Vector3.Lerp(startPos, endPos, t);
            yield return null;

        }
        yield return new WaitForSeconds(6f);
        Destroy(spawnedGhost);


    }




}






