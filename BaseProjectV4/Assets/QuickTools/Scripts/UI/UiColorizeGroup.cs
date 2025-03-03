using System.Collections.Generic;
using System.Linq;
using QuickTools.Scripts.Utilities;
using Sirenix.OdinInspector;
using UnityEngine;
namespace QuickTools.Scripts.UI
{
    [AddComponentMenu("QuickTools/UI/UI Colorize Group")]
    public class UiColorizeGroup : MonoBehaviour
    {
//-------Public Variables-------//


//------Serialized Fields-------//
        [PropertySpace(10f), LabelText("Color"), ColorPalette("IncludeQuickTools"), HorizontalGroup("Split", .8f),
         LabelWidth(45)]
        public Color UiColor = QuickColors.Blue;

//------Private Variables-------//
        [ShowInInspector, ReadOnly] private List<UiColorizeGroup> _children = new List<UiColorizeGroup>();


#region UNITY_METHODS

        private void Awake()
        {
            GetChildGroups();
        }

        private void Start()
        {
            ApplyColors();
        }

#endregion


#region PUBLIC_METHODS

        [Button]
        public void ApplyColors()
        {
            GetChildGroups();
            foreach (var uiColorize in GetComponentsInChildren<UIColorize>(true))
            {
                if (uiColorize.IgnoreGroup)
                    continue;
                if (_children.Count <= 0)
                {
                    uiColorize.UiColor = UiColor;
                    uiColorize.ApplyColor();
                }

                var childGroupHasThis = false;
                foreach (var childGroup in _children)
                {
                    var uiColorizeInChildren = childGroup.transform.GetComponentsInChildren<UIColorize>(true);
                    if (uiColorizeInChildren.Contains(uiColorize))
                    {
                        childGroupHasThis = true;
                        break;
                    }
                }

                if (childGroupHasThis)
                    continue;
                uiColorize.UiColor = UiColor;
                uiColorize.ApplyColor();
            }

            foreach (var uiColorize in GetComponentsInChildren<UiColorizeGradient>(true))
            {
                if (uiColorize.IgnoreGroup)
                    continue;

                if (_children.Count <= 0)
                {
                    uiColorize.UiColor = UiColor;
                    uiColorize.ApplyColors();
                }

                var childGroupHasThis = false;
                foreach (var childGroup in _children)
                {
                    var uiColorizeGradientsInChildren =
                        childGroup.transform.GetComponentsInChildren<UiColorizeGradient>(true);
                    if (uiColorizeGradientsInChildren.Contains(uiColorize))
                    {
                        childGroupHasThis = true;
                        break;
                    }
                }

                if (childGroupHasThis)
                    continue;
                uiColorize.UiColor = UiColor;
                uiColorize.ApplyColors();
            }
        }

#endregion


#region PRIVATE_METHODS

        private void GetChildGroups()
        {
            _children = new List<UiColorizeGroup>();
            foreach (var group in GetComponentsInChildren<UiColorizeGroup>(true))
            {
                if (group == this)
                    continue;
                _children.Add(group);
            }
        }

#endregion
    }
}