using System.Collections.Generic;
using QuickTools.Scripts.Extensions;
using Sirenix.OdinInspector;
using UnityEngine;
namespace QuickTools.Scripts.Utilities
{
    public class NineSliceCube : MonoBehaviour
    {
//-------Public Variables-------//


//------Serialized Fields-------//
        [TabGroup("Config"), SerializeField, EnumToggleButtons, OnValueChanged(nameof(ApplyBevelSize)),
         PropertyOrder(999)]
        private BevelSize BevelType;

        [TabGroup("Config"), SerializeField] private Vector3 Size = Vector3.one;

        [TabGroup("References")] [SerializeField]
        private Transform LFrontUp;

        [TabGroup("References")] [SerializeField]
        private Transform LFrontDown;

        [TabGroup("References")] [SerializeField]
        private Transform LBackUp;

        [TabGroup("References")] [SerializeField]
        private Transform LBackDown;

        [TabGroup("References")] [SerializeField]
        private Transform RFrontUp;

        [TabGroup("References")] [SerializeField]
        private Transform RFrontDown;

        [TabGroup("References")] [SerializeField]
        private Transform RBackUp;

        [TabGroup("References")] [SerializeField]
        private Transform RBackDown;

        [TabGroup("References"), SerializeField]
        private GameObject Small;

        [TabGroup("References"), SerializeField]
        private GameObject Medium;

        [TabGroup("References"), SerializeField]
        private GameObject Large;


//------Private Variables-------//
        private enum BevelSize
        {
            Small,
            Medium,
            Large
        }

#region UNITY_METHODS

#endregion


#region PUBLIC_METHODS

#endregion


#region PRIVATE_METHODS

        [Button("Apply Size", ButtonSizes.Large)]
        private void UpdateBonePositions()
        {
            foreach (var leftBone in GetLeftBones())
            {
                leftBone.localPosition = leftBone.localPosition.WithX(-Size.x / 2f);
            }

            foreach (var rightBone in GetRightBones())
            {
                rightBone.localPosition = rightBone.localPosition.WithX(Size.x / 2f);
            }

            foreach (var frontBone in GetFrontBones())
            {
                frontBone.localPosition = frontBone.localPosition.WithZ(Size.z / 2f);
            }

            foreach (var backBone in GetBackBones())
            {
                backBone.localPosition = backBone.localPosition.WithZ(-Size.z / 2f);
            }

            foreach (var upBone in GetUpBones())
            {
                upBone.localPosition = upBone.localPosition.WithY(Size.y);
            }

            foreach (var backBone in GetDownBones())
            {
                backBone.localPosition = backBone.localPosition.WithY(0f);
            }

            var col = GetComponent<BoxCollider>();
            if (!col)
                col = gameObject.AddComponent<BoxCollider>();
            col.center = Vector3.up.WithY(Size.y / 2f);
            col.size = Size;
        }

        private List<Transform> GetFrontBones()
        {
            return new List<Transform>()
            {
                LFrontUp,
                LFrontDown,
                RFrontUp,
                RFrontDown
            };
        }

        private List<Transform> GetBackBones()
        {
            return new List<Transform>()
            {
                LBackUp,
                LBackDown,
                RBackUp,
                RBackDown,
            };
        }

        private List<Transform> GetLeftBones()
        {
            return new List<Transform>()
            {
                LFrontUp,
                LBackUp,
                LFrontDown,
                LBackDown,
            };
        }

        private List<Transform> GetRightBones()
        {
            return new List<Transform>()
            {
                RFrontUp,
                RBackUp,
                RFrontDown,
                RBackDown,
            };
        }

        private List<Transform> GetUpBones()
        {
            return new List<Transform>()
            {
                LFrontUp,
                LBackUp,
                RFrontUp,
                RBackUp,
            };
        }

        private List<Transform> GetDownBones()
        {
            return new List<Transform>()
            {
                LFrontDown,
                LBackDown,
                RFrontDown,
                RBackDown,
            };
        }

        private void ApplyBevelSize()
        {
            Small.SetActive(BevelType == BevelSize.Small);
            Medium.SetActive(BevelType == BevelSize.Medium);
            Large.SetActive(BevelType == BevelSize.Large);
        }

#endregion
    }
}