using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class NewBehaviourScript1 : MonoBehaviour
{
    [SerializeField] private AudioClipsRefsSO audioClipsRefsSo;
    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSuccess += InstanceSuccess_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailed += InstanceFailed_OnRecipeFailed;

    }

    private void InstanceFailed_OnRecipeFailed(object sender, EventArgs e)
    {
        
    }

    private void InstanceSuccess_OnRecipeSuccess(object sender, EventArgs e)
    {
        
    }

    private void PlaySound(AudioClip[] audioCliparray, Vector3 position, float volume = 1f)
    {
           PlaySound(audioCliparray[Random.Range(0, audioCliparray.Length)], position, volume);
    }
    
    private void PlaySound(AudioClip audioClip, Vector3 position, float volume = 1f)
    {
           
    }
}
