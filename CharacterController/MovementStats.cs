using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MovementStats", menuName = "FSM/Character/MovementStats", order = 0)]
public class MovementStats : ScriptableObject
{

    public float groudCheckMod= -.67f;

    public ForceMode movementForce = ForceMode.VelocityChange;

    public float movementSpeed = 2;

    public float maxSpeed = 2;

  public  float jumpForce;
    
    

    
    
   
}
