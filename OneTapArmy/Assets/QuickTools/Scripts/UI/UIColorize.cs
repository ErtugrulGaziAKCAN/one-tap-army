using Nova;
using QuickTools.Scripts.Extensions;
using QuickTools.Scripts.Utilities;
using Sirenix.OdinInspector;
using UnityEngine;
namespace QuickTools.Scripts.UI
{
    [AddComponentMenu("QuickTools/UI/UI Colorize")]
    public class UIColorize : MonoBehaviour
    {
        [Required("Assign Image to be colorized!")]
        public UIBlock ColorImage;

        public bool IgnoreGroup;

        [PropertySpace(10f), LabelText("Color"), ColorPalette("QuickColors"), HorizontalGroup("Split", .8f),
         LabelWidth(45)]
        public Color UiColor = QuickColors.Blue;

        [SerializeField, Range(-3, 3)] private float Shade;

        private void OnEnable()
        {
            ApplyColor();
        }

        [Button("Apply", ButtonHeight = 25), PropertySpace(10f), HorizontalGroup("Split/Right")]
        public void ApplyColor()
        {
            if (ColorImage != null)
            {
                ColorImage.Color = UiColor.GetShadedColor(Shade);
            }
            else
            {
                ColorImage = GetComponent<UIBlock>();
                if (ColorImage)
                    ApplyColor();
            }
        }

        private void Reset()
        {
            ColorImage ??= GetComponent<UIBlock>();
        }
    }
}