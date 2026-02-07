using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DiskGather : MonoBehaviour
{
    Raycast takeRay;
    Quaternion fixRot = Quaternion.Euler(-90f, 0f, 0f);

    public List<GameObject> Disks = new List<GameObject>();

    // coppied disks
    GameObject firstDiskCopy;
    GameObject secDiskCopy;
    GameObject thirdDiskCopy;

    public List<bool> GrabbedDisks = new List<bool>();
    public List<bool> ActivateCamFunc = new List<bool>();

    public int maxDiskToCarry;

    public Transform diskPlaceToInstantiate;
    public Transform DiskPlaceSec;
    public Transform DiskPlaceThird;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip pickUpSound;
    public AudioClip insertSound;

    void Awake()
    {
        takeRay = GetComponent<Raycast>();
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            PickUpDisk();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            InsertDisk();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetAllValues();
        }
    }

    void PickUpDisk()
    {
        if (takeRay == null) return;
        if (takeRay.rayCastInfo.collider == null) return;

        GameObject diskOnCursor = takeRay.rayCastInfo.collider.gameObject;

        if (diskOnCursor == Disks[0] && maxDiskToCarry < 1 && InterractableObjects.memoryCollect >= 1)
        {
            InterractableObjects.memoryCollect -= 1;
            maxDiskToCarry += 1;
            GrabbedDisks[0] = true;
            Disks[0].SetActive(false);

            audioSource.PlayOneShot(pickUpSound);
        }

        if (diskOnCursor == Disks[1] && maxDiskToCarry < 1 && InterractableObjects.memoryCollect >= 2)
        {
            InterractableObjects.memoryCollect -= 2;
            maxDiskToCarry += 1;
            GrabbedDisks[1] = true;
            Disks[1].SetActive(false);

            audioSource.PlayOneShot(pickUpSound);
        }

        if (diskOnCursor == Disks[2] && maxDiskToCarry < 1 && InterractableObjects.memoryCollect >= 3)
        {
            InterractableObjects.memoryCollect -= 3;
            maxDiskToCarry += 1;
            GrabbedDisks[2] = true;
            Disks[2].SetActive(false);

            audioSource.PlayOneShot(pickUpSound);
        }
    }

    void InsertDisk()
    {
        for (int i = 0; i < GrabbedDisks.Count; i++)
        {
            if (GrabbedDisks[i] == false) continue;

            if (GrabbedDisks[0] == true)
            {
                firstDiskCopy = Instantiate(Disks[0], diskPlaceToInstantiate.position, diskPlaceToInstantiate.rotation * fixRot);
                firstDiskCopy.SetActive(true);

                maxDiskToCarry -= 1;
                GrabbedDisks[0] = false;
                ActivateCamFunc[0] = true;

                audioSource.PlayOneShot(insertSound);
            }
            else if (GrabbedDisks[1] == true)
            {
                secDiskCopy = Instantiate(Disks[1], DiskPlaceSec.position, DiskPlaceSec.rotation * fixRot);
                secDiskCopy.SetActive(true);

                maxDiskToCarry -= 1;
                GrabbedDisks[1] = false;
                ActivateCamFunc[1] = true;

                audioSource.PlayOneShot(insertSound);
            }
            else if (GrabbedDisks[2] == true)
            {
                thirdDiskCopy = Instantiate(Disks[2], DiskPlaceThird.position, DiskPlaceThird.rotation * fixRot);
                thirdDiskCopy.SetActive(true);

                maxDiskToCarry -= 1;
                GrabbedDisks[2] = false;
                ActivateCamFunc[2] = true;

                audioSource.PlayOneShot(insertSound);
            }
        }
    }

    void ResetAllValues() // reseting for the next scene
    {
        for (int i = 0; i < Disks.Count; i++)
        {
            Disks[i].SetActive(true);
        }

        for (int i = 0; i < GrabbedDisks.Count; i++)
        {
            GrabbedDisks[i] = false;
        }

        for (int i = 0; i < ActivateCamFunc.Count; i++)
        {
            ActivateCamFunc[i] = false;
        }

        maxDiskToCarry = 0;

        Destroy(firstDiskCopy);
        Destroy(secDiskCopy);
        Destroy(thirdDiskCopy);

        Debug.Log("reset working");
    }
}
