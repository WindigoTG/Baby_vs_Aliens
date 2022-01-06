using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Baby_vs_Aliens
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] SpriteRenderer _healthBar;

        private float _defaultBarSize;
        private Color _defaultColor;

        private void Awake()
        {
            _defaultBarSize = _healthBar.size.x;
            _defaultColor = _healthBar.color;
        }

        public void SetBarSize(float percentage)
        {
            _healthBar.size = new Vector2(_defaultBarSize * percentage, _healthBar.size.y);
            if (percentage < 0.5f)
                _healthBar.color = Color.red;
            else
                _healthBar.color = _defaultColor;
        }
    }
}