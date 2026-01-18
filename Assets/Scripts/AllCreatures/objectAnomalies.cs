using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class objectAnomalies : MonoBehaviour
{
    InterractableObjects takeListStatic;
    HeatDisk takeImage;
    public List<GameObject> objectCanFall = new List<GameObject>();
    public List<Transform> whereToFall = new List<Transform>();
    public List<Vector3> originalObjPos = new List<Vector3>();
    public List<Quaternion> originalRots = new List<Quaternion>();
    public List<bool> boolsToCutAction = new List<bool>();//ont making for once
    public List<bool> checkerForMotionAnimation = new List<bool>();//motion anim set active bool
    public bool canTransformPos = true;//for static obj generate after collected

    public int Index;
    public int maxValue = 100;
    public bool canCreateNewRandom = true;
    /// <summary>
    /// Setting the ghost type from the beginning
    /// </summary>
    public List<bool> setCreatureType = new List<bool>();//1- ghost 2-Poltergeist 3-Demon
    public int determineCreatureInt;

    void Start()
    {
        Index = 50;
        for (int i = 0; i < objectCanFall.Count; i++)
        {
            originalObjPos.Add(objectCanFall[i].transform.position);
            originalRots.Add(objectCanFall[i].transform.rotation);
            boolsToCutAction.Add(false);
        }
        determineCreatureInt = UnityEngine.Random.Range(1, 4);
        for (int i = 0; i < 3; i++)
        {
            setCreatureType.Add(false);
        }
        if (determineCreatureInt == 1)
        {
            setCreatureType[0] = true; //means ghost active
        }
        else if (determineCreatureInt == 2)
        {
            setCreatureType[1] = true; // Poltergeist active
        }
        else
        {
            setCreatureType[2] = true;//Poltergeist active
        }

    }
    void Awake()
    {
        takeImage = GetComponent<HeatDisk>();
        takeListStatic = GetComponent<InterractableObjects>();
    }
    void Update()
    {
        ImageDiskAppearAnim();
    }
    IEnumerator PossibilityPolWithObjects()
    {
        Index = UnityEngine.Random.Range(0, maxValue);
        if (Index >= 0 && Index <= 10 && canTransformPos == true)
        {
            if (setCreatureType[1] == true)
            {
                Debug.Log(Index);
                if (Index < 3 && boolsToCutAction[0] == false) //book first in list //put checker here!!!(ghost type)
                {
                    canCreateNewRandom = false;
                    objectCanFall[0].transform.position = whereToFall[0].transform.position;
                    objectCanFall[0].transform.rotation = Quaternion.Euler(-24.721f, 0, 0);
                    boolsToCutAction[0] = true;
                    Debug.Log("Book pos changed");
                    checkerForMotionAnimation[0] = true;
                    canTransformPos = false;
                }
                else if (Index >= 3 && Index < 7 && boolsToCutAction[1] == false)//paper cond;
                {
                    canCreateNewRandom = false;
                    objectCanFall[1].transform.position = whereToFall[1].transform.position;
                    objectCanFall[1].transform.rotation = Quaternion.Euler(0, 180, 0);
                    boolsToCutAction[1] = true;
                    checkerForMotionAnimation[1] = true;
                    canTransformPos = false;
                    Debug.Log("Paper pos changed");
                }
                else //kettle
                {
                    if (boolsToCutAction[2] == false)
                    {
                        canCreateNewRandom = false;
                        objectCanFall[2].transform.position = whereToFall[2].transform.position;
                        objectCanFall[2].transform.rotation = Quaternion.Euler(2.401f, -5.953f, -95.849f);
                        Debug.Log("Kettle moved");
                        boolsToCutAction[2] = true;
                        checkerForMotionAnimation[2] = true;
                        canTransformPos = false;
                    }
                }
                yield return new WaitForSeconds(2f);
                canCreateNewRandom = true;
            }
        }
        else if (Index > 10)
        {
            {
                canCreateNewRandom = false;
                Debug.Log("out of range unlucky m8" + Index);
                yield return new WaitForSeconds(2f);
                canCreateNewRandom = true;
            }
        }

    }
    public void ImageDiskAppearAnim()
    {
        if (takeImage.motionSensor)
        {
            if (checkerForMotionAnimation[0] == true || checkerForMotionAnimation[1] == true || checkerForMotionAnimation[2] == true)
            {
                takeImage.motionSensorImage.SetActive(true);
                if (checkerForMotionAnimation[0] == true && checkerForMotionAnimation[1] == true && checkerForMotionAnimation[2] == true)
                {
                    takeImage.motionSensorImage.SetActive(false);
                }
            }
            else
            {
                takeImage.motionSensorImage.SetActive(false);
            }
        }
    }




    public void StartPossibilities()
    {
        if (canCreateNewRandom == true)
        {
            StartCoroutine(PossibilityPolWithObjects());
        }
    }

}
