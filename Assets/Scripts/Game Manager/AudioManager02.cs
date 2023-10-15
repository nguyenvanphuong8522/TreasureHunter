using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager02 : MonoBehaviour
{
    public static AudioManager02 instance;
    public AudioSource musicSource;
    public AudioSource[] sfxSources;
    private Queue<AudioSource> _queueSources;

    public List<Sound> sfxs;
    public void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
            _queueSources = new Queue<AudioSource>(sfxSources);
        }
    }


    public void PlayShot(AudioClip clip)
    {
        if (clip == null)
        {
            return;
        }
        var source = _queueSources.Dequeue();
        if (!source)
        {
            return;
        }
        source.PlayOneShot(clip);
        _queueSources.Enqueue(source);
    }
}
