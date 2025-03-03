using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace QuickTools.Scripts.Utilities
{
    [AddComponentMenu("QuickTools/Fx/Scale From Zero")]
    public class ScaleFromZero : MonoBehaviour
    {
        public UnityEvent OnGrowComplete;

        [SerializeField] private bool InheritGrownScale = true;

        [SerializeField, Indent, DisableIf("InheritGrownScale")]
        private Vector3 GrownScale = Vector3.one;

        [SerializeField, HorizontalGroup(Title = "Animate On Axis", Width = 30), LabelWidth(10)]
        private bool X = true, Y = true, Z = true;

        [TitleGroup("Animation")] [SerializeField]
        private float DurationInSec = 0.25f;

        [SerializeField] private bool RandomDelay = false;

        [HideIf(nameof(RandomDelay)), SerializeField]
        private float Delay = 0f;

        [ShowIf(nameof(RandomDelay)), SerializeField, HorizontalGroup("RandomDelayGroup", LabelWidth = 60)]
        private float MinDelay = 0f;

        [ShowIf(nameof(RandomDelay)), SerializeField, HorizontalGroup("RandomDelayGroup", LabelWidth = 60)]
        private float MaxDelay = 0f;

        [SerializeField] private Ease EaseType = Ease.OutBack;

        [SerializeField, SuffixLabel("Def: 1.70158f"), ShowIf("@EaseType == Ease.OutBack"), Indent]
        private float Overshoot = 1.70158f;
        [SerializeField] private bool UnscaledAnimation;
        private Vector3 _originalScale;
        private Vector3 _zeroScale;
        private bool _isFirstTime = true;

        private void Awake()
        {
            _originalScale = InheritGrownScale ? transform.localScale : GrownScale;

            _zeroScale.x = X ? 0f : _originalScale.x;
            _zeroScale.y = Y ? 0f : _originalScale.y;
            _zeroScale.z = Z ? 0f : _originalScale.z;
        }

        private void OnEnable()
        {
            if (_isFirstTime)
                return;

            Shrink();
            var delay = RandomDelay ? Random.Range(MinDelay, MaxDelay) : Delay;
            PlayAnimation(delay);
            _isFirstTime = false;
        }

        private void OnDisable()
        {
            Shrink();
        }

        private void Start()
        {
            if (!_isFirstTime)
                return;

            Shrink();
            var delay = RandomDelay ? Random.Range(MinDelay, MaxDelay) : Delay;
            PlayAnimation(delay);
            _isFirstTime = false;
        }

        private void PlayAnimation(float delay)
        {
            var tween = transform.DOScale(_originalScale, DurationInSec).SetDelay(delay).SetEase(EaseType)
                .SetUpdate(UnscaledAnimation)
                .OnComplete(() => { OnGrowComplete?.Invoke(); });
            if (EaseType == Ease.OutBack)
                tween.easeOvershootOrAmplitude = Overshoot;
        }

        private void Shrink()
        {
            transform.localScale = _zeroScale;
        }
    }
}