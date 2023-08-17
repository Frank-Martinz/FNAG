using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class play_last_animation : MonoBehaviour
{
    public Animator anim;
    public AnimationClip finish_night_clip;

    void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void StartAnimaton()
    {
        anim.Play(finish_night_clip.name);
    }
}
