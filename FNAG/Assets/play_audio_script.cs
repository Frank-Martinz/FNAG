using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class play_audio_script : MonoBehaviour
{
    public AudioSource audio_source;
    public AudioClip audio_clip;

    void Start()
    {
        audio_source = GetComponent<AudioSource>();
    }

    public void PlayASound()
    {
        audio_source.clip = audio_clip;
        audio_source.Play();
    }
}
