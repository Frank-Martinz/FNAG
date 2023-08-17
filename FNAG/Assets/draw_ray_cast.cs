using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class draw_ray_cast : MonoBehaviour
{
    public Camera main_cam;
    private Ray _ray;
    private RaycastHit _hit;
    public bool look_at_switcher = false;

    void Ray()
    {
        _ray = main_cam.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
    }
    void Update()
    {
        look_at_switcher = false;
        Ray();
        DrawRay();
    }
    void DrawRay()
    {
        if (Physics.Raycast(_ray, out _hit, 5f))
        {
            if (_hit.transform.gameObject.name == "light switcher")
            {
                look_at_switcher = true;
            }
        }
    }
}
