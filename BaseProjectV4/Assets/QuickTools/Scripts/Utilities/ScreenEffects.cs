using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
namespace QuickTools.Scripts.Utilities
{
    public class ScreenEffects : QuickSingleton<ScreenEffects>
    {
        /****************PUBLIC_VARIABLES****************/


        /**************SERIALIZED_VARIABLES**************/
        [SerializeField] private Image ScreenEffectImage;

        /***************PRIVATE_VARIABLES****************/

#region UNITY_METHODS

        private void Start()
        {
            ScreenEffectImage.raycastTarget = false;
        }

#endregion

#region PUBLIC_METHODS

        [Button]
        public void Flash(Color color, float maxAlpha = 1f, float duration = .3f)
        {
            ScreenEffectImage.DOKill();
            color.a = maxAlpha;
            ScreenEffectImage.color = color;

            ScreenEffectImage.DOColor(Color.clear, duration);
        }

        [Button]
        public void Fade(Color color, float duration = 2f)
        {
            ScreenEffectImage.raycastTarget = true;
            ScreenEffectImage.DOKill();
            ScreenEffectImage.color = Color.clear;

            ScreenEffectImage.DOColor(color, duration / 2f)
                .SetLoops(2, LoopType.Yoyo)
                .OnComplete(() =>
                {
                    ScreenEffectImage.raycastTarget = false;
                });
        }

        [Button]
        public void SetBlackFade(float duration) => Fade(Color.black, duration);

        [Button]
        public void SetRedFade(float duration) => Fade(QuickColors.Red, duration);

#endregion

#region PRIVATE_METHODS

#endregion
    }
}