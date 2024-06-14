using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class cameraTrigg : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera room1;
    [SerializeField] private CinemachineVirtualCamera room2;

    private void Start()
    {
        Physics.IgnoreLayerCollision(0,8);
    }

    private void OnTriggerExit(Collider col)
    {
        if(room1.enabled == true)
        {
            room1.enabled = false;
            room2.enabled = true;
        }
        else
        {
            room2.enabled = false;
            room1.enabled = true;
        }              
    }
}
