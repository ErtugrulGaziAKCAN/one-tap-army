using Plugins.SaveGameFree.Scripts;
using UnityEngine;
namespace QuickTools.Scripts.RewardSystem
{
    public abstract class RewardItemNonConsumable : ScriptableObject, IPayableNonConsumable
    {
        public bool IsPaidBefore { get; internal set; }
        protected abstract string SaveKey { get; }

        public abstract void Pay();

        public void Load()
        {
            IsPaidBefore = SaveGame.Load(SaveKey + "_isPaid", false);
        }
        
        public void Save()
        {
            SaveGame.Save(SaveKey + "_isPaid", IsPaidBefore);
        }
    }
}