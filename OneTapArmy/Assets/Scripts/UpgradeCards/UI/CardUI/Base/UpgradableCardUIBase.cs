using System.Collections.Generic;
using Nova;
using Sirenix.OdinInspector;
using UnityEngine;
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

//------Private Variables-------//

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
        }

#endregion


#region PRIVATE_METHODS

#endregion
    }
}