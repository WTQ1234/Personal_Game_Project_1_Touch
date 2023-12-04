using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HRL;

public class SoundManager : MonoSingleton<SoundManager>
{
    [SerializeField] AudioSource sfxPlayer_Music;
    [SerializeField] AudioSource sfxPlayer;

    public void SwitchBGM(AudioClip audioClip)
    {
        sfxPlayer_Music.clip = audioClip;
        sfxPlayer_Music.Play();
    }

    public void PlaySfx(AudioClip audioClip)
    {
        sfxPlayer.PlayOneShot(audioClip);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
