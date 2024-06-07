using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorOpenClose : MonoBehaviour
{
    [SerializeField] private Animator doorAnim;
    [SerializeField] private GameObject animTrigg;
    private string ENTER_COLL = "enterColide";
    [SerializeField] private GameObject room;
    

    void OnTriggerEnter(Collider col)
    {
        
        OpenDoor();
    }

    void OnTriggerExit(Collider col)
    {
        
        CloseDoor();
        ShowFloor();
        
    }

    private void OpenDoor()
    {
        if (doorAnim != null)
        {
            doorAnim.SetBool(ENTER_COLL, true);
        }
    }

    private void CloseDoor()
    {
        if (doorAnim != null)
        {
            doorAnim.SetBool(ENTER_COLL, false);
            
        }
    }

    private void ShowFloor()
    {
         room.SetActive(true);
    }
}
