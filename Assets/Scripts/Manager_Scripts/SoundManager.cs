using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    [SerializeField] private AudioClipsRefsSO audioClipsRefsSo;
    public float EffectsVolume = 1f;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSuccess += InstanceSuccess_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailed += InstanceFailed_OnRecipeFailed;
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        Player.Instance.OnPickedSomething += PickedSomething_Player;
        BaseCounter.OnAnyObjectPlacedHere += AnyPlacedObject_BaseCounterPlace;
        TrashCan.OnAnyObjectTrahsed += TrashCanOn_OnAnyObjectTrahsed;
    }

    private void TrashCanOn_OnAnyObjectTrahsed(object sender, EventArgs e)
    {
        TrashCan trashCan = sender as TrashCan;
        PlaySound(audioClipsRefsSo.objectDrop, trashCan.transform.position);
    }

    private void AnyPlacedObject_BaseCounterPlace(object sender, EventArgs e)
    {
        BaseCounter baseCounter = sender as BaseCounter;
        PlaySound(audioClipsRefsSo.objectDrop, baseCounter.transform.position);
    }

    private void PickedSomething_Player(object sender, EventArgs e)
    {
        PlaySound(audioClipsRefsSo.objectPickup, Player.Instance.transform.position);
    }

    private void CuttingCounter_OnAnyCut(object sender, EventArgs e)
    {
        CuttingCounter cuttingCounter = sender as CuttingCounter;
        PlaySound(audioClipsRefsSo.chop, cuttingCounter.transform.position);
    }

    private void InstanceFailed_OnRecipeFailed(object sender, EventArgs e)
    {
        PlaySound(audioClipsRefsSo.deliveryFail, Camera.main.transform.position);
    }

    private void InstanceSuccess_OnRecipeSuccess(object sender, EventArgs e)
    {
        PlaySound(audioClipsRefsSo.deliverySuccess, Camera.main.transform.position);
    }

    private void PlaySound(AudioClip[] audioCliparray, Vector3 position, float volume = 1f)
    {
           PlaySound(audioCliparray[Random.Range(0, audioCliparray.Length)], position, volume);
    }
    
    private void PlaySound(AudioClip audioClip, Vector3 position, float volume = 1f)
    {
           AudioSource.PlayClipAtPoint(audioClip, position, volume * EffectsVolume);
    }
    
}
