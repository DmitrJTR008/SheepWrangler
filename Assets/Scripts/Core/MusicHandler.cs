using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicHandler : MonoBehaviour
{
    private AudioSource _musicSource;
    public static MusicHandler Singleton { get; private set; } 

    private void Awake()
    {
        if (Singleton == null)
        {
            Singleton = this;
            _musicSource = GetComponent<AudioSource>();  
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
