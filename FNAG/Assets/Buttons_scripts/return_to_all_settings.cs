using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class return_to_all_settings : MonoBehaviour
{
    public Canvas settings;
    public Canvas detailed_settings;

    public void Return_to_settings()
    {
        settings.gameObject.SetActive(true);
        detailed_settings.gameObject.SetActive(false);
    }
}
