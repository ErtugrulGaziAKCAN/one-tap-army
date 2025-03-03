using System.Collections.Generic;
using Nova;
using QuickTools.Scripts.UI;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UpgradeCards.Data.Base;
namespace UpgradeCards.UI.CardUI.Base
{
    public abstract class UpgradableCardUIBase : MonoBehaviour
    {
//-------Public Variables-------//


//------Serialized Fields-------//
        [SerializeField, BoxGroup("References")] private UIBlock2D CardImage;
        [SerializeField, BoxGroup("References")] private UIBlock2D CardNameImage;
        [SerializeField, BoxGroup("References")] private UIBlock2D CardBackground;
        [SerializeField, BoxGroup("References")] private List<GameObject> Stars;
        [SerializeField, BoxGroup("Events")] private UnityEvent OnDeInteractable;

//------Private Variables-------//
        private UpgradableCardController _upgradableCardController;
        private NovaButton _novaButton;

#region UNITY_METHODS

#endregion


#region PUBLIC_METHODS

        [Button]
        public virtual void InitCard(UpgradeCardSo cardSo)
        {
            CardImage.SetImage(cardSo.TargetCardImage());
            CardNameImage.SetImage(cardSo.CardNameSprite);
            CardBackground.SetImage(cardSo.BackgroundImage);
            for (var index = 0; index < Stars.Count; index++)
            {
                var star = Stars[index];
                star.SetActive(index <= (cardSo.CurrentCardLevel - 1));
            }
            TryGetComponent(out _novaButton);
            TryGetComponent(out _upgradableCardController);
            _upgradableCardController.Assign(cardSo, _novaButton);
        }
        
        public void DeInteractable()
        {
            _novaButton.SetInteractable(false);
            OnDeInteractable?.Invoke();
        }

#endregion


#region PRIVATE_METHODS

#endregion
    }
}