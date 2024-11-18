using UnityEngine;
using UnityEngine.AI;

namespace FiniteStateMachine.States
{
    public abstract class BasePatrolling : BaseState
    {
        private readonly Transform[] _waypoints;
        private Transform _currentTarget;
        private int _targetIndex;

        private readonly Transform _myTransform;
        private readonly Transform _player;
        private readonly NavMeshAgent _agent;

        private readonly float _speed;
        private readonly float _distanceToChangeWaypoint;
        private readonly float _distanceToChase;

        private float _timerToChange;

        private readonly LayerMask _layerMask;

        protected BasePatrolling(StateMachine stateMachine, Transform myTransform, Transform player, NavMeshAgent agent, float speed, float distanceToChangeWaypoint, float distanceToChase, Transform[] waypoints, LayerMask layerMask) : base(stateMachine)
        {
            this.stateMachine = stateMachine;

            _myTransform = myTransform;
            _player = player;
            _agent = agent;
            _speed = speed;

            _distanceToChangeWaypoint = distanceToChangeWaypoint;
            _distanceToChase = distanceToChase;

            _waypoints = waypoints;
            _layerMask = layerMask;
        }

        public override void Enter()
        {
            _targetIndex = Random.Range(0, _waypoints.Length);
            
            if (_waypoints.Length <= 0) return;

            _currentTarget = _waypoints[_targetIndex];
            _agent.SetDestination(_currentTarget.position);
            
            _agent.speed = _speed;
            _agent.acceleration = _speed * 2;
        }

        public abstract override void UpdatePhysics();

        protected void PursuitOrWaypoint(BasePursuing pursuingState)
        {
            var position = _myTransform.position;
            var ray = new Ray(position, (_player.position - position).normalized);

            if (Physics.Raycast(ray, out var hit, _distanceToChase, _layerMask, QueryTriggerInteraction.Ignore))
            {
                if (hit.transform.gameObject.CompareTag("Player"))
                {
                    stateMachine.ChangeState(pursuingState);
                    return;
                }
            }
            
            if (!_currentTarget)
            {
                Debug.LogError($"{_myTransform.gameObject.name} We don't have any current target!");
                return;
            }
            
            var directionalVector = _currentTarget.position - _myTransform.position;
            
            if (directionalVector.magnitude <= _distanceToChangeWaypoint)
            {
                ChangeWaypoint(2);
            }
        }
        
        protected void PursuitOrWaypoint(BasePursuing pursuingState, bool distanceEnemy)
        {
            if (!distanceEnemy) return;
            
            var position = _myTransform.position;
            var ray = new Ray(position, (_player.position - position).normalized);

            if (Physics.Raycast(ray, out var hit, _distanceToChase, _layerMask, QueryTriggerInteraction.Ignore))
            {
                if (hit.transform.gameObject.CompareTag("Player"))
                {
                    stateMachine.ChangeState(pursuingState);
                }
            }
        }

        private void ChangeWaypoint(int timerBeforeChange)
        {
            _timerToChange += Time.fixedDeltaTime;
            
            if (_timerToChange < timerBeforeChange) return;

            _timerToChange = 0;
            
            var chooseDifferent = _targetIndex;

            while (chooseDifferent == _targetIndex)
            {
                _targetIndex = Random.Range(0, _waypoints.Length);
            }

            _currentTarget = _waypoints[_targetIndex];
            _agent.SetDestination(_currentTarget.position);
        }
    }
}