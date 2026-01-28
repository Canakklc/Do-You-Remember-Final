using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class carPostProcess : MonoBehaviour
{
    PostProcessVolume effect;
    ColorGrading colorGrading;
    Grain grain;


    void Awake()
    {
        effect = GameObject.FindWithTag("This Car").GetComponent<PostProcessVolume>();
        effect.profile = Instantiate(effect.profile);
        effect.profile.TryGetSettings<ColorGrading>(out colorGrading);
        effect.profile.TryGetSettings<Grain>(out grain);

    }
    IEnumerator PostExposureCar()
    {
        float Duration = 1f;
        float Elapsed = 0f;
        float maxPostVal = -10f;
        float minPostVal = -0f;
        colorGrading.postExposure.overrideState = true;
        while (Elapsed < Duration)
        {
            Elapsed += Time.deltaTime;
            float t = Elapsed / Duration;

            t = Mathf.SmoothStep(0f, 1f, t);
            colorGrading.postExposure.value = Mathf.Lerp(maxPostVal, minPostVal, t);
            yield return null;

        }
        colorGrading.postExposure.value = minPostVal;

    }
    IEnumerator Grain()
    {
        float Duration = 1f;
        float Elapsed = 0f;
        float maxInt = 1f;
        float maxSize = 3f;
        grain.intensity.value = maxInt;
        grain.size.value = maxSize;
        yield return new WaitForSeconds(2f);
        while (Elapsed < Duration)
        {
            Elapsed += Time.deltaTime;
            float t = Elapsed / Duration;
            t = t * t;
            t = Mathf.SmoothStep(0f, 1f, t);
            grain.intensity.value = Mathf.Lerp(maxInt, 0.46f, t);
            grain.size.value = Mathf.Lerp(maxSize, 1.3f, t);
            yield return null;
        }
    }
    public void StartEffects()
    {
        StartCoroutine(PostExposureCar());
        StartCoroutine(Grain());
    }

}
