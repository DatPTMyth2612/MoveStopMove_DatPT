using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager> 
{
    [SerializeField] private AudioSource effectsSource;
    [SerializeField] internal AudioClip buttonAudioClip;
    [SerializeField] internal AudioClip attackAudioClip;
    [SerializeField] internal AudioClip dieAudioClip;
    [SerializeField] internal AudioClip levelUpAudioClip;
    public void PlaySound(AudioClip clip)
    {
        effectsSource.PlayOneShot(clip);
    }
}
