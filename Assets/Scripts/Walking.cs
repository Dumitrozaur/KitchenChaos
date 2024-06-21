using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode.Components;
using UnityEngine;

public class Walking : NetworkAnimator
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
        if (!IsOwner)
        {
            return;
        }
        animator.SetBool(IS_WALKING, player.Walking());
    }
}
