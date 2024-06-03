using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walking : MonoBehaviour
{
    [SerializeField] private Player player;
    public const string IS_WALKING = "isWalking";
    private Animator animator;
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
        
    }

    private void Update()
    {
        animator.SetBool(IS_WALKING, player.Walking());
    }
}
