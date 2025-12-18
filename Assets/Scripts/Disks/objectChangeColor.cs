using System.Collections.Generic;
using UnityEngine;

public class ObjectChangeColor : MonoBehaviour
{
    public HeatDisk thermalCheck;

    public List<GameObject> objects = new List<GameObject>();
    private List<Color> originalColors = new List<Color>();
    private List<Material> originalMats = new List<Material>();
    public Material Trying;

    void Start()
    {

        for (int i = 0; i < objects.Count; i++)
        {
            Renderer r = objects[i].GetComponent<Renderer>();
            originalColors.Add(r.material.color);
            originalMats.Add(r.material);
        }
    }

    void Update()
    {
        if (thermalCheck == null) return;

        if (thermalCheck.thermalActive)
        {
            ApplyThermal();
        }
        else
        {
            RestoreOriginal();
        }
    }

    void ApplyThermal()
    {
        for (int i = 0; i < objects.Count; i++)
        {
            Renderer r = objects[i].GetComponent<Renderer>();
            //r.material.color = Color.yellow;
            r.material = Trying;
            r.material.EnableKeyword("_EMISSION");
            // r.material.SetColor("_EmissionColor", Color.yellow * 0.1f);

        }
    }

    void RestoreOriginal()
    {
        for (int i = 0; i < objects.Count; i++)
        {
            Renderer r = objects[i].GetComponent<Renderer>();
            r.material.color = originalColors[i];
            r.material = originalMats[i];
            r.material.DisableKeyword("_EMISSION");
        }
    }
}
