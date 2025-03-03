using Nova;
using QuickTools.Scripts.UI;
using Sirenix.OdinInspector;
using UnityEngine;
namespace QuickTools.QuickUpgradables.Scripts
{
    public partial class UpgradeableButton : MonoBehaviour
    {
//-------Public Variables-------//


//------Serialized Fields-------//
        [SerializeField] private UpgradableSo UpgradeData;

        [SerializeField, FoldoutGroup("References")]
        private Buyer BuyController;

        [SerializeField, FoldoutGroup("References")]
        private UIBlock2D IconImage;

        [SerializeField, FoldoutGroup("References")]
        private TextBlock NameText;

        [SerializeField, FoldoutGroup("Config")]
        private UpgradeableButton.CurrentValueTextDetail CurrentValueText;


//------Private Variables-------//


#region UNITY_METHODS

        private void Awake()
        {
            UpgradeData.Load();
        }

        private void OnEnable()
        {
            UpdateUi();
        }

        private void Start()
        {
            ApplyVisuals();
        }

        private void OnApplicationQuit()
        {
            UpgradeData.Save();
        }

#endregion


#region PUBLIC_METHODS

        public void BuySuccessful()
        {
            UpgradeData.Upgrade();
            UpdateUi();
        }

        public UpgradableSo GetUpgradable() => UpgradeData;

#endregion


#region PRIVATE_METHODS

        [Button]
        private void ApplyVisuals()
        {
            IconImage.SetImage(UpgradeData.Icon);
            NameText.Text = UpgradeData.Name;
        }

        private void UpdateUi()
        {
            BuyController.SetCost(UpgradeData.IsMaxedOut ? int.MaxValue : UpgradeData.UpgradeCost);
            CurrentValueText.SetText(GetValueText());
        }

        private string GetValueText()
        {
            var mid = CurrentValueText.Type switch
            {
                UpgradeableButton.CurrentValueTextDetail.TextType.Level => UpgradeData.IsMaxedOut
                    ? "MAX"
                    : UpgradeData.Level.ToString(),
                UpgradeableButton.CurrentValueTextDetail.TextType.Value => UpgradeData.CurrentValue.ToString("0.##"),
                _ => string.Empty
            };

            return CurrentValueText.Prefix + mid + CurrentValueText.Suffix;
        }

#endregion
    }
}