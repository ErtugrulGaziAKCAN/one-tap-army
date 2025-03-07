using QuickTools.Scripts.UI;
using UnityEngine;
using UpgradeCards.Data.Base;
using UpgradeCards.UI.CardUI.Base;
namespace UpgradeCards.UI
{
    public class UpgradableCardController : MonoBehaviour
    {
//-------Public Variables-------//


//------Serialized Fields-------//

//------Private Variables-------//
        private NovaButton _novaButton;
        private UpgradeCardSo _targetUpgradeCard;
        private UpgradableCardUIBase _cardUI;
        
#region UNITY_METHODS

        private void OnDisable()
        {
            _novaButton.OnClickAction -= OnClicked;
        }

#endregion


#region PUBLIC_METHODS

        public void Assign(UpgradeCardSo upgradeCardSo, NovaButton novaButton,UpgradableCardUIBase cardUI)
        {
            _novaButton = novaButton;
            _novaButton.OnClickAction += OnClicked;
            _targetUpgradeCard = upgradeCardSo;
            _cardUI = cardUI;
        }
        
#endregion


#region PRIVATE_METHODS

        private void OnClicked(NovaButton button)
        {
            _targetUpgradeCard.UpgradeCard();
            _cardUI.OnCardUpgraded();
        }

#endregion
    }
}