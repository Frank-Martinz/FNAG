using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class clock_counting_time : MonoBehaviour
{
    // объекты
    public GameObject clock;
    public Camera main_camera;
    public Camera finish_night_camera;
    public TextMesh time;
    // другие скрипты
    public player_movement pm;
    public closing_opening_eyes coe;
    public play_last_animation pla;
    // переменные
    public float seconds_have_passed = 0f;
    public int hour_now = 0;
    void Start()
    {
        time.text = $"0{hour_now}:00";
    }

    void Update()
    {
        Debug.Log(seconds_have_passed);
        if (!pm.Is_game_end)
        {
            seconds_have_passed += Time.deltaTime;
            if (seconds_have_passed >= 60)
            {
                hour_now += 1;
                seconds_have_passed = 0f;
                time.text = $"0{hour_now}:00";
            } 
            if (hour_now == 6)
            {
                pm.FinishGame();
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
                coe.FinishNightOpen();
            }
        }
    }
}
