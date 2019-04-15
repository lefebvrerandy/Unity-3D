using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingPole : MonoBehaviour
{
    static public EndingPole ep;
    public AudioClip BackGround;
    public AudioSource MusicSource;
    bool MusicPlaying;
    public bool endingSound = false;

    [Range(0.0f, 1.0f)]
    public float volume = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        ep = this;
        MusicSource.clip = BackGround;
        MusicSource.volume = volume;
    }

    private void Update()
    {
        if (endingSound)
        {
            if (!MusicPlaying)
            {
                PlayEndingSound();
            }
        }
    }

    private void PlayEndingSound()
    {
        MusicSource.Play();
        MusicPlaying = true;
    }

    private void TurnOffBackgroundMusic()
    {
        MusicSource.Stop();
        MusicPlaying = false;
    }
}
