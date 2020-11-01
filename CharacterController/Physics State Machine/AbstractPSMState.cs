

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CharacterPhysicsFsm
{
    public enum ExecutionState
    {
        NONE,
        ACTIVE,
        COMPLETED,
        TERMINATED,
    };
    public enum PhysicsStateType
    {
        GROUNDED,
        AIRBORNE,
        ZERO_G,
        AI_MOVETO,
        AI_FOLLOWPATH
    }
    public abstract class AbstractPSMState : ScriptableObject
    {
        protected Rigidbody _rigidbody;
        protected Abstract_Input_Handler _inputHandler;
        protected MovementStats _movementStats;
        protected Movement_Handler _psm;
        protected Animator _anim;
        public ExecutionState ExecutionState { get; protected set; }
        public PhysicsStateType StateType { get; protected set; }
        public bool EnteredState { get; protected set; }


        public virtual void OnEnable()
        {
            ExecutionState = ExecutionState.NONE;
        }
        public virtual bool EnterState()
        {
            bool successInput = true;
            bool successMovement = true;
            bool successRigidbody = true;
            ExecutionState = ExecutionState.ACTIVE;
            //does Rigidbody exist

            successRigidbody = (_rigidbody != null);
            //does Input exist
            successInput = (_inputHandler != null);
            //does Executing agent exist 
   
            successMovement = (_movementStats != null);
            return successInput & successMovement & successRigidbody;
        }
        public abstract void UpdateState();

        public virtual bool ExitState()
        {
            ExecutionState = ExecutionState.COMPLETED;
            return true;
        }
        public virtual void SetInputHandler(Abstract_Input_Handler input_Handler)
        {
            if (input_Handler != null)
            {
                _inputHandler = input_Handler;
            }
            if(_inputHandler == null)
            {
                Debug.LogError("No Input");
            }
        }
        public virtual void  InitializeState(Animator anim,Abstract_Input_Handler input_Handler, Movement_Handler fsm, MovementStats movement_Handler, Rigidbody rigidbody)
        {
            SetInputHandler(input_Handler);
            SetExecutingAnim(anim);
            SetExecutingFSM(fsm);
            SetExecutingMovementHandler(movement_Handler);
            SetExecutingRigidbody(rigidbody);
        }
        public virtual void SetExecutingAnim(Animator anim)
        {
            if (anim != null)
            {
                _anim = anim;
            }
        }
        public virtual void SetExecutingFSM(Movement_Handler fsm)
        {
            if (fsm != null)
            {
                _psm = fsm;
            }
        }
        public virtual void SetExecutingMovementHandler(MovementStats movement_Handler)
        {
            if (movement_Handler != null)
            {
                _movementStats = movement_Handler;
            }
        }
        public virtual void SetExecutingRigidbody(Rigidbody rigidbody)
        {
            if (rigidbody != null)
            {
                _rigidbody = rigidbody;
            }
        }  
        //we use ForceMode.VelocityChange here because it give a more responsive movement of the camera
        public virtual void HandleRot(float x,float lookSpeed)
        {
           
            _rigidbody.AddTorque(Vector3.up * x * lookSpeed, ForceMode.VelocityChange);
            _rigidbody.angularVelocity += new Vector3(0, -_rigidbody.angularVelocity.y, 0);
        }
        //movement,ForceMode.VelocityChange changes the velocity,ignoring mass,ForceMode.Impulse changes velocity with mass,ForceMode.Acceleration feels like shit
        public virtual void HandleMovement(float horizontal, float vertical,ForceMode force)
        {

            _rigidbody.AddForce(_rigidbody.transform.right * horizontal * (_movementStats.movementSpeed / -Physics.gravity.y), force);
            if(_anim != null)
            {
                _anim.SetFloat("XBlend", horizontal);
            }
       
            _rigidbody.AddForce(_rigidbody.transform.forward * vertical * (_movementStats.movementSpeed / -Physics.gravity.y), force);
            if (_anim != null)
            {
                _anim.SetFloat("YBlend", vertical);
            }
          
        }
        //cancle out velocity so we dont slide around
        public virtual void HandleStop()
        {
            if (_rigidbody.velocity.x != 0)
            {
                _rigidbody.velocity += new Vector3(-_rigidbody.velocity.x, 0, 0);
            }
            if (_rigidbody.velocity.z != 0)
            {
                _rigidbody.velocity += new Vector3(0, 0, -_rigidbody.velocity.z);
            }
            if (_rigidbody.velocity.magnitude > _movementStats.maxSpeed)
            {
                _rigidbody.velocity = _rigidbody.velocity.normalized * _movementStats.maxSpeed;
            }
        }
        // seperated jump from movement,so we can have an airborne state with the ability to move,but not jump again,or to allow double jumps
        public virtual void HandleJump(float jump)
        {
           
            if (jump != 0)
            {
                _rigidbody.AddForce(_rigidbody.transform.up * jump * _movementStats.jumpForce, ForceMode.Impulse);
            }
        }
    }
}
