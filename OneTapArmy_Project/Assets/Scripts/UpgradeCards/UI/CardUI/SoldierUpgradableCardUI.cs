using Nova;
using QuickTools.Scripts.Utilities;
using UnityEngine;
using UpgradeCards.Data;
using UpgradeCards.Data.Base;
using UpgradeCards.UI.CardUI.Base;
namespace UpgradeCards.UI.CardUI
{
    public class SoldierUpgradableCardUI : UpgradableCardUIBase
    {
//-------Public Variables-------//


//------Serialized Fields-------//
        [SerializeField] private TextBlock HealthText;
        [SerializeField] private TextBlock AttackText;
        [SerializeField] private TextBlock SpeedText;

//------Private Variables-------//

#region UNITY_METHODS

#endregion


#region PUBLIC_METHODS

        public override void InitCard(UpgradeCardSo cardSo)
        {
            base.InitCard(cardSo);
            var card = cardSo as SoldierUpgradeCardSo;
            var currentCardLevel = card.CurrentCardLevel;
            HealthText.Text = currentCardLevel == 0
                ? string.Empty
                : "Health  +" + (card.HealthValue.GetValueOnLevel(currentCardLevel + 1) -
                                 card.HealthValue.GetValueOnLevel(currentCardLevel));
            AttackText.Text = currentCardLevel == 0
                ? string.Empty
                : "Attack  +" + (card.AttackValue.GetValueOnLevel(currentCardLevel + 1) -
                                 card.AttackValue.GetValueOnLevel(currentCardLevel));
            SpeedText.Text = currentCardLevel == 0
                ? string.Empty
                : "Speed  +" + MoneyFormat.DefaultWithoutIcon(Mathf.RoundToInt((card.SpeedValue.GetValueOnLevel(currentCardLevel + 1) -
                                                               card.SpeedValue.GetValueOnLevel(currentCardLevel)) *
                                                   100));
        }

#endregion


#region PRIVATE_METHODS

#endregion
    }
}