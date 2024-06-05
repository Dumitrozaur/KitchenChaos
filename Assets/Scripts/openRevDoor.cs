using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class openRevDoor : MonoBehaviour
{
    [SerializeField] private Animator animDoor;
    private string EXIT_COLL = "exitColide";
    private void OnTriggerEnter(Collider col)
    {
        OpenDoor();
    }

    private void OnTriggerExit(Collider col)
    {
        CloseDoor();
    }
    
    private void OpenDoor()
    {
        if (animDoor != null)
        {
            animDoor.SetBool(EXIT_COLL, true);
        }
    }

    private void CloseDoor()
    {
        if (animDoor != null)
        {
            animDoor.SetBool(EXIT_COLL, false);
            
        }
    }
}
