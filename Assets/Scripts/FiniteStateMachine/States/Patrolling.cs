using UnityEngine;

namespace FiniteStateMachine.States
{
    public class Patrolling : BaseState
    {
        private readonly Transform[] _waypoints;
        private Transform _currentTarget;
        private int _targetIndex;

        private readonly Transform _myTransform;
        private readonly Rigidbody _rigidBody;

        private readonly float _speed;
        private readonly float _distanceToChangeWaypoint;
        private readonly float _distanceToDetect;

        private readonly Transform _player;
        
        // ReSharper disable once SuggestBaseTypeForParameter
        public Patrolling(BossStateMachine stateMachine, Transform myTransform, Rigidbody rigidBody, float speed, float distanceToChangeWaypoint, float distanceToDetect, Transform[] waypoints, Transform player) : base(stateMachine)
        {
            this.stateMachine = stateMachine;

            _myTransform = myTransform;
            _rigidBody = rigidBody;

            _speed = speed;
            _distanceToChangeWaypoint = distanceToChangeWaypoint;
            _distanceToDetect = distanceToDetect;

            _waypoints = waypoints;
            _player = player;
        }

        public override void Enter()
        {
            _targetIndex = Random.Range(0, _waypoints.Length);
            _currentTarget = _waypoints[_targetIndex];
        }

        public override void UpdatePhysics()
        {
            if ((_player.position - _myTransform.position).magnitude < _distanceToDetect)
            {
                stateMachine.ChangeState(((BossStateMachine) stateMachine).pursuingState);
                return;
            }
            
            var directionalVector = _currentTarget.transform.position - _myTransform.position;

            if (directionalVector.magnitude < _distanceToChangeWaypoint)
            {
                var chooseDifferent = _targetIndex;

                while (chooseDifferent == _targetIndex)
                {
                    _targetIndex = Random.Range(0, _waypoints.Length);
                }

                _currentTarget = _waypoints[_targetIndex];
            }
            else
            {
                _myTransform.LookAt(_currentTarget);
                MoveTowardTarget(directionalVector, _speed);
            }
        }

        private void MoveTowardTarget(Vector3 direction, float speed)
        {
            _rigidBody.MovePosition(_myTransform.position + direction.normalized * (speed * Time.deltaTime));
        }
    }
}