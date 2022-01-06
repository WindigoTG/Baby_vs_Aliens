using System;
using UnityEngine;

namespace Baby_vs_Aliens
{
    public class HealthHolder : IHealthHolder
    {
        #region Fields

        private int _maxHealth;
        private int _currentHealth;

        public event Action Death;

        #endregion


        #region Properties

        public int CurrentHelth => _currentHealth;

        public bool IsDead => _currentHealth <= 0;

        #endregion


        #region ClassLifeCycles

        public HealthHolder(int maxHealth)
        {
            _maxHealth = maxHealth;
            _currentHealth = maxHealth;
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
        }

        public void ResetHealth()
        {
            _currentHealth = _maxHealth;
        }

        public void TakeDamage(int damage)
        {
            _currentHealth -= damage;
            Debug.Log($"Current health: {_currentHealth}");

            if (IsDead)
                Death?.Invoke();
        }

        #endregion
    }
}