using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Abstract_Input_Handler : MonoBehaviour
{
    public float horizontal { get; protected set; }
    public float vertical { get; protected set; }
    public float mouse_X { get; protected set; }
    public float mouse_y { get; protected set; }
    public float jump { get; protected set; }
    public float fire { get; protected set; }
    public float viewRange = 60;

    public float lookSpeed = 4;
   
}
