using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CharacterPhysicsFsm
{
    [CreateAssetMenu(fileName = "AirborneControlledState", menuName = "FSM/Character/States/AirborneControlledState", order = 1)]

    public class AirborneControlledState : AbstractPSMState
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
            HandleMovement(_inputHandler.horizontal/10, _inputHandler.vertical/10, ForceMode.Impulse);
            HandleRot(_inputHandler.mouse_X, _inputHandler.lookSpeed);
            if (_psm.isGrounded == true)
            {
                _psm.EnterState(PhysicsStateType.GROUNDED);
            }
        }
    }
}
