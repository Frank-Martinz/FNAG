using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Palmmedia.ReportGenerator.Core;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class player_movement : MonoBehaviour
{
    // объекты
    public Transform cam; // главная камера (чтобы крутить)
    public GameObject player; // игрок
    public AudioSource player_aud;
    public Rigidbody player_rb; // твёрдое тело
    public Camera main_cam; // главная камера (для удобного переключения)
    public Camera scfp; // камера наблюдения
    public GameObject tablet;
    public Light lamp;
    public Image white_noice;
    public Image camera_frame;
    public Canvas pause_menu;
    public Canvas settings;
    public Light flashlight;

    // импорты других скриптов
    public open_closing_tablet oct;
    public open_closing_tablet oct_cam;
    public draw_ray_cast drc;
    public play_switcher_audio psa;
    public grandma_moving gm;
    public play_audio_script flash_light_audio;

    // переменные
    public float sensitivity = 5f; // чувствительность мыши
    public float headMinY = -40f; // ограничение угла для головы
    public float headMaxY = 40f; 
    private float rotationY;
    private float speed_of_moving_head = 0f;
    public float volume = 0.28f;
    public float energy = 150f; // энергия фонарика
    public bool near_by_bed = false; // нахождение рядом с кроватью
    public bool near_by_closet = false; // нахождение рядом со шкафом
    public bool on_bed = false; // нахождение на кровати
    public bool inside_the_closet = false; // нахожение в шкафу
    public bool in_camera = false; // нахождение в просмотре камеры
    public bool in_save_zone = false; // нахождение в безопасной зоне
    public bool Is_game_end = false; // закончилась ли игра
    public bool up_down_head = false; // поднятие головы для просмотра планшета
    public bool is_game_paused = false; // игра на паузе
    public bool is_player_walking = false;

    private void OnCollisionEnter(Collision other) // проверка на вход в коллизию
    {
        if (other.collider.tag == "ground")
        {
            player_rb.constraints = RigidbodyConstraints.FreezePositionY;
        } 
        if (other.collider.tag == "bed") { near_by_bed = true; }
    }
    // проверка нахождения в коллизии
    private void OnCollisionStay(Collision other) 
    {
        if (other.collider.tag == "bed") { near_by_bed = true; }    
    }
    // проверка выхода из коллизии
    private void OnCollisionExit(Collision other) 
    {
        if (other.collider.tag == "bed") { near_by_bed = false; }    
    }
    // проверка на вход в триггер
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "savezone") { in_save_zone = true; }
        if (other.tag == "danger zone") { gm.MakeAScrimmer(); }
    }
    // проверка на нахождение в триггере
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "savezone") { in_save_zone = true; }
    }
    // проверка на выход из триггера
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "savezone") { in_save_zone = false;}
    }
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        player_aud.Play();
        player_rb = GetComponent<Rigidbody>();
        main_cam.enabled = true;
        scfp.enabled = false;
    }

    void Update()
    {
        if (!Is_game_end && !is_game_paused)
        {
            is_player_walking = false;
            if (energy <= 0f) {flashlight.enabled = false;}
            if (flashlight.enabled) {energy -= Time.deltaTime; }
            if (up_down_head) 
            {
                cam.localEulerAngles = new Vector3(cam.localEulerAngles.x + speed_of_moving_head, cam.localEulerAngles.y, 0);
                if (cam.localEulerAngles.x < 1 && cam.localEulerAngles.x > -1)
                {
                    up_down_head = false;
                    speed_of_moving_head = 0f;
                }
            }
            if(!in_camera)
            {
                // вращение камеры
                if (!on_bed && !inside_the_closet)
                {
                    float rotationX = cam.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivity;
                    rotationY += Input.GetAxis("Mouse Y") * sensitivity;
                    rotationY = Mathf.Clamp(rotationY, headMinY, headMaxY);
                    player.transform.rotation *= Quaternion.Euler(0f, rotationX, 0f);
                    cam.localEulerAngles = new Vector3(-rotationY, 0, 0);

                    // перемещение
                    if (Input.GetKey(KeyCode.W))
                    {
                        float x = cam.forward.x * 10f;
                        float z = cam.forward.z * 10f;
                        player_rb.AddForce(new Vector3(x, 0, z), ForceMode.Force);
                        is_player_walking = true;
                    }
                    if (Input.GetKey(KeyCode.S))
                    {
                        float x = cam.forward.x * -10f;
                        float z = cam.forward.z * -10f;
                        player_rb.AddForce(new Vector3(x, 0, z), ForceMode.Force);
                        is_player_walking = true;
                    }
                    if (Input.GetKey(KeyCode.D))
                    {
                        float x = cam.right.x * 10f;
                        float z = cam.right.z * 10f;
                        player_rb.AddForce(new Vector3(x, 0, z), ForceMode.Force);
                        is_player_walking = true;
                    }
                    if (Input.GetKey(KeyCode.A))
                    {
                        float x = cam.right.x * -10f;
                        float z = cam.right.z * -10f;
                        player_rb.AddForce(new Vector3(x, 0, z), ForceMode.Force);
                        is_player_walking = true;
                    }
                    // включение/выключение фонарика
                    if (Input.GetMouseButtonDown(1)) 
                    {
                        flash_light_audio.PlayASound();
                        if (energy > 0f){ flashlight.enabled = !flashlight.enabled; }
                    }
                }               
            }
            if (is_player_walking) {player_aud.volume = volume; }
            else {player_aud.volume = 0; }
            // включить\выключить свет
            if (Input.GetMouseButtonDown(0) && drc.look_at_switcher)
            {
                lamp.gameObject.SetActive(!lamp.gameObject.activeSelf);
                psa.Play_Sound();
            }
            // просмотр камер
            if (Input.GetKeyDown(KeyCode.Space) && !on_bed && !oct_cam.is_animation_running)
            {
                if (in_camera) 
                {
                    TurnOnOffCameras();
                    oct.StartAnimation();
                    Invoke("HideShowTablet", 1f);
                }
                else
                {
                    UpDownPlayerHead();
                    HideShowTablet();
                    oct.StartAnimation();
                    Invoke("TurnOnOffCameras", 1f);
                    in_camera = true;
                }
                
            }
            // встать с кровати
            if (Input.GetKeyDown(KeyCode.F) && on_bed && !in_camera)
            {
                StandUpFromBed();
            }
            // лечь на кровать
            else if (Input.GetKeyDown(KeyCode.F) && near_by_bed && !on_bed && !in_camera)
            {
                GetOnBed();
            }
        }
        // включение меню паузы
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause_resume_game();
        }
    }
    // лечь на кровать
    void GetOnBed()
    {
        on_bed = true;
        flashlight.enabled = false;
        player.transform.position = new Vector3(5, 1, 5);
        cam.transform.localEulerAngles = new Vector3(0, 0, 0);
        player.transform.localEulerAngles = new Vector3(-90, -90, 0);
    }
    // встать с кровати
    void StandUpFromBed()
    {
        on_bed = false;
        player.transform.position = new Vector3(5, 1.2f, 3.5f);
        player.transform.localEulerAngles = new Vector3(0, 0, 0);
    }
    // Смена камеры
    void TurnOnOffCameras()
    {
        white_noice.color = new Color(0, 0, 0, 0);
        camera_frame.gameObject.SetActive(!camera_frame.gameObject.activeSelf);
        main_cam.enabled = !main_cam.isActiveAndEnabled;
        scfp.enabled = !scfp.isActiveAndEnabled;
        oct_cam.ChangeStatusOfAnimation(); 
    }
    // Спрятать\Показать планшет
    void HideShowTablet()
    {
        tablet.SetActive(!tablet.activeSelf);
        if (in_camera) {in_camera = false; }
        oct_cam.ChangeStatusOfAnimation();
    }
    // поднять|опустить голову
    void UpDownPlayerHead()
    {
        up_down_head = true;
        if (cam.localEulerAngles.x > 0 && cam.localEulerAngles.x <= 41f) { speed_of_moving_head = -0.5f;}
        else if (cam.localEulerAngles.x >= 319) {speed_of_moving_head = 0.5f;}
    }
    // Поставить на паузу|продолжить игру
    public void Pause_resume_game()
    {
        if (!pause_menu.gameObject.activeSelf && settings.gameObject.activeSelf) {return ;}
        if (Time.timeScale == 0) {Time.timeScale = 1;}
        else {Time.timeScale = 0;}
        is_game_paused = !is_game_paused;
        pause_menu.gameObject.SetActive(!pause_menu.gameObject.activeSelf);
        if (!is_game_paused) {Cursor.lockState = CursorLockMode.Locked;}
        else {Cursor.lockState = CursorLockMode.None;}
    }
    // изменить булл
    public void GameEnd()
    {
        Is_game_end = true;
    }
    // закончить игру
    public void WinGame()
    {
        Debug.Log(123);
        GameEnd();
        if (in_camera) 
        {
            TurnOnOffCameras();
            oct.StartAnimation();
            oct_cam.StartAnimation();
            Invoke("HideShowTablet", 1f);
        }
        string path = @"Assets\Settings and Info\Settings.txt";
        string[] text = File.ReadAllLines(path);
        text[9] = $"night:{Convert.ToInt16(text[9].Split(":")[1]) + 1}";
        string text_to_write = string.Join("\n", text);
        File.WriteAllText(path, text_to_write);
    }

    public void FinishGame()
    {
        GameEnd();
        if (in_camera) 
        {
            TurnOnOffCameras();
            tablet.SetActive(false);
        }
    }

    public void Change_Sensitivity(float sen)
    {
        sensitivity = sen;
    }
}
