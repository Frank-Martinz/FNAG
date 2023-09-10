using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class open_close_settings_in_game : MonoBehaviour
{
    public Canvas menu;
    public Canvas settings;
    
    public void Open_Close_Settings_In_Game()
    {
        settings.gameObject.SetActive(!settings.gameObject.activeSelf);
        menu.gameObject.SetActive(!menu.gameObject.activeSelf);
    }
}
