using System.Collections.Generic;
using System.Linq;
using Plugins.SaveGameFree.Scripts;
using Sirenix.OdinInspector;
using UnityEngine;
namespace QuickTools.Scripts.Manager
{
    public class BalanceManager : MonoBehaviour
    {
//-------Public Variables-------//
        public delegate void BalanceChanged(bool willAnimate);

        public static event BalanceChanged OnBalanceChange;
        public static readonly List<Transform> ActiveBalanceTexts = new List<Transform>();
        public static Transform ActiveBalanceText => ActiveBalanceTexts.LastOrDefault();
        public static int CurrentBalance { get; set; }


//------Serialized Fields-------//

//------Private Variables-------//
        private const string BALANCE_SAVE_KEY = "Balance_Money";


        private void Awake()
        {
            Load();
        }

        private void OnDisable()
        {
            Save();
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            if (hasFocus)
                return;
            Save();
        }

        [Button]
        public void SetBalance(int amount)
        {
            CurrentBalance = amount;
            OnBalanceChange?.Invoke(false);
        }

        [Button]
        public static void AddToBalance(int amount)
        {
            CurrentBalance += amount;
            OnBalanceChange?.Invoke(false);
        }

        public static void AddToBalance(int amount, int spawnCoinsCount, Vector2 viewportPosition)
        {
            CurrentBalance += amount;
            OnBalanceChange?.Invoke(true);
        }

        public static void AddToBalance(int amount, int spawnCoinsCount, Transform source)
        {
            CurrentBalance += amount;
            OnBalanceChange?.Invoke(true);
        }

        public static bool CanBuy(int price)
        {
            return CurrentBalance >= price;
        }

        private void Load()
        {
            var balance = SaveGame.Exists(BALANCE_SAVE_KEY) ? SaveGame.Load(BALANCE_SAVE_KEY, 0) : 0;
            SetBalance(balance);
        }

        private void Save()
        {
            SaveGame.Save(BALANCE_SAVE_KEY, CurrentBalance);
        }
    }
}