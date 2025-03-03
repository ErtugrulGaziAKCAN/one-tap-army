using Nova;
using QuickTools.Scripts.Manager;
using QuickTools.Scripts.Utilities;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
namespace QuickTools.Scripts.UI
{
    public class Buyer : MonoBehaviour
    {
//-------Public Variables-------//


//------Serialized Fields-------//
        [SerializeField] private UnityEvent OnBuySuccess;
        [SerializeField] private TextBlock CostText;

        [Space, SerializeField] private bool HasFixedCost;

        [SerializeField, EnableIf("HasFixedCost", true), Indent]
        private int FixedCost = 500;

        [SerializeField] private UnityEvent OnCanAfford;
        [SerializeField] private UnityEvent OnCannotAfford;

//------Private Variables-------//
        private int _cost;
        private bool _canAfford;


#region UNITY_METHODS

        private void OnEnable()
        {
            BalanceManager.OnBalanceChange += UpdateState;
            UpdateState(false);
        }

        private void OnDisable()
        {
            BalanceManager.OnBalanceChange -= UpdateState;
        }

        private void Start()
        {
            if (HasFixedCost)
                SetCost(FixedCost);
            _canAfford = !BalanceManager.CanBuy(_cost);
            UpdateState(false);
        }

#endregion


#region PUBLIC_METHODS

        public void SetCost(int cost)
        {
            _cost = cost;
            CostText.Text = _cost >= int.MaxValue ? "MAX" :  MoneyFormat.DefaultWithoutIcon(_cost);
            UpdateState(false);
        }

        public void AttemptToBuy()
        {
            var canBuy = BalanceManager.CanBuy(_cost);
            if (!canBuy)
                return;
            BalanceManager.AddToBalance(-_cost);
            OnBuySuccess.Invoke();
        }

        public int GetCost() => _cost;
#endregion


#region PRIVATE_METHODS

        private void UpdateState(bool _)
        {
            _canAfford = BalanceManager.CanBuy(_cost);
            if (_canAfford)
                OnCanAfford.Invoke();
            else
                OnCannotAfford.Invoke();
        }

#endregion
    }
}