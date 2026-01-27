using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class anomalyCollectEffect : MonoBehaviour
{
    PostProcessVolume volume;
    ChromaticAberration Chromatic;
    Grain grain;
    Vignette vignette;
    void Awake()
    {
        volume = GameObject.FindWithTag("Settings").GetComponentInChildren<PostProcessVolume>();
        volume.profile = Instantiate(volume.profile);
        volume.profile.TryGetSettings(out Chromatic);
        volume.profile.TryGetSettings(out grain);
        volume.profile.TryGetSettings(out vignette);
    }

    IEnumerator ChromaticEffect()
    {
        float Duration = 1;
        float Elapsed = 0;
        float chromaticMax = 1f;
        while (Elapsed < Duration)
        {
            Elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(Elapsed / Duration);
            t = Mathf.SmoothStep(0f, 1f, t);
            Chromatic.intensity.overrideState = true;
            Chromatic.intensity.value = chromaticMax;//chromatic start
            Chromatic.intensity.value = Mathf.Lerp(chromaticMax, 0f, t);
            yield return null;
        }

    }
    IEnumerator GraintEffect()
    {
        float Duration = 2;
        float Elapsed = 0;
        float graintMax = 1f;
        float grainSizeMax = 3f;
        float roundMaxSize = 1f;
        while (Elapsed < Duration)
        {
            Elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(Elapsed / Duration);
            t = t * t;
            t = Mathf.SmoothStep(0f, 1f, t);
            grain.intensity.overrideState = true;
            grain.size.overrideState = true;
            vignette.roundness.overrideState = true;
            vignette.roundness.value = roundMaxSize;
            grain.intensity.value = graintMax;
            grain.size.value = grainSizeMax;
            grain.intensity.value = Mathf.Lerp(graintMax, 0.6f, t);
            grain.size.value = Mathf.Lerp(grainSizeMax, 1.63f, t);
            vignette.roundness.value = Mathf.Lerp(roundMaxSize, 0.269f, t);
            yield return null;
        }
        Debug.Log("Grain working");
    }

    public void CallChromaticEffect()
    {
        StartCoroutine(ChromaticEffect());
        StartCoroutine(GraintEffect());
    }
}
