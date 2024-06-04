using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorOpenClose : MonoBehaviour
{
    [SerializeField] private Animator doorAnim;
    //[SerializeField] private GameObject roomTwo;
    //[SerializeField] private GameObject playerTag;
    //[SerializeField] private GameObject cubeWhite, cubeBlack;
    private string ENTER_COLL = "enterColide";
    //private roomOneFloor a;

    void OnTriggerEnter(Collider col)
    {
            OpenDoor();
            /*if (doorAnim.GetBool(ENTER_COLL) == true & GameObject.FindWithTag("Player"))
            {
                a.GenerateCubesRoom1(cubeBlack, cubeWhite, roomTwo, 1);
            }*/
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
