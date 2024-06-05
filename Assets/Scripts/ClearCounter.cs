using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField] private GameObject tomatoPrefab;
    [SerializeField] private Transform counterTopPoit;
    
    public void Iteract()
    {
        var spawnedObj = Instantiate(tomatoPrefab, counterTopPoit);
        spawnedObj.transform.localPosition = Vector3.zero;
    }
}
