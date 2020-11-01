using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCamera : MonoBehaviour
{
  

    public Abstract_Input_Handler input;
    void Start()
    {
       
        
    }
    void Update()
    {
       transform.localRotation = Quaternion.Euler(-input.mouse_y, 0f, 0f);
    }
}
