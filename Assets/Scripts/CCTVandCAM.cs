using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CCTVandCAM : MonoBehaviour
{
    [Header("GameObjects")]
    public GameObject camUnit;

    CamPostProcess takeEffects;

    [Header("values")]
    public float distanceToCCTV;

    [Header("bools")]
    public bool onCCTV;
    public bool canEnterCCTV;
    public bool canExitCCTV;

    public Button nextCam;
    public Button exitButton;

    charMovement charMove;
    objectAnomalies objectFall;
    Raycast takeRay;

    public List<Camera> allCams = new List<Camera>();
    public GameObject CameraCanvas;

    /* ================= AUDIO ================= */
    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip enterCCTVClip;
    public AudioClip exitCCTVClip;
    public AudioClip switchCamClip;
    /* ========================================= */

    void Start()
    {
        CameraCanvas.SetActive(false);
        canEnterCCTV = true;
        onMainCam = true;
        nextCam.gameObject.SetActive(false);
        exitButton.gameObject.SetActive(false);
    }

    void Awake()
    {
        charMove = GameObject.FindWithTag("Player").GetComponent<charMovement>();
        takeRay = GetComponent<Raycast>();
        mainCam = Camera.main;
        takeEffects = GameObject.FindWithTag("Settings").GetComponent<CamPostProcess>();
        objectFall = GetComponent<objectAnomalies>();
    }

    void Update()
    {
        CalculateDistanceCCTV();
        ControlCCTV();
    }

    void CalculateDistanceCCTV()
    {
        distanceToCCTV = Vector3.Distance(charMove.playerCam.position, camUnit.transform.position);
    }

    void ControlCCTV()
    {
        var hit = takeRay.rayCastInfo.collider?.name == "CCTV";
        var keyButton = Input.GetKeyDown(KeyCode.E);

        // ENTER CCTV
        if (hit && distanceToCCTV <= 3 && keyButton && canEnterCCTV)
        {
            PlaySound(enterCCTVClip);

            chooseCamToRay[0] = true;
            CameraCanvas.SetActive(true);
            onCCTV = true;
            canExitCCTV = true;
            canEnterCCTV = false;

            nextCam.gameObject.SetActive(true);
            exitButton.gameObject.SetActive(true);

            Cursor.lockState = CursorLockMode.None;
            StartCoroutine(CameraLogic());
        }
        // EXIT CCTV
        else if (canExitCCTV && keyButton)
        {
            PlaySound(exitCCTVClip);

            CameraCanvas.SetActive(false);
            onCCTV = false;
            canExitCCTV = false;
            canEnterCCTV = true;

            nextCam.gameObject.SetActive(false);
            exitButton.gameObject.SetActive(false);

            Cursor.lockState = CursorLockMode.Locked;
            ExitButton();
        }
    }

    /* ================= CAMERA LOGIC ================= */

    [Header("CAMERALOGIC")]
    public Camera mainCam;

    [Header("bools")]
    public bool canTriggerRepeat;
    public bool onMainCam;
    public bool triggerFortThirth;
    public bool canActive = true;

    [Header("others")]
    public Camera activeCam;
    public Camera previousCam;
    public List<bool> chooseCamToRay = new List<bool>();

    public void ExitButton()
    {
        PlaySound(exitCCTVClip);
        for (int i = 0; i < allCams.Count; i++)
        {
            allCams[i].depth = -2;
        }

        onMainCam = true;
        onCCTV = false;
        canExitCCTV = false;
        canEnterCCTV = true;

        nextCam.gameObject.SetActive(false);
        exitButton.gameObject.SetActive(false);
        CameraCanvas.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;

        for (int i = 0; i < chooseCamToRay.Count; i++)
        {
            chooseCamToRay[i] = false;
        }
    }

    IEnumerator CameraLogic()
    {
        if (onMainCam && canActive)
        {
            PlaySound(switchCamClip);

            chooseCamToRay[0] = true;
            canActive = false;

            takeEffects.ResetAllVals();
            takeEffects.StartingCoro();

            allCams[0].depth = 0;
            onMainCam = false;

            yield return new WaitForSeconds(2f);
            canActive = true;
        }
        else if (allCams[3].depth == 0 && canTriggerRepeat && !onMainCam && canActive)
        {
            PlaySound(switchCamClip);

            chooseCamToRay[3] = false;
            chooseCamToRay[0] = true;

            canActive = false;

            takeEffects.ResetAllVals();
            takeEffects.StartingCoro();

            allCams[3].depth = -2;
            allCams[0].depth = 0;

            canTriggerRepeat = false;

            yield return new WaitForSeconds(2f);
            canActive = true;
        }
        else if (!onMainCam && allCams[0].depth == 0 && canActive)
        {
            PlaySound(switchCamClip);

            chooseCamToRay[0] = false;
            chooseCamToRay[1] = true;

            canActive = false;

            takeEffects.ResetAllVals();
            takeEffects.StartingCoro();

            allCams[0].depth = -2;
            allCams[1].depth = 0;

            triggerFortThirth = true;

            yield return new WaitForSeconds(2f);
            canActive = true;
        }
        else if (!onMainCam && allCams[1].depth == 0 && triggerFortThirth && canActive)
        {
            PlaySound(switchCamClip);

            chooseCamToRay[1] = false;
            chooseCamToRay[2] = true;

            canActive = false;

            takeEffects.ResetAllVals();
            takeEffects.StartingCoro();

            allCams[1].depth = -2;
            allCams[2].depth = 0;

            triggerFortThirth = false;

            yield return new WaitForSeconds(2f);
            canActive = true;
        }
        else if (!onMainCam && allCams[2].depth == 0 && canActive)
        {
            PlaySound(switchCamClip);

            chooseCamToRay[2] = false;
            chooseCamToRay[3] = true;

            canActive = false;

            takeEffects.ResetAllVals();
            takeEffects.StartingCoro();

            allCams[2].depth = -2;
            allCams[3].depth = 0;

            canTriggerRepeat = true;

            yield return new WaitForSeconds(2f);
            canActive = true;
        }
    }

    public void TriggerCamLogic()
    {
        StartCoroutine(CameraLogic());
        objectFall.StartPossibilities();
    }

    /* ================= AUDIO HELPER ================= */

    void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
