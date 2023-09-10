using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

public class open_close_settings : MonoBehaviour
{
    public Canvas settings;
    public Canvas menu;
    public Animator main_cam;
    public AnimationClip clip;


    public void Open_Close_settings()
    {
        settings.gameObject.SetActive(!settings.gameObject.activeSelf);
        menu.gameObject.SetActive(!menu.gameObject.activeSelf);
        main_cam.Play(clip.name);
    }
}
