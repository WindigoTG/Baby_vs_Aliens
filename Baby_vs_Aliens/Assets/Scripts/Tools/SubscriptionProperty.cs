using System;

namespace Baby_vs_Aliens.Tools
{
    public class SubscriptionProperty<T> : IReadOnlySubscriptionProperty<T>
    {
        private T _value;
        private Action<T> _onValueChange;

        public T Value
        {
            get => _value;
            set
            {
                _value = value;
                _onValueChange?.Invoke(_value);
            }
        }

        public void SubscribeOnChange(Action<T> subscriptionAction)
        {
            _onValueChange += subscriptionAction;
        }

        public void UnSubscriptibeOnChange(Action<T> unsubscriptionAction)
        {
            _onValueChange -= unsubscriptionAction;
        }
    }
}
