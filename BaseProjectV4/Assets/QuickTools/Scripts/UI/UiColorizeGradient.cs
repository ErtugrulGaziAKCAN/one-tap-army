using Nova;
using QuickTools.Scripts.Extensions;
using QuickTools.Scripts.Utilities;
using Sirenix.OdinInspector;
using UnityEngine;
namespace QuickTools.Scripts.UI
{
    [AddComponentMenu("QuickTools/UI/UI Colorize Gradient")]
    public class UiColorizeGradient : MonoBehaviour
    {
//-------Public Variables-------//


//------Serialized Fields-------//
        [PropertySpace(10f), LabelText("Color"), ColorPalette("QuickColors"), HorizontalGroup("Split", .8f), LabelWidth(45)]
        public Color UiColor = QuickColors.Blue;
        public bool IgnoreGroup;

        [SerializeField] private float Shade = -2;
        [SerializeField, Range(0f, 1f)] private float Alpha = 1f;

//------Private Variables-------//
        private UIBlock2D _gradient;


#region UNITY_METHODS

        private void Awake()
        {
            _gradient = GetComponent<UIBlock2D>();
        }

#endregion


#region PUBLIC_METHODS
        [Button]
        public void ApplyColors()
        {
            if (_gradient == null)
                _gradient = GetComponent<UIBlock2D>();

            var color = UiColor.GetShadedColor(Shade);
            color.a = Alpha;
            var gradientRef = _gradient.Gradient;
            gradientRef.Color = color;
            _gradient.Gradient = gradientRef;
        }

#endregion


#region PRIVATE_METHODS


#endregion
    }
}