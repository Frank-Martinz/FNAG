using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class closing_opening_eyes : MonoBehaviour
{
    public float alfa = 0;
    public float step = 0;
    public Image eyes;
    public player_movement pm;
    void Update()
    {
        if (!pm.is_game_paused)
        {
            alfa += step;
            if (alfa <= 0 || alfa >= 1)
            {
                step = 0;
                if (alfa <= 0) {alfa = 0;}
                else {alfa = 1;}
            }
            if (pm.on_bed && pm.near_by_bed && !pm.Is_game_end) {CloseEyes();}
            else if (!pm.on_bed && pm.near_by_bed && !pm.Is_game_end) {OpenEyes();}
            eyes.color = new Color(0, 0, 0, alfa);
        }
    }
    void OpenEyes()
    {   
        step = -0.01f;
    }
    void CloseEyes()
    {
        step = 0.001f;
    }
    public void OpenForScreamer()
    {
        eyes.color = new Color(0, 0, 0, 0);
    }
    public void FinishNightClose()
    {
        CloseEyes();
    }
    public void FinishNightOpen()
    {
        OpenEyes();
    }
}
