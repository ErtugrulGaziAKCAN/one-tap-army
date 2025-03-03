using System.Collections;
using DG.Tweening;
using Nova;
using QuickTools.Scripts.Extensions;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
namespace Effects.TransformEffects
{
    public class SlideFromCenter : MonoBehaviour
    {
//-------Public Variables-------//


//------Serialized Fields-------//
        [SerializeField] private bool InheritCenterPos;
        [SerializeField, ShowIf(nameof(InheritCenterPos))] private Vector3 CenterPos;
        [SerializeField] private float DistanceFromCenter;
        [SerializeField] private float FxDuration = .45f;
        [SerializeField] private float FXDelay;
        [SerializeField] private Slider.Direction EffectDirection;
        [SerializeField] private bool PlayOnEnable;
        [SerializeField, ShowIf(nameof(PlayOnEnable))] private bool AppearOnEnable = true;
        [SerializeField] private bool IsNovaUI;
        [SerializeField] private Ease AppearEase = Ease.OutBack;
        [SerializeField] private Ease DisappearEase = Ease.InBack;
        [SerializeField] private UnityEvent OnAppeared;
        [SerializeField] private UnityEvent OnDisappeared;

//------Private Variables-------//
        private Vector3 _defPos;
        private Vector3 _dirPos;
        private Tween _moveTween;
        private bool _isEnabled;
        private bool _isInit;
        private bool _isPlayedOnce;
        private UIBlock _uiBlock;

#region UNITY_METHODS

        private void Awake()
        {
            if (IsNovaUI)
            {
                TryGetComponent(out _uiBlock);
                _defPos = InheritCenterPos ? CenterPos : _uiBlock.Position.Value;
            }
            else
            {
                _defPos = InheritCenterPos ? CenterPos : transform.localPosition;
            }
            _isEnabled = !AppearOnEnable;
            _dirPos = PositionByDirection();
        }

        private IEnumerator Start()
        {
            if (!IsNovaUI)
                Init();
            yield return new WaitForEndOfFrame();
            if (IsNovaUI)
                Init();
        }


        private void OnEnable()
        {
            if (_isInit && PlayOnEnable)
                PlaySlideFx(AppearOnEnable);
        }

#endregion


#region PUBLIC_METHODS

        [Button]
        public void PlaySlideFx(bool enable)
        {
            if (_isEnabled == enable && _isPlayedOnce)
                return;
            if (!_isInit)
            {
                if (!IsNovaUI)
                    transform.localPosition = enable ? _dirPos : _defPos;
                else
                    _uiBlock.Position = enable ? _dirPos : _defPos;
            }

            _isEnabled = enable;
            if (FXDelay > 0)
            {
                DOVirtual.DelayedCall(FXDelay, () => TriggerFX(enable));
                return;
            }
            TriggerFX(enable);
        }

        [Button]
        public void PlaySlideFxWithNoDelay(bool enable)
        {
            var delay = FXDelay;
            FXDelay = 0f;
            PlaySlideFx(enable);
            FXDelay = delay;
        }

#endregion


#region PRIVATE_METHODS

        private void Init()
        {
            if (PlayOnEnable)
                PlaySlideFx(AppearOnEnable);
            _isInit = true;
        }

        private void TriggerFX(bool enable)
        {
            if (enable)
                OnAppeared?.Invoke();
            _isPlayedOnce = true;
            var targetEase = enable ? AppearEase : DisappearEase;
            _moveTween.Kill();
            if (!IsNovaUI)
                _moveTween = transform.DOLocalMove(enable ? _defPos : _dirPos, FxDuration).SetEase(targetEase)
                    .OnComplete(
                        () =>
                        {
                            if (!enable)
                                OnDisappeared?.Invoke();
                        });
            else
            {
                _moveTween = DOVirtual.Vector3(_uiBlock.Position.Value, enable ? _defPos : _dirPos, FxDuration,
                    (p) => _uiBlock.Position = p).SetEase(targetEase).OnComplete(() =>
                {
                    if (!enable)
                        OnDisappeared?.Invoke();
                });
            }
        }

        private Vector3 PositionByDirection()
        {
            var dirPos = EffectDirection switch
            {
                Slider.Direction.LeftToRight => _defPos.WithAddedX(-DistanceFromCenter),
                Slider.Direction.RightToLeft => _defPos.WithAddedX(DistanceFromCenter),
                Slider.Direction.BottomToTop => _defPos.WithAddedY(-DistanceFromCenter),
                Slider.Direction.TopToBottom => _defPos.WithAddedY(DistanceFromCenter),
                _ => Vector3.zero
            };
            return dirPos;
        }

#endregion
    }
}