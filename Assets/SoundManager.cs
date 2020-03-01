using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource), typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    //Static - so it can be accessed by other classes in code.
    public static SoundManager instance = null;

    [Space]
    [Header("AudioSources")]
    [Tooltip("Background Player. Loop set to true.")]
    public AudioSource background;
    [Tooltip("Sound Player.")]
    public AudioSource sound;

    [Space]
    [Header("Audio Clips")]
    [Tooltip("Audio Clips ready to be played.")]
    public AudioClip[] clips;

    public void Awake()
    {
        //Singleton Pattern - Singleton
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    //Methods called by other classes to play background music or play sounds.
    public void PlayBackgroundMusic(int clipIndex)
    {
        background.clip = clips[clipIndex];
        background.Play();
    }

    public void PlaySound(int clipIndex)
    {
        sound.clip = clips[clipIndex];
        sound.Play();
    }

    [ContextMenu("PlayBackground")]
    public void DebugPlay()
    {
        background.clip = clips[0];
        background.Play();
    }
}
