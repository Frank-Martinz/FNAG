using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resume_button_script : MonoBehaviour
{
    // импорт других скриптов
    public player_movement pm;
    
    public void Resume_game()
    {
        pm.Pause_resume_game();
    }
}
