using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class apply_btn_script : MonoBehaviour
{
    public Button apply;

    public Slider volume;
    public Dropdown audiomode;
    public Dropdown language;
    public Slider mouse_sensitivity;
    public Slider field_of_view;
    public Dropdown resolution;
    public Dropdown fullscreen_mode;
    public Dropdown vsync;
    public Dropdown shadows;

    public setup_main_menu smm;

    public void Apply_All_Changes()
    {
        apply.gameObject.SetActive(false);
        string path = @"Assets\Settings and Info\Settings.txt";
        string[] text = new string[] {"volume:", 
        "speaker_mode:", 
        "language:", 
        "mouse_sensitivity:", 
        "field_of_view:", 
        "resolution:", 
        "fullscreen_mode:", 
        "vsync:", 
        "shadows:",
        "night:"}; 
        
        text[0] += volume.value.ToString();
        text[1] += audiomode.options[audiomode.value].text;
        text[2] += language.options[language.value].text;
        text[3] += mouse_sensitivity.value.ToString();
        text[4] += field_of_view.value.ToString();
        text[5] += resolution.options[resolution.value].text;
        text[6] += fullscreen_mode.options[fullscreen_mode.value].text;
        text[7] += vsync.options[vsync.value].text;
        text[8] += shadows.options[shadows.value].text;
        text[9] += File.ReadAllLines(path)[9].Split(":")[1];
        
        string text_to_write = string.Join("\n", text);
        File.WriteAllText(path, text_to_write);
        smm.SetUpGame();
    }
}
