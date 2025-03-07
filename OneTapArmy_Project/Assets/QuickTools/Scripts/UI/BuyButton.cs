using QuickTools.Scripts.Manager;
using QuickTools.Scripts.Utilities;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
namespace QuickTools.Scripts.UI
{
    [RequireComponent(typeof(Button))]
    public class BuyButton : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI CostText = null;
        [SerializeField] private string Prefix = "";
        [SerializeField] private string Suffix = "";

        [Space, SerializeField] private bool HasFixedCost = false;
        [EnableIf("HasFixedCost", true), Indent(1)]
        [SerializeField] private int FixedCost = 500;

        [Space, SerializeField] private UnityEvent OnBuySuccessful = null;

        private int _amount = 0;
        private Button _button;

        private void Awake()
        {
            Initialize();
        }

        private void Start()
        {
            _button.onClick.AddListener(Clicked);
        }

        private void OnEnable()
        {
            BalanceManager.OnBalanceChange += UpdateState;
            if (HasFixedCost)
                SetAmount(FixedCost);
            UpdateState(true);
        }

        private void OnDisable()
        {
            BalanceManager.OnBalanceChange -= UpdateState;
        }

        public void SetAmount(int amount)
        {
            _amount = amount;
            CostText.SetText(Prefix + MoneyFormat.DefaultWithoutIcon(_amount) + Suffix);
            UpdateState(true);
        }

        private void Initialize()
        {
            _button = GetComponent<Button>();
            if (_button == null)
                _button = gameObject.AddComponent<Button>();
        }

        private void Clicked()
        {
            if (BalanceManager.CanBuy(_amount))
            {
                BalanceManager.AddToBalance(-_amount);
                OnBuySuccessful.Invoke();
            }
            else
            {
                UpdateState(true);
            }
        }

        private void UpdateState(bool _)
        {
            bool canAfford = BalanceManager.CanBuy(_amount);
            if (_button != null)
            {
                _button.interactable = canAfford;
            }
        }
    }
}