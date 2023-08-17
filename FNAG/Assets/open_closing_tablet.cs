using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class open_closing_tablet : MonoBehaviour
{
    public Animator anim;
    public AnimationClip a;
    public AnimationClip b;
    bool is_opened = false;
    public bool is_animation_running = false;
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void StartAnimation()
    {
        if (!is_opened) {anim.Play(a.name);}
        else {anim.Play(b.name);}
        is_opened = !is_opened;
    }
    public void ChangeStatusOfAnimation()
    {
        is_animation_running = !is_animation_running;
    }
}
