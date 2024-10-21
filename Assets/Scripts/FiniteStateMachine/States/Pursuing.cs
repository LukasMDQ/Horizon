using UnityEngine;

namespace FiniteStateMachine.States
{
    public class Pursuing : BaseState
    {
        private readonly Transform _myTransform;
        private readonly Transform _player;
        private readonly Rigidbody _rigidBody;
        private readonly float _speed;
        private readonly float _distanceToAttack;
        
        // ReSharper disable once SuggestBaseTypeForParameter
        public Pursuing(BossStateMachine stateMachine, Transform myTransform, Transform player, Rigidbody rigidBody, float speed, float distanceToAttack) : base(stateMachine)
        {
            this.stateMachine = stateMachine;

            _myTransform = myTransform;
            _player = player;
            _rigidBody = rigidBody;

            _speed = speed;
            _distanceToAttack = distanceToAttack;
        }

        public override void UpdatePhysics()
        {
            var directionalVector = _player.position - _myTransform.position;
            
            if (directionalVector.magnitude < _distanceToAttack)
            {
                stateMachine.ChangeState(((BossStateMachine) stateMachine).attackingState);
                return;
            }

            _myTransform.LookAt(_player);
            MoveTowardTarget(directionalVector, _speed);
        }

        private void MoveTowardTarget(Vector3 direction, float speed)
        {
            _rigidBody.MovePosition(_myTransform.position + direction.normalized * (speed * 1.5f * Time.deltaTime));
        }
    }
}