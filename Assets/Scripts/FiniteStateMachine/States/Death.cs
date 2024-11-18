using UnityEngine.AI;

namespace FiniteStateMachine.States
{
    public class Death : BaseState
    {
        private readonly NavMeshAgent _agent;
        
        public Death(StateMachine stateMachine, NavMeshAgent agent) : base(stateMachine)
        {
            _agent = agent;
        }

        public override void Enter()
        {
            _agent.isStopped = true;
        }
    }
}