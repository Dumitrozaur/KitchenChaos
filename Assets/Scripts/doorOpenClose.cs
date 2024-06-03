using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorOpenClose : MonoBehaviour
{
    [SerializeField] private Animator doorAnim;
    private Animator animator;
    private string ENTER_COLL = "enterColide";
    private string EXIT_COL = "exitColide";
    

    void OnTriggerEnter(Collider col)
    {
            OpenDoor();
    }
    
    void OnTriggerExit(Collider col)
    {

            CloseDoor();
        
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
}
