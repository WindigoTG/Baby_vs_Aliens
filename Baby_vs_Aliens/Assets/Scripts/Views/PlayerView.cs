using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Baby_vs_Aliens
{
    public class PlayerView : MonoBehaviour
    {
        //private Animator _animator;
        private Rigidbody _rigidBody;

        private Vector3 _moveVector;

        //public Animator Animator => _animator;
        public Rigidbody RigidBody => _rigidBody;


        private void Awake()
        {
            //_animator = GetComponent<Animator>();
            _rigidBody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            var oldPosition = _rigidBody.position;
            var newPosition = oldPosition + _moveVector;

            _rigidBody.MovePosition(newPosition);
            _moveVector = Vector3.zero;
        }

        public void SetMoveVector(Vector3 vector)
        {
            _moveVector = vector;
        }
    }
}