using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterPhysicsFsm
{
    [CreateAssetMenu(fileName = "GroundedState", menuName = "FSM/Character/States/Grounded", order = 0)]
    public class GroundedState : AbstractPSMState
    {
        public override void OnEnable()
        {
            base.OnEnable();
            StateType = PhysicsStateType.GROUNDED;
        }
        public override bool EnterState()
        {
            base.EnterState();
            if (_psm.isGrounded == false)
            {
                EnteredState = false;
            }
            if(_psm.isGrounded == true)
            {
               
                EnteredState = true;
            }

            return EnteredState;
        }
        public override void UpdateState()
        {
            if (EnteredState)
            {
                
               HandleMovement(_inputHandler.horizontal, _inputHandler.vertical, _movementStats.movementForce);
               HandleJump(_inputHandler.jump);
               HandleStop();
               HandleRot(_inputHandler.mouse_X, _inputHandler.lookSpeed);
                if (_psm.isGrounded == false)
                {
                    _psm.EnterState(PhysicsStateType.AIRBORNE);
                }
            }
        }

       


       

    }
}
