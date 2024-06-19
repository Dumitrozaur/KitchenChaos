using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class VolumeControler : MonoBehaviour
{
    private const String PLAYER_PREFS_VOLUME_MUSIC = "BackroundMusicVolume";
    private const String PLAYER_PREFS_VOLUME_SFX = "EffectsVolume";

    
    [SerializeField] private Slider sfxVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private AudioSource backroundMusic;

    private void Start()
    {
        musicVolumeSlider.onValueChanged.AddListener(ChangeMusicVolume);
        sfxVolumeSlider.onValueChanged.AddListener(ChangeSfxVolume);
    }

    public void UpdateSliders()
    {
        musicVolumeSlider.value = PlayerPrefs.GetFloat(PLAYER_PREFS_VOLUME_MUSIC, 1f);
        sfxVolumeSlider.value = PlayerPrefs.GetFloat(PLAYER_PREFS_VOLUME_SFX, 1f);
        
    }

    private void ChangeSfxVolume(float volume)
    {
        SoundManager.Instance.EffectsVolume = volume;
        
        PlayerPrefs.SetFloat(PLAYER_PREFS_VOLUME_SFX, volume);
        PlayerPrefs.Save();
    }

    private void ChangeMusicVolume(float volume)
    {
        backroundMusic.volume = volume;
        
        PlayerPrefs.SetFloat(PLAYER_PREFS_VOLUME_MUSIC, volume);
        PlayerPrefs.Save();

    }
    
}
