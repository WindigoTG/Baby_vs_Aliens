using System;

namespace Baby_vs_Aliens.Tools
{
    public interface IReadOnlySubscriptionProperty<T>
    {
        T Value { get; }
        void SubscribeOnChange(Action<T> subscriptionAction);
        void UnSubscriptibeOnChange(Action<T> unsubscriptionAction);
    }
}
