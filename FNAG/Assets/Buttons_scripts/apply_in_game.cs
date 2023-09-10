using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class apply_in_game : MonoBehaviour
{
    public setup_game_script sgs;

    public void ApplyInGame()
    {
        sgs.SetUpGame();
    }
}
