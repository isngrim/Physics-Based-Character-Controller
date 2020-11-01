
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.AI;
namespace CharacterPhysicsFsm
{
    public class Movement_Handler : MonoBehaviour
    {
         Animator animator;
        [SerializeField]
        MovementStats movement;
        [SerializeField]
        AbstractPSMState startingState;
        [SerializeField]
        AbstractPSMState currentState;
        [SerializeField]
        List<AbstractPSMState> _validStates;
        Rigidbody rigidbody;
        Abstract_Input_Handler input;
        [SerializeField]
        Vector3 movementTarget;

        public bool isGrounded { get; protected set; }
        Dictionary<PhysicsStateType, AbstractPSMState> _psmStates;

        public void Awake()
        {
            animator = this.GetComponentInChildren<Animator>();
            if(animator != null)
            {
                Debug.LogWarning("No Animator is present");
            }
            Cursor.lockState = CursorLockMode.Locked;
             input = this.GetComponent<Abstract_Input_Handler>();
            currentState = null;
             rigidbody = this.GetComponent<Rigidbody>();
            _psmStates = new Dictionary<PhysicsStateType, AbstractPSMState>();
            foreach (AbstractPSMState state in _validStates)
            {
                InitializeState(state);
                _psmStates.Add(state.StateType, state);
            }
        }
        public void Start()
        {
            if (startingState != null)
            {
                EnterState(startingState);
            }
        }
        public void FixedUpdate()
        {
            if (currentState != null)
            {
                InitializeState(currentState);
                currentState.UpdateState();
            }

        }
        //check if grounded
        #region Ground Check
        private void OnCollisionStay(Collision collision)
        {
            foreach (ContactPoint p in collision.contacts)
            {
                Vector3 curve = transform.position + (Vector3.up * movement.groudCheckMod);
                Debug.DrawLine(curve, p.point, Color.blue, 0.5f);
                Vector3 dir = curve - p.point;
                if (dir.y > 0f)
                {
                    if(isGrounded != true)
                    {
                        Ai_Input aiInput = GetComponent<Ai_Input>();
                        if (aiInput != null && input == aiInput)
                        {

                            NavMeshAgent agent = GetComponent<NavMeshAgent>();
                            Vector3 destination = agent.destination;
                            agent.Warp(transform.position);
                            //agent.SetDestination(transform.position);
                            agent.SetDestination(destination);
                        }
                    }
                    isGrounded = true;
                  
               
                }
            }
        }
        private void OnCollisionExit(Collision collision)
        {
            isGrounded = false;
        }
        #endregion
        //manage sate entering
        #region STATE MANAGEMENT
            void InitializeState(AbstractPSMState state)
        {
            state.InitializeState(animator, input, this, movement, rigidbody);

        }
        void EnterState(AbstractPSMState nextState)
        {
            if (nextState == null)
            {
                return;
            }
            if (currentState != null)
            {
                currentState.ExitState();
            }
            currentState = nextState;
            InitializeState(nextState);
            currentState.EnterState();
        }
        public void EnterState(PhysicsStateType stateType)
        {
            if (_psmStates.ContainsKey(stateType))
            {
                AbstractPSMState nextState = _psmStates[stateType];
                EnterState(nextState);
            }
        }
        #endregion
    }
}





