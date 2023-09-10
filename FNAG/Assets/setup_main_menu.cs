using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;

public class setup_main_menu : MonoBehaviour
{
    public Slider volume;
    public Dropdown audiomode;
    public Dropdown language;
    public Slider mouse_sensitivity;
    public Slider field_of_view;
    public Dropdown resolution;
    public Dropdown fullscreen_mode;
    public Dropdown vsync;
    public Dropdown shadows;
    public Light sun;
    public Camera main_cam;
    public Text night_txt;
    public AudioSource birds;

    void Awake()
    {
        SetUpGame();
    }

    public void SetUpGame()
    {
        string path = @"Assets\Settings and Info\Settings.txt";
        string[] text = File.ReadAllLines(path);
        string[] settings = new string[10];
        int i = 0;
        foreach (string line in text)
        {
            settings[i] = line.Split(":")[1];
            i++;
        }
        volume.value = Convert.ToInt32(settings[0]);
        audiomode.value = audiomode.options.FindIndex(option => option.text == settings[1]);
        language.value = language.options.FindIndex(option => option.text == settings[2]);
        mouse_sensitivity.value = float.Parse(settings[3]);
        field_of_view.value = Convert.ToInt32(settings[4]);
        resolution.value = resolution.options.FindIndex(option => option.text == settings[5]);
        fullscreen_mode.value = fullscreen_mode.options.FindIndex(option => option.text == settings[6]);
        vsync.value = vsync.options.FindIndex(option => option.text == settings[7]);
        shadows.value = shadows.options.FindIndex(option => option.text == settings[8]);
        night_txt.text = $"<<< {settings[9]} Night";
        // тени
        if (settings[8] == "Hard") {sun.shadows = LightShadows.Hard;}
        else if (settings[8] == "Soft") {sun.shadows = LightShadows.Soft;}
        else {sun.shadows = LightShadows.None;
        }
        // угол обзора
        main_cam.fieldOfView = Convert.ToInt32(settings[4]);
        // разрешение экрана
        int x, y;
        bool fullscreen;
        x = Convert.ToInt32(settings[5].Split("x")[0]);
        y = Convert.ToInt32(settings[5].Split("x")[1]);
        if (settings[6] == "Fullscreen") {fullscreen = true;}
        else {fullscreen = false;}
        Screen.SetResolution(x, y, fullscreen);
        // vsync
        if (settings[7] == "off") {QualitySettings.vSyncCount = 0;}
        else {QualitySettings.vSyncCount = 1;}
        // звуки
        birds.volume = Convert.ToSingle(settings[0]) / 100;
    }
}
