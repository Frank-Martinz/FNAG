using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shadow_movement : MonoBehaviour
{
    // объекты
    public GameObject shadow;
    public AudioSource audios;
    public AudioClip screamer;
    public GameObject player;
    public Transform cam;

    // скрипты
    public player_movement pm;

    // переменные
    float i = 25;
    int from_time = 15;
    int to_time = 45;

    private int stage = 0;

    void Start()
    {
        audios = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (i <= 0 && !pm.Is_game_end)
        {
            audios.Play();
            stage += 1;
            if (stage == 3)
            {
                if (pm.in_save_zone)
                {
                    stage = 0;
                }
                else 
                {
                    pm.FinishGame();
                    audios.clip = screamer;
                    audios.Play();
                    cam.localEulerAngles = new Vector3(0, 0, 0);
                    player.transform.position = new Vector3(0, 1, 0);
                    shadow.transform.position = new Vector3(0, 1, 2f);
                }
            }
            else
            {
                
            }
            i = GetRandomNum();
        }
        i -= Time.deltaTime;
    }
    public int GetRandomNum()
    {
        var rnd = new System.Random();
        return Convert.ToInt32(rnd.Next(from_time, to_time));
    }
}
