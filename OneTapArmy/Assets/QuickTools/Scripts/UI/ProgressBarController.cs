using System;
using Nova;
using Obvious.Soap;
using Sirenix.OdinInspector;
using UnityEngine;
namespace UI
{
    public class ProgressBarController : MonoBehaviour
    {
//-------Public Variables-------//
        [ShowInInspector, ShowIf("@Source == ProgressSource.Manual"), PropertyRange(0f, 1f), PropertyOrder(-1)]
        public float Progress
        {
            get => _progress;
            set
            {
                _progress = value;
                var block2D = Fill as UIBlock2D;
                if (block2D == null)
                    return;
                var isRadial = block2D.RadialFill.Enabled;

                if (isRadial)
                    block2D.RadialFill.FillAngle = Mathf.Lerp(0f, -360f, _progress);
                else
                {
                    if (IsYProgress)
                    {
                        block2D.Size.Y = Length.Percentage(_progress);
                    }
                    else
                    {
                        block2D.Size.X = Length.Percentage(_progress);
                    }
                }
            }
        }

//------Serialized Fields-------//
        [SerializeField, OnValueChanged(nameof(UpdateIconBackground)), EnumToggleButtons]
        private IconType Icon;

        [SerializeField, OnValueChanged(nameof(UpdateIconBackground)), EnumToggleButtons]
        private ProgressSource Source;

        [SerializeField, ShowIf("@Source == ProgressSource.RefValue")]
        private FloatReference Reference;

        [SerializeField, FoldoutGroup("References")]
        private UIBlock Fill;

        [SerializeField, FoldoutGroup("References")]
        private UIBlock Background;

        [SerializeField, FoldoutGroup("References")]
        private UIBlock IconImage;
        [SerializeField] private bool IsYProgress;
//------Private Variables-------//
        private float _progress;
        private enum IconType
        {
            None,
            Left,
            Right
        }

        private enum ProgressSource
        {
            Manual,
            RefValue
        }


#region UNITY_METHODS

        private void OnEnable()
        {
            if (Source == ProgressSource.RefValue)
            {
                Reference.Variable.OnValueChanged += UpdateProgressFromReference;
                UpdateProgressFromReference(Reference.Value);
            }
        }

        private void OnDisable()
        {
            if (Source == ProgressSource.RefValue)
                Reference.Variable.OnValueChanged -= UpdateProgressFromReference;
        }

#endregion


#region PUBLIC_METHODS

#endregion


#region PRIVATE_METHODS

        private void UpdateIconBackground()
        {
            IconImage.gameObject.SetActive(Icon != IconType.None);
            switch (Icon)
            {
                case IconType.None:
                    Background.Margin.Left = Length.Zero;
                    Background.Margin.Right = Length.Zero;
                    break;
                case IconType.Left:
                    Background.Margin.Left = Length.FixedValue(-80f);
                    Background.Margin.Right = Length.Zero;
                    IconImage.Alignment = Alignment.Left;
                    break;
                case IconType.Right:
                    Background.Margin.Left = Length.Zero;
                    Background.Margin.Right = Length.FixedValue(-80f);
                    IconImage.Alignment = Alignment.Right;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void UpdateProgressFromReference(float value)
        {
            Progress = value;
        }

#endregion
    }
}