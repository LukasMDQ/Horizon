using UnityEngine;

namespace FiniteStateMachine
{
    public class StateMachine : MonoBehaviour
    {
        protected BaseState CurrentState { get; private set; }

        private void Start()
        {
            CurrentState = GetInitialState();
            CurrentState?.Enter();
        }

        private void Update()
        {
            CurrentState?.UpdateLogic();
        }

        private void FixedUpdate()
        {
            CurrentState?.UpdatePhysics();
        }

        public void ChangeState(BaseState newState)
        {
            CurrentState.Exit();

            CurrentState = newState;
            CurrentState.Enter();
        }

        protected virtual BaseState GetInitialState()
        {
            return null;
        }
    }
}