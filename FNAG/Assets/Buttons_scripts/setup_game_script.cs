using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEditor.Audio;

public class setup_game_script : MonoBehaviour
{
    public Light sun;
    public Light Lamp;
    public Light FlashLight;
    public Light Table_lamp;
    public Camera main_cam;
    public AudioListener main_cam_listen;
    public AudioSource Shadow_from_the_street;
    public AudioSource Shadow_from_the_closet;
    public AudioSource Grandma;
    public AudioSource LightSwitcher;
    public AudioSource FlashLight_Audio;
    public AudioSource player_aud;

    public player_movement pm;
    public shadow_movement sm;
    public grandma_moving gm;

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
        int volume = Convert.ToInt32(settings[0]);
        string audiomode = settings[1]; 
        string language = settings[2];
        float mouse_sensitivity = float.Parse(settings[3]);
        int field_of_view = Convert.ToInt32(settings[4]);
        string resolution = settings[5];
        string fullscreen_mode = settings[6];
        string vsync = settings[7];
        string shadows = settings[8];
        int night = Convert.ToInt32(settings[9]);
        // тени
        SetUpLight(shadows);
        // звук
        SetUpSound(volume, audiomode);
        // угол обзора
        main_cam.fieldOfView = Convert.ToInt32(field_of_view);
        // разрешение экрана
        int x, y;
        bool fullscreen;
        x = Convert.ToInt32(resolution.Split("x")[0]);
        y = Convert.ToInt32(resolution.Split("x")[1]);
        if (fullscreen_mode == "Fullscreen") {fullscreen = true;}
        else {fullscreen = false;}
        Screen.SetResolution(x, y, fullscreen);
        // vsync
        if (vsync == "off") {QualitySettings.vSyncCount = 0;}
        else {QualitySettings.vSyncCount = 1;}
        // чувствительность мыши
        pm.Change_Sensitivity(mouse_sensitivity);
        // установка сложности
        SetNightDif(night);
    }

    void SetUpLight(string shadows)
    {
        if (shadows == "Hard") 
        {
            sun.shadows = LightShadows.Hard;
            Lamp.shadows = LightShadows.Hard;
            FlashLight.shadows = LightShadows.Hard;
            Table_lamp.shadows = LightShadows.Hard;
        }
        else if (shadows == "Soft") 
        {
            sun.shadows = LightShadows.Soft;
            Lamp.shadows = LightShadows.Soft;
            FlashLight.shadows = LightShadows.Soft;
            Table_lamp.shadows = LightShadows.Soft;
        }
        else 
        {
            sun.shadows = LightShadows.None;
            Lamp.shadows = LightShadows.None;
            FlashLight.shadows = LightShadows.None;
            Table_lamp.shadows = LightShadows.None;
        }
    }

    void SetUpSound(int volume, string audiomode)
    {
        AudioConfiguration _audioConfig = AudioSettings.GetConfiguration();
        _audioConfig.speakerMode = AudioSettings.driverCapabilities;
        AudioSettings.Reset (_audioConfig);
        if (audiomode == "Mono") {AudioSettings.speakerMode = AudioSpeakerMode.Mono; }
        else if (audiomode == "Stereo") {AudioSettings.speakerMode = AudioSpeakerMode.Stereo; }
        else if (audiomode == "Quad") {AudioSettings.speakerMode = AudioSpeakerMode.Quad; }
        else if (audiomode == "Surround") {AudioSettings.speakerMode = AudioSpeakerMode.Surround; }
        else if (audiomode == "5.1") {AudioSettings.speakerMode = AudioSpeakerMode.Mode5point1; }
        else if (audiomode == "7.1") {AudioSettings.speakerMode = AudioSpeakerMode.Mode7point1; }
        float vol = Convert.ToSingle(volume) / 100;
        Shadow_from_the_closet.volume = vol;
        Shadow_from_the_street.volume = vol;
        Grandma.volume = vol;
        LightSwitcher.volume = vol;
        FlashLight_Audio.volume = vol;
        player_aud.volume = vol;
        pm.volume = vol;
    }

    void SetNightDif(int night)
    {
        string path = @"Assets\Settings and Info\Nights.txt";
        string[] text = File.ReadAllText(path).Split(";");
        string[] lines = text[night - 1].Split('\n');
        if (night > 1) { lines = lines[1..7];}
        float i = Convert.ToSingle(lines[1].Split(":")[1]);
        int ft_g = Convert.ToInt32(lines[2].Split(":")[1]);
        int tt_g = Convert.ToInt32(lines[3].Split(":")[1]);
        int ft_s = Convert.ToInt32(lines[4].Split(":")[1]);
        int tt_s = Convert.ToInt32(lines[5].Split(":")[1]);
        gm.SetDif(i, ft_g, tt_g);
        sm.SetDif(i, ft_s, tt_s);
    }
}
