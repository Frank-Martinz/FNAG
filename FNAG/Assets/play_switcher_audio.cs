using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class play_switcher_audio : MonoBehaviour
{
    public AudioSource audios;
    void Start()
    {
        audios = GetComponent<AudioSource>();
    }

    public void Play_Sound()
    {
        audios.Play();
    }
}
