using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
namespace QuickTools.Scripts.Utilities
{
    [AddComponentMenu("QuickTools/Ragdoll/Ragdoll Controller")]
    [RequireComponent(typeof(Animator))]
    public partial class RagdollController : MonoBehaviour
    {
//-------Public Variables-------//
        [ReadOnly, TabGroup("Config")] public bool IsRagdolled;

//------Serialized Fields-------//
        [SerializeField, TabGroup("Config")] private bool HaveMainCollider;

        [Indent, ShowIf("HaveMainCollider"), SerializeField, TabGroup("Config")]
        private Collider MainCollider;

        [TableList] [SerializeField, TabGroup("Config")]
        private List<Part> Parts;

        [SerializeField, TabGroup("Config")] private bool ActiveRagdollOnAwake;

        [SerializeField, TabGroup("Config"), ShowIf(nameof(ActiveRagdollOnAwake))]
        private bool IncludeColliders;

        [SerializeField, TabGroup("Events")] private UnityEvent OnCharacterRagdolled;

//------Private Variables-------//
        private Animator _animator;
        private Rigidbody _mainRigidbody;

#region UNITY_METHODS

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            if (HaveMainCollider)
                _mainRigidbody = MainCollider.gameObject.GetComponent<Rigidbody>();
            if (ActiveRagdollOnAwake)
            {
                SetRagdoll(true);
            }
        }

#endregion


#region PUBLIC_METHODS

#endregion


#region PRIVATE_METHODS

        [PropertySpace, PropertyOrder(999)]
        [Button(ButtonSizes.Large), GUIColor(0.3f, .8f, .3f), TabGroup("Config")]
        private void Initialize()
        {
            if (_animator == null)
                _animator = GetComponent<Animator>();
            Parts = new List<Part>();
            Parts = InitializeParts();
        }

        private List<Part> InitializeParts()
        {
            var parts = new List<Part>
            {
                GetPart(HumanBodyBones.Hips),
                GetPart(HumanBodyBones.LeftUpperLeg),
                GetPart(HumanBodyBones.LeftLowerLeg),
                GetPart(HumanBodyBones.RightUpperLeg),
                GetPart(HumanBodyBones.RightLowerLeg),
                GetPart(HumanBodyBones.LeftUpperArm),
                GetPart(HumanBodyBones.LeftLowerArm),
                GetPart(HumanBodyBones.RightUpperArm),
                GetPart(HumanBodyBones.RightLowerArm),
                GetPart(HumanBodyBones.Chest),
                GetPart(HumanBodyBones.Head),
                GetPart(HumanBodyBones.LeftFoot),
                GetPart(HumanBodyBones.RightFoot),
                GetPart(HumanBodyBones.LeftHand),
                GetPart(HumanBodyBones.RightHand)
            };
            parts = parts.Where(part => part != null).ToList();
            return parts;
        }

        private Part GetPart(HumanBodyBones bone)
        {
            var boneIndex = 999;
            var boneNames = System.Enum.GetNames(typeof(BoneName));
            for (var i = 0; i < boneNames.Length; i++)
            {
                if (bone.ToString() == boneNames[i])
                {
                    boneIndex = i;
                }
            }

            var newPart = new Part
            {
                Bone = (BoneName)boneIndex,
                Collider = _animator.GetBoneTransform(bone).GetComponent<Collider>(),
                Rigidbody = _animator.GetBoneTransform(bone).GetComponent<Rigidbody>(),
                Joint = _animator.GetBoneTransform(bone).GetComponent<Joint>(),
                Transform = _animator.GetBoneTransform(bone),
                GameObject = _animator.GetBoneTransform(bone).gameObject
            };
            if (newPart.Collider != null)
            {
                return newPart;
            }

            foreach (Transform child in newPart.Rigidbody.transform)
            {
                if (!child.gameObject.name.Contains(newPart.Rigidbody.transform.name))
                    continue;

                newPart.Collider = child.GetComponent<Collider>();
                break;
            }

            return newPart;
        }

        [System.Serializable]
        public class Part
        {
            public Rigidbody Rigidbody;
            public Collider Collider;
            [HideInInspector] public Joint Joint;
            public BoneName Bone;
            public Transform Transform;
            public GameObject GameObject;
        }

        public enum BoneName
        {
            Hips,
            LeftUpperLeg,
            LeftLowerLeg,
            RightUpperLeg,
            RightLowerLeg,
            LeftUpperArm,
            LeftLowerArm,
            RightUpperArm,
            RightLowerArm,
            Chest,
            Head,
            LeftFoot,
            RightFoot,
            LeftHand,
            RightHand
        }

#endregion
    }
}