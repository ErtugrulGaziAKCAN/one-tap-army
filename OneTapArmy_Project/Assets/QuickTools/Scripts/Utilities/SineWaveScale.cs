using DG.Tweening;
using UnityEngine;
namespace QuickTools.Scripts.Utilities
{
    [AddComponentMenu("QuickTools/Fx/Sine Wave Scale")]
    public class SineWaveScale : MonoBehaviour
    {
        [SerializeField] private float ScaleTime = 1f;
        [SerializeField, Range(0f, 1f)] private float RandomSpeedFactor = 0f;
        [Space, SerializeField] private float ScaleFactor = .1f;
        [SerializeField, Range(0f, 1f)] private float RandomScaleFactor = 0f;

        private Vector3 initialScale;
        private Vector3 randScale;
        private float randDuration = 1f;
        private bool _wasLastMoveGrow;

        private void Awake()
        {
            var scaleFromZero = GetComponent<ScaleFromZero>();
            if (scaleFromZero)
            {
                scaleFromZero.OnGrowComplete.AddListener(Initialize);
                return;
            }

            Initialize();
        }


        private void Initialize()
        {
            initialScale = transform.localScale == Vector3.zero ? Vector3.one : transform.localScale;
            _wasLastMoveGrow = Random.value < 0.5f;
            SetValues();
        }

        private void SetValues()
        {
            var randSmallScale =
                initialScale - (initialScale * (ScaleFactor + Random.Range(0f, RandomScaleFactor)));
            var randBigScale = initialScale + initialScale * (ScaleFactor + Random.Range(0f, RandomScaleFactor));

            randDuration = ScaleTime + ScaleTime * Random.Range(-RandomSpeedFactor, RandomSpeedFactor);
            transform.DOScale(randSmallScale, randDuration).SetEase(Ease.InOutSine).OnComplete(() =>
            {
                transform.DOScale(randBigScale, randDuration).SetEase(Ease.InOutSine).OnComplete(() =>
                {
                    _wasLastMoveGrow = !_wasLastMoveGrow;
                    SetValues();
                });
            });
        }

        private void OnDisable()
        {
            transform.DOKill();
        }
    }
}