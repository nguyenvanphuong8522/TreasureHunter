using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] private AudioSource musicSource, sfxSource;
    [SerializeField] private Sound[] musicSounds, sfxSounds;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
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

        if(sfx != null)
        {
            sfxSource.clip = sfx.clip;
            sfxSource.Play();
        }
        else
        {
            Debug.Log("not found sfx");
        }
    }
}
