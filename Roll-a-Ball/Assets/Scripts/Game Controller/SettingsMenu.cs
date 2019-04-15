using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    public void SetFullScreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void HomeButton()
    {
        SceneManager.LoadScene(0);
    }

    public void SetMusic(int musicIndex)
    {
        switch (musicIndex)
        {
            case 0:
                Level1BGAudio.Instance.MusicSource.clip = Level1BGAudio.Instance.Background1;
                Level1BGAudio.Instance.PlayBackgroundMusic();
                break;
            case 1:
                Level1BGAudio.Instance.MusicSource.clip = Level1BGAudio.Instance.Background2;
                Level1BGAudio.Instance.PlayBackgroundMusic();
                break;
            default:
                break;
        }
    }

}
