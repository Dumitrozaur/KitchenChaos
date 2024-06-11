using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour
{
    [SerializeField] private PlateCounter plateCounter;
    [SerializeField] private GameObject plateVisual;
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private float plateDistanceY = .1f;

    private List<GameObject> plateVisuals;

    private void Awake()
    {
        plateVisuals = new List<GameObject>();
    }

    private void Start()
    {
        plateCounter.OnPlateSpawned += OnPlateSpawnedVisual_EventArgs;
        plateCounter.OnPlateRemoved += OnPlateRemovedVisual_EventArgs;
    }

    private void OnPlateRemovedVisual_EventArgs(object sender, EventArgs e)
    {
        GameObject plate = plateVisuals[plateVisuals.Count - 1];

        plateVisuals.Remove(plate);
        
        Destroy(plate);
    }

    private void OnPlateSpawnedVisual_EventArgs(object sender, EventArgs e)
    {
        GameObject plate = Instantiate(plateVisual, counterTopPoint);
        plateVisuals.Add(plate);

        plate.transform.position = new Vector3(plate.transform.position.x, plate.transform.position.y + plateDistanceY * plateVisuals.Count,
            plate.transform.position.z);
    }
}
