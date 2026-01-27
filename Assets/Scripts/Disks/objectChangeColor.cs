using System.Collections.Generic;
using UnityEngine;

public class ObjectChangeColor : MonoBehaviour
{
    public HeatDisk thermalCheck;

    public List<GameObject> objects = new List<GameObject>();
    private List<Color> originalColors = new List<Color>();
    private List<Material> originalMats = new List<Material>();
    public Material Trying;
    //Change thermal color anomaly objects
    objectAnomalies checkerCreatureType;
    public List<GameObject> thermalObjAnoms = new List<GameObject>();
    public List<Material> thermalObjOriginalAnoms = new List<Material>();
    public float timeTillAnom;

    void Start()
    {

        for (int i = 0; i < objects.Count; i++)
        {
            Renderer r = objects[i].GetComponent<Renderer>();
            originalColors.Add(r.material.color);
            originalMats.Add(r.material);
        }
        for (int j = 0; j < thermalObjAnoms.Count; j++)
        {
            Renderer r = thermalObjAnoms[j].GetComponent<Renderer>();
            thermalObjOriginalAnoms.Add(r.material);
        }
    }
    void Awake()
    {
        checkerCreatureType = GetComponent<objectAnomalies>();
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
        //Anom Part
        timeTillAnom += Time.deltaTime;
        ThermalAnomalyColorChange();
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
        for (int j = 0; j < thermalObjAnoms.Count; j++)
        {
            Renderer r = thermalObjAnoms[j].GetComponent<Renderer>();
            r.material = thermalObjOriginalAnoms[j];
        }
    }

    void ThermalAnomalyColorChange() // for polger lvl
    {
        if (!checkerCreatureType.setCreatureType[1]) return;
        for (int i = 0; i < thermalObjAnoms.Count; i++)
        {
            Renderer r = thermalObjAnoms[i].GetComponent<Renderer>();
            if (thermalCheck.thermalActive == true)
            {
                if (timeTillAnom > 100f)
                {
                    r.material = Trying;
                }
            }
        }
    }
}
