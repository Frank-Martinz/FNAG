using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class play_switcher_audio : MonoBehaviour
{
    public AudioSource audios;
    public GameObject light_switcher_model_1;
    public GameObject light_switcher_model_2;

    void Start()
    {
        audios = GetComponent<AudioSource>();
    }

    public void Play_Sound()
    {
        audios.Play();
        light_switcher_model_1.SetActive(!light_switcher_model_1.activeSelf);
        light_switcher_model_2.SetActive(!light_switcher_model_2.activeSelf);
    }
}
