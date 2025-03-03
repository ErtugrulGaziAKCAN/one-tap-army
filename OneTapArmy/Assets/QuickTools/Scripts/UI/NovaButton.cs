using System;
using DG.Tweening;
using Nova;
using QuickTools.Scripts.Utilities;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
namespace QuickTools.Scripts.UI
{
    public class NovaButton : MonoBehaviour
    {
//-------Public Variables-------//
        [OnValueChanged(nameof(UpdateInteractableState), InvokeOnInitialize = false)]
        public bool Interactable = true;
        public Action<NovaButton> OnClickAction;
        public Action<NovaButton> OnReleasedAction;
        
//------Serialized Fields-------//
        [SerializeField, OnValueChanged(nameof(ApplyColors))]
        private bool Colorize = true;

        [SerializeField] private bool ClickScaleEffect = true;

        [PropertySpace(10f), LabelText("Color"), ShowIf(nameof(Colorize)), LabelWidth(45),
         OnValueChanged(nameof(ApplyColors), InvokeOnInitialize = false, IncludeChildren = false)]
        public Color UiColor = QuickColors.Blue;
        [SerializeField] private UnityEvent OnInvoke;
        [SerializeField] private ButtonType TargetButtonType;
        [SerializeField] private bool SetFadeEffect = true;
        
//------Private Variables-------//
        private bool _isInitialized;
        private UIBlock _uiBlock;
        private Interactable _interactable;
        private ClipMask _clipMask;
        private Tween _clipMaskColorTween;

        private enum ButtonType
        {
            Clicked,
            Pressed,
            Hover
        }

#region UNITY_METHODS

        protected virtual void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            _uiBlock = GetComponent<UIBlock>();
            AddInteractable();
            AddClipMask();
            if (ClickScaleEffect)
                gameObject.AddComponent<ButtonClickFeedbacks>();
            UpdateInteractableState();
            _isInitialized = true;
        }

        protected virtual void OnEnable()
        {
            // Subscribe to desired events
            if (TargetButtonType == ButtonType.Clicked)
            {
                _uiBlock.AddGestureHandler<Gesture.OnClick>(_ => Clicked());
            }
            else if (TargetButtonType == ButtonType.Pressed)
            {
                _uiBlock.AddGestureHandler<Gesture.OnPress>(_ => Clicked());
            }
            else if (TargetButtonType == ButtonType.Hover)
            {
                _uiBlock.AddGestureHandler<Gesture.OnHover>(_ => Clicked());
            }
            _uiBlock.AddGestureHandler<Gesture.OnUnhover>(_ => OnReleased());
        }

        protected virtual void OnDisable()
        {
            // Unsubscribe from events
            if (TargetButtonType == ButtonType.Clicked)
            {
                _uiBlock.RemoveGestureHandler<Gesture.OnClick>(_ => Clicked());
            }
            else if (TargetButtonType == ButtonType.Pressed)
            {
                _uiBlock.RemoveGestureHandler<Gesture.OnPress>(_ => Clicked());
            }
            else if (TargetButtonType == ButtonType.Hover)
            {
                _uiBlock.RemoveGestureHandler<Gesture.OnHover>(_ => Clicked());
            }
            _uiBlock.RemoveGestureHandler<Gesture.OnUnhover>(_ => OnReleased());
        }

        private void Reset()
        {
            ApplyColors();
        }

#endregion


#region PUBLIC_METHODS

        public void SetInteractable(bool interactable)
        {
            if (!_isInitialized)
                return;
            Interactable = interactable;
            UpdateInteractableState();
        }

        public void Clicked()
        {
            OnInvoke?.Invoke();
            OnClickAction?.Invoke(this);
        }

#endregion


#region PRIVATE_METHODS

        private void UpdateInteractableState()
        {
            if (!Application.isPlaying)
                return;
            _interactable.enabled = Interactable;
            if(!SetFadeEffect)
                return;
            var targetColor = Interactable ? Color.white : new Color(0.5f, 0.5f, 0.5f, 0.75f);
            _clipMaskColorTween.Kill();
            _clipMaskColorTween = DOVirtual.Color(_clipMask.Tint, targetColor, .25f, (c) => _clipMask.Tint = c);
        }



        private void ApplyColors()
        {
            var colorizer = GetComponent<UiColorizeGroup>();
            if (!Colorize)
            {
                if (colorizer != null)
                    DestroyImmediate(colorizer);
                return;
            }

            colorizer ??= gameObject.AddComponent<UiColorizeGroup>();
            colorizer.hideFlags = HideFlags.HideInInspector;
            colorizer.UiColor = UiColor;
            colorizer.ApplyColors();
        }

        private void AddClipMask()
        {
            _clipMask = gameObject.AddComponent<ClipMask>();
            _clipMask.Clip = false;
            _clipMask.Tint = Color.white;
        }

        private void AddInteractable()
        {
            _interactable = GetComponent<Interactable>();
            if (_interactable != null)
                return;
            _interactable = gameObject.AddComponent<Interactable>();
            if (TargetButtonType == ButtonType.Clicked)
            {
                _interactable.ClickBehavior = ClickBehavior.OnRelease;
            }
            else if (TargetButtonType == ButtonType.Pressed)
            {
                _interactable.ClickBehavior = ClickBehavior.OnPress;

            }
            else if (TargetButtonType == ButtonType.Hover)
            {
                _interactable.ClickBehavior = ClickBehavior.None;
            }
            _interactable.Navigable = false;
            _interactable.Draggable = false;
        }

        private void OnReleased() => OnReleasedAction?.Invoke(this);

#endregion
    }
}