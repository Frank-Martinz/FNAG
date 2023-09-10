using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UI;
using UnityEngine.UI;

public class set_text_from_slider : MonoBehaviour
{
    public Slider sldr;
    public Text txt;

    void Awake()
    {
        SetTextFromSlider();
    }
    
    public void SetTextFromSlider()
    {
        txt.text = sldr.value.ToString();
    }
}
