using QuickTools.Scripts.Manager;
using UnityEngine;
namespace QuickTools.Scripts.RewardSystem
{
    [CreateAssetMenu(fileName = "Money Reward", menuName = "QuickTools/Consumables/Money Reward")]
    public class MoneyReward : RewardItemConsumable
    {
        public override void Pay()
        {
            var amountToPay = RandomAmount ? Random.Range(MinAmount, MaxAmount + 1) : Amount;
            BalanceManager.AddToBalance(amountToPay);
        }
    }
}
