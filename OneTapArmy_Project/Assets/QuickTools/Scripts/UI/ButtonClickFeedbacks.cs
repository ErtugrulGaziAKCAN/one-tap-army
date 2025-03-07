using DG.Tweening;
using Lofelt.NiceVibrations;
using Nova;
using UnityEngine;
namespace QuickTools.Scripts.UI
{
    public class ButtonClickFeedbacks : MonoBehaviour
    {
        private UIBlock _uiBlock;
        private Transform _transform;
        private Vector3 _originalScale;

        private void Awake()
        {
            _uiBlock = GetComponent<UIBlock>();
            _transform = transform;
            _originalScale = _transform.localScale;
        }

        private void OnEnable()
        {
            // Subscribe to desired events
            _uiBlock.AddGestureHandler<Gesture.OnPress>(_ => Shrink());
            _uiBlock.AddGestureHandler<Gesture.OnRelease>(_ => GrowBack());
            _uiBlock.AddGestureHandler<Gesture.OnCancel>(_ => GrowBack());
        }

        private void OnDisable()
        {
            // Unsubscribe from events
            _uiBlock.RemoveGestureHandler<Gesture.OnPress>(_ => Shrink());
            _uiBlock.RemoveGestureHandler<Gesture.OnRelease>(_ => GrowBack());
            _uiBlock.RemoveGestureHandler<Gesture.OnCancel>(_ => GrowBack());
        }


        private void Shrink()
        {
            _transform.DOKill();
            _transform.localScale = _originalScale * .95f;
            HapticPatterns.PlayPreset(HapticPatterns.PresetType.SoftImpact);
        }

        private void GrowBack()
        {
            _transform.DOKill();
            _transform.DOScale(_originalScale, .15f).SetEase(Ease.OutBack).easeOvershootOrAmplitude = 6f;
            HapticPatterns.PlayPreset(HapticPatterns.PresetType.LightImpact);
        }
    }
}