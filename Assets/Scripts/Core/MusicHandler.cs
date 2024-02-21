using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicHandler : MonoBehaviour
{
    private AudioSource _musicSource;
    public static MusicHandler Singleton { get; private set; }

    private void Awake()
    {
        if (_musicSource == null)
        {
            _musicSource = GetComponent<AudioSource>();  
            Singleton = this;
        }
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    public void SetVolume(float value)
    {
        _musicSource.volume = value;
    }
    public void ChangeMusic(AudioClip clip)
    {
        _musicSource.clip = clip;
        _musicSource.Play();
    }
}
