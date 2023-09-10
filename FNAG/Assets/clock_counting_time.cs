using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class clock_counting_time : MonoBehaviour
{
    // объекты
    public GameObject clock;
    public GameObject sun;
    public Camera main_camera;
    public Camera finish_night_camera;
    public TextMesh time;
    public Canvas pause_menu;
    public AudioSource clock_aud;
    public AudioSource birds_aud;    
    public AudioListener main_cam_lis;
    public AudioListener finish_cam_lis;
    // другие скрипты
    public player_movement pm;
    public closing_opening_eyes coe;
    public play_last_animation pla;
    public Loading_a_scenes ls;
    // переменные
    public float seconds_have_passed = 0f;
    public int hour_now = 0;

    void Start()
    {
        time.text = $"0{hour_now}:00";
    }

    void Update()
    {
        if (!pm.Is_game_end && !pm.is_game_paused)
        {
            sun.transform.Rotate(0.195f * Time.deltaTime, 0, 0);
            seconds_have_passed += Time.deltaTime;
            if (seconds_have_passed >= 60)
            {
                hour_now += 1;
                seconds_have_passed = 0f;
                time.text = $"0{hour_now}:00";
            } 
            if (hour_now == 6)
            {
                pm.WinGame();
                coe.FinishNightClose();
            }
        }
        else if (hour_now == 6)
        {
            if (coe.step == 0 && coe.alfa == 0)
            {
                pla.StartAnimaton();
            }
            else if(coe.step == 0 && coe.alfa == 1)
            {
                main_camera.gameObject.SetActive(false);
                finish_night_camera.gameObject.SetActive(true);
                finish_cam_lis.enabled = true;
                main_cam_lis.enabled = false;
                coe.FinishNightOpen();
                clock_aud.Play();
                birds_aud.Play();
                Invoke("ReturnToMenu", 11f);   
            }
        }
    }

    public void ReturnToMenu()
    {
        pause_menu.gameObject.SetActive(true);
        clock_aud.Stop();
        Cursor.lockState = CursorLockMode.None;
        ls.Loading_main_menu();
    }
}
