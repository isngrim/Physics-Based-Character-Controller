using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CharacterPhysicsFsm
{
    [CreateAssetMenu(fileName = "AirborneState", menuName = "FSM/Character/States/AirborneState", order = 1)]

    public class AirborneState : AbstractPSMState
    {
        public override void OnEnable()
        {
            base.OnEnable();
            StateType = PhysicsStateType.AIRBORNE;
        }
        public override bool EnterState()
        {
            base.EnterState();
            if (_psm.isGrounded == true)
            {
                EnteredState = false;
            }
            if (_psm.isGrounded == false)
            {

                EnteredState = true;
            }

            return EnteredState;

        }
        public override void UpdateState()
        {
            if(_inputHandler != null)
            {
                HandleRot(_inputHandler.mouse_X, _inputHandler.lookSpeed);
            }
          
            if (_psm.isGrounded == true)
            {
                _psm.EnterState(PhysicsStateType.GROUNDED);
            }
        }
    }
}

