using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Input_Handler : Abstract_Input_Handler
{

    void Update()
    {
        if(Input.GetButtonDown("Jump")) { jump = 1; } else { jump = 0; }
        if (Input.GetButtonDown("Fire1")) { fire = 1; } else { fire = 0; }
        mouse_X = Input.GetAxis("Mouse X");
        mouse_y += Input.GetAxis("Mouse Y") * lookSpeed;
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        mouse_y = Mathf.Clamp(mouse_y, -viewRange, 90);
    }
}
