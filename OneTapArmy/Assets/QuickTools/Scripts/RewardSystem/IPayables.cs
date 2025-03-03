namespace QuickTools.Scripts.RewardSystem
{
    public interface IPayableConsumable
    {
        int GetAmount();
        void Pay();
    }

    public interface IPayableNonConsumable
    {
        void Pay();
    }
}