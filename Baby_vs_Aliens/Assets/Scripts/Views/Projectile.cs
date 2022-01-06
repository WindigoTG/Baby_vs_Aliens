using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Baby_vs_Aliens.Tools;

namespace Baby_vs_Aliens
{
    public class Projectile : MonoBehaviour, IProjectile
    {
        #region Fields

        private Vector3 _moveDirection;
        private ProjectileConfig _config;

        private Rigidbody _rigidBody;

        #endregion


        #region Properties

        public EntityType Type => _config.ProjectileOwner;

        public int Damage => _config.Damage;

        #endregion


        #region IProjectile

        public void Init(Vector3 startingPosition, Vector3 moveDirection, ProjectileConfig config)
        {
            _config = config;
            transform.position = startingPosition;
            _moveDirection = moveDirection;

            _rigidBody.velocity = _moveDirection * _config.Velocity;
        }

        #endregion


        #region UnityMethods

        private void Awake()
        {
            _rigidBody = GetComponent<Rigidbody>();
        }

        private void OnTriggerEnter(Collider other)
        {
            var damageable = other.GetComponent<IDamageable>();

            if (damageable != null && damageable.Type != Type)
                damageable.TakeDamege(Damage);

            _rigidBody.velocity = Vector3.zero;
            ServiceLocator.GetService<ObjectPoolManager>().GetBulletPool(_config.Prefab).Push(this.gameObject);
        }

        #endregion
    }
}