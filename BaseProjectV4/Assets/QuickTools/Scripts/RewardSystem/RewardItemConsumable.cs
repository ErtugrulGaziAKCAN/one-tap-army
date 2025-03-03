using Sirenix.OdinInspector;
using UnityEngine;
namespace QuickTools.Scripts.RewardSystem
{
    public abstract class RewardItemConsumable : ScriptableObject, IPayableConsumable
    {
        [SerializeField] protected bool RandomAmount;
        [SerializeField, ShowIf(nameof(RandomAmount)), Indent] protected int MinAmount, MaxAmount;
        [SerializeField, HideIf(nameof(RandomAmount)), Indent] protected int Amount;


        public abstract void Pay();

        public int GetAmount()
        {
            return Amount;
        }
    }
}