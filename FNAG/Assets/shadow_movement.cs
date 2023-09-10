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
    public Animator anim;
    public AnimationClip ac;

    // части скелета
    public GameObject head;

    // скрипты
    public player_movement pm;

    // переменные
    public float i = 25;
    public int from_time = 15;
    public int to_time = 45;

    private int stage = 0;

    Dictionary<int, Vector3> stages = new Dictionary<int, Vector3>()
    {
        {0, new Vector3(-7.7f, 1.2f,-25f)},
        {1, new Vector3(-5f, 1.2f, -15f)},
        {2, new Vector3(0f, 1.2f, -9f)}
    };
    Dictionary<int, Vector3> position_of_head = new Dictionary<int, Vector3>()
    {
        {0, new Vector3(-0.9833f, 0.4157f, 0f)},
        {1, new Vector3(-0.9833f, 0.4157f, 0f)},
        {2, new Vector3(-1.016f, 0.377f, 0f)}
    };

    void Start()
    {
        audios = GetComponent<AudioSource>();
        shadow.transform.position = stages[stage]; 
        head.transform.localPosition = position_of_head[stage];
    }

    void Update()
    {
        if (i <= 0 && !pm.Is_game_end && !pm.is_game_paused)
        {
            audios.Play();
            stage += 1;
            if (stage == 3)
            {   
                if (pm.in_save_zone)
                {
                    stage = 0;
                    i = GetRandomNum();
                    shadow.transform.position = stages[stage];
                }
                else 
                {
                    pm.FinishGame();
                    cam.localEulerAngles = new Vector3(0, 0, 0);
                    player.transform.position = new Vector3(0, 1, 0);
                    player.transform.localEulerAngles = new Vector3(-10f, 0, 0f);
                    shadow.transform.position = new Vector3(0, 1, 2f);
                    shadow.transform.localEulerAngles = new Vector3(0, 180, 0);
                    head.transform.localEulerAngles = new Vector3(0, 0, 0);
                    head.transform.localPosition = new Vector3(0, 0, 0);
                    audios.clip = screamer;
                    audios.Play();
                    anim.Play(ac.name);
                }
            }
            else
            {
                shadow.transform.position = stages[stage]; 
                head.transform.localPosition = position_of_head[stage];
            }
            i = GetRandomNum();
        }
        if (!pm.is_game_paused) {i -= Time.deltaTime;}
    }
    public int GetRandomNum()
    {
        var rnd = new System.Random();
        return Convert.ToInt32(rnd.Next(from_time, to_time));
    }

    public void SetDif(float _i, int ft, int tt)
    {
        i = _i;
        from_time = ft;
        to_time = tt; 
    }
}
