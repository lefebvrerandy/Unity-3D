using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Level1BGAudio : MonoBehaviour
{
    public static Level1BGAudio Instance;

    public AudioClip Background1;
    public AudioClip Background2;
    public AudioSource MusicSource;
    bool MusicPlaying;
    bool faddedIn;

    [Range(0.0f, 1.0f)]
    public float volume = 1.0f;
    
    void Start()
    {
        MusicSource.clip = Background1;
        PlayBackgroundMusic();
        MusicSource.volume = 0.0f;
        faddedIn = false;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        InitialFadeIn();
    }

    private void InitialFadeIn()
    {
        if (!faddedIn)
        {
            if (MusicSource.volume <= volume)
            {
                MusicSource.volume += 0.001f;
            }
            else
            {
                faddedIn = true;
            }
        }
        else
        {
            MusicSource.volume = volume;
        }
    }

    public void PlayBackgroundMusic()
    {
        MusicSource.Play();
        MusicPlaying = true;
    }

    public void TurnOffBackgroundMusic()
    {
        MusicSource.Stop();
        MusicPlaying = false;
    }
}
