using System;
using UnityEngine;
using Baby_vs_Aliens.Tools;

namespace Baby_vs_Aliens
{
    public class HealthHolder : IHealthHolder
    {
        #region Fields

        private int _maxHealth;
        private int _currentHealth;

        public event Action Death;

        public readonly SubscriptionProperty<float> HealthPercentage;

        #endregion


        #region Properties

        public int CurrentHelth => _currentHealth;

        public bool IsDead => _currentHealth <= 0;

        private float HelthPerc => (float)_currentHealth / (float)_maxHealth;

        #endregion


        #region ClassLifeCycles

        public HealthHolder(int maxHealth)
        {
            _maxHealth = maxHealth;
            _currentHealth = maxHealth;

            HealthPercentage = new SubscriptionProperty<float>();

            HealthPercentage.Value = HelthPerc;
        }

        #endregion


        #region IHealthHolder

        public void GetHealth(int amount)
        {
            _currentHealth = Mathf.Max(_currentHealth + amount, _maxHealth);
        }

        public void ResetHealth(int newMaxAmount)
        {
            _maxHealth = newMaxAmount;
            _currentHealth = newMaxAmount;

            HealthPercentage.Value = HelthPerc;
        }

        public void ResetHealth()
        {
            _currentHealth = _maxHealth;

            HealthPercentage.Value = HelthPerc;
        }

        public void TakeDamage(int damage)
        {
            _currentHealth -= damage;
            Debug.Log($"Current health: {_currentHealth}");
            HealthPercentage.Value = HelthPerc;

            if (IsDead)
                Death?.Invoke();

        }

        #endregion
    }
}