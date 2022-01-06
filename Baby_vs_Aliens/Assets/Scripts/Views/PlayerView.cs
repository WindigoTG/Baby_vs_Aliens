using UnityEngine;
using System;

namespace Baby_vs_Aliens
{
    public class PlayerView : MonoBehaviour, IDamageable
    {
        #region Fields

        [SerializeField] private EntityType _type = EntityType.Baby;
        [SerializeField] private ParticleSystem _bubblesShot;
        [SerializeField] private ParticleSystem _bubblesDeath;
        [SerializeField] private GameObject _characterModel;

        [SerializeField] private HealthBar _healthBar;

        private Animator _animator;
        private Rigidbody _rigidBody;

        private Vector3 _moveVector;

        private CharacterState _currentState;

        private CapsuleCollider _collider;

        public event Action<int> Damaged;

        #endregion


        #region Properties

        public Animator Animator => _animator;

        public Rigidbody RigidBody => _rigidBody;

        public EntityType Type => _type;

        public Vector3 BulletSpawn => transform.position + _collider.center + transform.forward * _collider.radius * 2;

        public ParticleSystem ShotParticles => _bubblesShot;

        public ParticleSystem DeathParticles => _bubblesDeath;

        public HealthBar HealthBar => _healthBar;

        #endregion


        #region UnityMethods

        private void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
            _rigidBody = GetComponent<Rigidbody>();
            _collider = GetComponent<CapsuleCollider>();

            SetState(CharacterState.Idle);
        }

        private void FixedUpdate()
        {
            var oldPosition = _rigidBody.position;
            var newPosition = oldPosition + _moveVector;

            _rigidBody.MovePosition(newPosition);
            _moveVector = Vector3.zero;
        }

        private void Update()
        {
            _healthBar.transform.rotation = Quaternion.Euler( new Vector3(70, 0, 0));
            _healthBar.transform.position = transform.position + Vector3.back * 0.5f + Vector3.up * 0.1f;
        }

        #endregion


        #region Methods

        public void SetMoveVector(Vector3 vector)
        {
            _moveVector = vector;
        }

        public void SetState(CharacterState state)
        {
            if (_currentState != state)
            {
                _currentState = state;

                _animator.SetTrigger(state.ToString());
            }
        }

        public void TakeDamege(int damage)
        {
            Damaged?.Invoke(damage);
        }

        public void HideCharacter()
        {
            _characterModel.SetActive(false);
            _collider.enabled = false;
            _healthBar.gameObject.SetActive(false);
        }

        public void ShowCharacter()
        {
            _characterModel.SetActive(true);
            _collider.enabled = true;
            _healthBar.gameObject.SetActive(true);
        }

        #endregion
    }
}