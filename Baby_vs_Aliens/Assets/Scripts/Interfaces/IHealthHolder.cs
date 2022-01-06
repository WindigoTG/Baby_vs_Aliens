
namespace Baby_vs_Aliens
{
    public interface IHealthHolder
    {
        int CurrentHelth { get; }
        event System.Action Death;
        bool IsDead{ get; }
        void TakeDamage(int damage);
        void GetHealth(int amount);
        void ResetHealth(int newMaxAmount);
        void ResetHealth();
    }
}