using DG.Tweening;
using UnityEngine;
namespace QuickTools.Scripts.TimeSystem
{
    public static class TimescaleManager
    {
        /****************PUBLIC_VARIABLES****************/


        /**************SERIALIZED_VARIABLES**************/


        /***************PRIVATE_VARIABLES****************/


#region PUBLIC_METHODS

        public static void EnterSlowMotion(float targetTimescale, float transitionDurationInSeconds,
            float slowMotionDurationInSeconds)
        {
            var originalTimescale = Time.timeScale < 1f ? 1f : Time.timeScale;
            DOVirtual.Float(originalTimescale, targetTimescale, transitionDurationInSeconds, SetTimescale)
                .SetEase(Ease.OutQuad).OnComplete(() =>
                {
                    DOVirtual.DelayedCall(slowMotionDurationInSeconds, () =>
                    {
                        DOVirtual.Float(targetTimescale, originalTimescale, transitionDurationInSeconds, SetTimescale)
                            .SetEase(Ease.InQuad)
                            .timeScale = 1f / targetTimescale;
                    });
                });
        }

        public static void ChangeTimescale(float targetTimescale, float transitionDurationInSeconds)
        {
            var originalTimescale =  Time.timeScale;
            DOVirtual.Float(originalTimescale, targetTimescale, transitionDurationInSeconds, SetTimescale)
                .SetEase(Ease.OutQuad).SetUpdate(true);
        }

        public static void FrameFreeze(int frameCount)
        {
            var originalTimescale = Time.timeScale < 1f ? 1f : Time.timeScale;
            var durationInSeconds = Time.unscaledDeltaTime * frameCount;
            SetTimescale(0f);
            DOVirtual.DelayedCall(durationInSeconds, () => SetTimescale(originalTimescale));
        }

#endregion

#region PRIVATE_METHODS

        private static void SetTimescale(float newScale)
        {
            Time.timeScale = newScale;
            Time.fixedDeltaTime = newScale * .02f;
        }

#endregion
    }
}