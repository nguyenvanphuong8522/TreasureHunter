using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] private AudioSource musicSource, sfxSource;
    [SerializeField] private Sound[] musicSounds, sfxSounds;
    public AudioSource[] soundSources;
    private Queue<AudioSource> _queueSources;
    private bool IsSoundOn;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            _queueSources = new Queue<AudioSource>(soundSources);
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void ChangeMusicVolume()
    {
        if (musicSource != null)
        {
            musicSource.mute = !IsSoundOn;
            //musicSource.volume = UserData.Music;
        }
    }
    private void Start()
    {
        IsSoundOn = true;
        PlayMusic("theme");
    }

    public void PlayMusic(string name)
    {
        Sound sound = Array.Find(musicSounds, x => x.name == name);

        if(sound != null)
        {
            musicSource.clip = sound.clip;
            musicSource.Play();
        }
        else
        {
            Debug.Log("not found sound");
        }
    }
    public void PlaySfx(string name)
    {
        Sound sfx = Array.Find(sfxSounds, x => x.name == name);


        var source = _queueSources.Dequeue();
        if (!source)
        {
            return;
        }
        
        if (sfx != null)
        {
            source.PlayOneShot(sfx.clip);
            _queueSources.Enqueue(source);
        }
        else
        {
            Debug.Log("not found sfx");
        }
    }

    public void ButtonClick()
    {
        PlaySfx("btnClick");
    }
}
