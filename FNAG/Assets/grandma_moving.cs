using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class grandma_moving : MonoBehaviour
{
    // объекты
    public GameObject grandma;
    public GameObject player;
    public player_movement pl;
    public Transform cam;
    public Image white_noice;
    public open_closing_tablet oct_white_noise;
    public AudioSource audios;
    public AudioClip openning_door;
    public AudioClip closing_door;
    public AudioClip screamer;
    public GameObject danger_zone;
    public Light lamp;
    public Camera surcam;

    // сторонние скрипты
    public player_movement pm;
    public open_closing_tablet oct;
    public play_switcher_audio psa;
    
    // переменные 
    int stage = 0;
    public bool grandma_in_room = false;
    public float i = 25f;
    public int from_time = 15;
    public int to_time = 45;
    float danger_zone_moving = 0f;
    
    Dictionary<int, Vector3> stages = new Dictionary<int, Vector3>()
    {
        {0, new Vector3(16f, 1f, 20f)},
        {1, new Vector3(0f, 1f, 20f)},
        {2, new Vector3(2f, 1f, 18f)},
        {3, new Vector3(0f, 1f, 16f)},
        {4, new Vector3(-1f, 1f, 14f)},
    };

    void Start() 
    {
        grandma.transform.position = stages[stage]; 
        audios = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (danger_zone_moving != 0f && !pm.Is_game_end)
        {
            danger_zone.transform.localScale += new Vector3(0.1f, 0, 0);
            if (danger_zone.transform.localScale.x >= 28f)
            {
                danger_zone_moving = 0f;
            }
        }
        if (i <= 0 && !pm.Is_game_end && !pm.is_game_paused)
        {
            stage++;
            if (stage >= 4 && !audios.isPlaying && !pm.Is_game_end) 
            {
                audios.clip = openning_door;
                audios.Play();
                oct.StartAnimation();
                Invoke("MakeDangerZoneBigger", 2.5f);
                Invoke("CheckPlayerOnBed", 5);
            }
            else
            {
                i = GetRandomNum();
                if (pm.in_camera)
                {
                    ShowWhiteNoice();
                    Invoke("HideWhiteNoice", 1.5f);
                }
                grandma.transform.position = stages[stage]; 
            }    
        }
        if (!pm.is_game_paused) {i -= Time.deltaTime;}
    }
    public int GetRandomNum()
    {
        var rnd = new System.Random();
        return Convert.ToInt32(rnd.Next(from_time, to_time));
    }

    void ShowWhiteNoice()
    {
        if (!surcam.isActiveAndEnabled) {return;}
        white_noice.color = new Color(255, 255, 255, 0.85f);
        oct_white_noise.StartAnimation();
    }

    void HideWhiteNoice()
    {
        white_noice.color = new Color(0, 0, 0, 0);
    }
    void CheckPlayerOnBed()
    {
        if (pl.on_bed)
        {
            if (lamp.gameObject.activeSelf)
            {
                psa.Play_Sound();
                lamp.gameObject.SetActive(false);
            }
            Invoke("GetOutOfRoom", 1.1f);
        }
        else
        {
            MakeAScrimmer();
        }
    }
    void GetOutOfRoom()
    {
        audios.clip = closing_door;
        audios.Play();
        oct.StartAnimation();
        stage = 0;
        i = GetRandomNum();
        MakeDangerZoneLess();
        grandma.transform.position = stages[stage];
    }
    void MakeDangerZoneBigger()
    {
        danger_zone_moving = 0.1f;
    }
    void MakeDangerZoneLess()
    {
        danger_zone_moving = 0f;
        danger_zone.transform.localScale = new Vector3(0, 0.5f, 16f);
    }
    public void MakeAScrimmer()
    {
        if (!pm.Is_game_end)
        {
            pm.FinishGame();
            audios.clip = screamer;
            player.transform.localEulerAngles = new Vector3(0, 0, 0);
            cam.localEulerAngles = new Vector3(-10, 0, 0);
            player.transform.position = new Vector3(0, 1, 0);
            grandma.transform.position = new Vector3(0, 1, 2f);
            audios.Play();
        }
        
    }
    
    public void SetDif(float _i, int ft, int tt)
    {
        i = _i;
        from_time = ft;
        to_time = tt; 
    }
}
