namespace FiniteStateMachine
{
    public abstract class BaseState
    {
        // ReSharper disable once MemberCanBePrivate.Global
        protected StateMachine stateMachine;

        protected BaseState(StateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }
    
        public virtual void Enter(){}
        public virtual void UpdateLogic(){}
        public virtual void UpdatePhysics(){}
        public virtual void Exit(){}
    }
}