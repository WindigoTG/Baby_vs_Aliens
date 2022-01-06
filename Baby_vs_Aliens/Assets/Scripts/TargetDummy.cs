using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Baby_vs_Aliens.Tools;

namespace Baby_vs_Aliens
{
    public class TargetDummy : MonoBehaviour, IDamageable
    {
        [SerializeField] EntityType _type;
        [SerializeField] ProjectileConfig _config;

        private float _currentDelay;

        public EntityType Type => _type;

        public void TakeDamege(int damage)
        {
            Debug.Log($"Took {damage} damage");
        }

        private void Awake()
        {
            _currentDelay = _config.FireDelay;
        }

        private void Update()
        {
            if (_currentDelay > 0)
                _currentDelay -= Time.deltaTime;
            else
            {
                Fire();
                _currentDelay = _config.FireDelay;
            }
        }

        private void Fire()
        {
            float x = Random.Range(-1f, 1f);
            float z = Random.Range(-1f, 1f);

            var direction = new Vector3(x, 0, z).normalized;

            var projectile = ServiceLocator.GetService<ObjectPoolManager>().
                GetBulletPool(_config.Prefab).Pop().GetComponent<Projectile>();

            projectile.Init(transform.position + direction, direction, _config);
        }
    }
}