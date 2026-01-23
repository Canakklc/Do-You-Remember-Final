using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class anomalyCollectEffect : MonoBehaviour
{
    PostProcessVolume volume;
    ChromaticAberration Chromatic;
    void Awake()
    {
        volume = GameObject.FindWithTag("Settings").GetComponentInChildren<PostProcessVolume>();
        volume.profile = Instantiate(volume.profile);

        volume.profile.TryGetSettings(out Chromatic);
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
            Chromatic.intensity.value = chromaticMax;
            Chromatic.intensity.value = Mathf.Lerp(chromaticMax, 0f, t);
            yield return null;

        }

    }

    public void CallChromaticEffect()
    {
        StartCoroutine(ChromaticEffect());
    }
}
