using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class cursor_on_button : MonoBehaviour
{
    public Button play_button;
    public Text night;

    public void ShowTheNight()
    {
        night.gameObject.SetActive(true);
    }
    public void HideTheNight()
    {
        night.gameObject.SetActive(false);
    }
}
