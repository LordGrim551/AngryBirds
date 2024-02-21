using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicBG : MonoBehaviour
{
    public AudioClip bgMusics;
    public float maxTime;

    private float currentTime;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = AudioManager.instance.PlayAudioOnLoop(bgMusics);
    }

    void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime >= maxTime)
        {
            currentTime = 0;
            audioSource.Stop();
            audioSource = AudioManager.instance.PlayAudioOnLoop(bgMusics);
        }
    }

    public void PauseMusic()
    {
        audioSource.Pause();
    }

    public void ResumeMusic()
    {
        audioSource.UnPause();
    }
}

