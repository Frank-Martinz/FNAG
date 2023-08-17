using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow_from_the_closet : MonoBehaviour
{
    // Объекты
    public GameObject shadow;
    public GameObject player;
    public AudioSource audios;
    public Transform cam;
    // Переменные
    float patience = 15f;
    // Другие скрипты
    public player_movement pm;

    void Update()
    {
        if (!pm.Is_game_end)
        {
            if (pm.lamp.gameObject.activeSelf)
            {
                if (patience < 30) {patience += 0.001f;}
                else if (patience > 30) {patience = 30f; }
            }
            else
            {
                if (patience > 0) { patience -= Time.deltaTime; }   
                else { MakeAScrimmer(); }
            }
        }
    }
    public void MakeAScrimmer()
    {
        if (!pm.Is_game_end)
        {
            pm.FinishGame();
            audios.Play();
            cam.localEulerAngles = new Vector3(0, 0, 0);
            player.transform.position = new Vector3(0, 1, 0);
            shadow.transform.position = new Vector3(0, 1, 2f);
        }
        
    }
}
