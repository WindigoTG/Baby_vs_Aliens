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
        [SerializeField] int _maxHealth;
        private IHealthHolder _health;

        [SerializeField] private HealthBar _healthBar;

        private float _currentDelay;

        public EntityType Type => _type;

        public void TakeDamege(int damage)
        {
            _health.TakeDamage(damage);
        }

        private void Awake()
        {
            _currentDelay = _config.FireDelay;

            _health = new HealthHolder(_maxHealth);
            (_health as HealthHolder).HealthPercentage.SubscribeOnChange(_healthBar.SetBarSize);
            _health.Death += () => { gameObject.SetActive(false); };
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