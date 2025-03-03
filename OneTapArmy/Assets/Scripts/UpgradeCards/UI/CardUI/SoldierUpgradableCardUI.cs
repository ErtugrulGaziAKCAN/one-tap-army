using Nova;
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
            HealthText.Text = "Health  +" + (card.HealthValue.GetValueOnLevel(card.CurrentCardLevel + 1)-card.HealthValue.GetValueOnLevel(card.CurrentCardLevel));
            AttackText.Text = "Attack  +" + (card.AttackValue.GetValueOnLevel(card.CurrentCardLevel + 1)-card.AttackValue.GetValueOnLevel(card.CurrentCardLevel));
            SpeedText.Text = "Speed  +" + (card.SpeedValue.GetValueOnLevel(card.CurrentCardLevel + 1)-card.SpeedValue.GetValueOnLevel(card.CurrentCardLevel));
        }

#endregion


#region PRIVATE_METHODS

#endregion
    }
}