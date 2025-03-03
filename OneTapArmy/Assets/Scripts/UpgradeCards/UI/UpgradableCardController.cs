using QuickTools.Scripts.UI;
using UnityEngine;
using UpgradeCards.Data.Base;
namespace UpgradeCards.UI
{
    public class UpgradableCardController : MonoBehaviour
    {
//-------Public Variables-------//


//------Serialized Fields-------//

//------Private Variables-------//
        private NovaButton _novaButton;
        private UpgradeCardSo _targetUpgradeCard;

#region UNITY_METHODS

        private void OnDisable()
        {
            _novaButton.OnClickAction -= OnClicked;
        }

#endregion


#region PUBLIC_METHODS

        public void Assign(UpgradeCardSo upgradeCardSo, NovaButton novaButton)
        {
            _novaButton = novaButton;
            _novaButton.OnClickAction += OnClicked;
            _targetUpgradeCard = upgradeCardSo;
        }
        
#endregion


#region PRIVATE_METHODS

        private void OnClicked(NovaButton button)
        {
            _targetUpgradeCard.UpgradeCard();
        }

#endregion
    }
}