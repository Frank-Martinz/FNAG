using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class detailed_settings : MonoBehaviour
{
    public GameObject game;
    public GameObject sound;
    public GameObject video;

    public void Close_All_panels()
    {
        game.SetActive(false);
        sound.SetActive(false);
        video.SetActive(false);
    }
    public void Open_Sound_panel()
    {
        Close_All_panels();
        sound.SetActive(true);
    }
    public void Open_Game_panel()
    {
        Close_All_panels();
        game.SetActive(true);
    }
    public void Open_Video_panel()
    {
        Close_All_panels();
        video.SetActive(true);
    }
}
