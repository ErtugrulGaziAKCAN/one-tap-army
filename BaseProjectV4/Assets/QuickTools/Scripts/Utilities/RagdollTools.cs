using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
namespace QuickTools.Scripts.Utilities
{
    public partial class RagdollController
    {
        [Button, TabGroup("Tools")]
        public void SetRagdoll(bool activate)
        {
            if (_animator == null)
            {
                _animator = GetComponent<Animator>();
            }
            _animator.enabled = !activate;
            foreach (var part in Parts)
            {
                part.Rigidbody.isKinematic = !activate;
            }

            if (HaveMainCollider)
            {
                if (_mainRigidbody == null)
                {
                    _mainRigidbody = MainCollider.gameObject.GetComponent<Rigidbody>();
                }
                _mainRigidbody.isKinematic = activate;
                MainCollider.enabled = !activate;
            }

            SetCollidersActive(activate);
            if (activate)
                OnCharacterRagdolled?.Invoke();
            IsRagdolled = activate;
        }

        [Button, TabGroup("Tools"), LabelText("AddForce")]
        public void AddForce(BoneName boneName, Vector3 forceVector, ForceMode forceMode = ForceMode.Impulse)
        {
            foreach (var part in Parts.Where(part => part.Bone == boneName))
                part.Rigidbody.AddForce(forceVector, forceMode);
        }

        [Button, TabGroup("Tools"), LabelText("AddForce Selectable")]
        public void AddForce(List<BoneName> boneNames, Vector3 forceVector, ForceMode forceMode = ForceMode.Impulse)
        {
            foreach (var part in from part in Parts from bone in boneNames.Where(bone => part.Bone == bone) select part)
            {
                part.Rigidbody.AddForce(forceVector, forceMode);
            }
        }

        [Button, TabGroup("Tools"), LabelText("Add Force All Parts")]
        public void AddForceToAllParts(Vector3 forceVector, ForceMode forceMode = ForceMode.Impulse)
        {
            foreach (var part in Parts)
            {
                part.Rigidbody.AddForce(forceVector, forceMode);
            }
        }

        [Button, TabGroup("Tools")]
        public float GetTotalMass()
        {
            return Enumerable.Sum(Parts, part => part.Rigidbody.mass);
        }

        public Part GetBodyPart(BoneName bone)
        {
            var wantedPart = new Part();
            foreach (var part in Parts.Where(part => part.Bone == bone))
            {
                wantedPart = part;
            }

            return wantedPart;
        }

        public List<Part> GetAllBodyPart()
        {
            return Parts;
        }

        [Button]
        public void SetPartLayer(LayerMask layer)
        {
            foreach (var part in Parts)
            {
                part.GameObject.layer = layer;
            }
        }


#region PRIVATE_METHODS

        [Button, PropertyOrder(999), GUIColor(0.8f, .3f, .3f), TabGroup("Tools")]
        private void DeleteRagdoll()
        {
            if (Parts.Count <= 0)
            {
                EditorDebug.Log("Parts are null!! Initialize Parts", gameObject);
                return;
            }

            foreach (var part in Parts)
            {
                DestroyImmediate(part.Collider);
                DestroyImmediate(part.Joint);
                DestroyImmediate(part.Rigidbody);
            }

            Parts = new List<Part>();
        }

        [Button, TabGroup("Tools")]
        private void SetCollisionDetectionMode(CollisionDetectionMode mode)
        {
            foreach (var part in Parts)
            {
                part.Rigidbody.collisionDetectionMode = mode;
            }
        }

        [Button, TabGroup("Tools")]
        private void SetCollidersActive(bool activate)
        {
            if (HaveMainCollider)
            {
                MainCollider.enabled = !activate;
            }

            foreach (var part in Parts)
            {
                part.Collider.enabled = activate;
            }
        }

        [Button, TabGroup("Tools")]
        private void SetGravityActive(bool value)
        {
            foreach (var part in Parts)
            {
                part.Rigidbody.useGravity = value;
            }
        }

        [Button, TabGroup("Tools")]
        private void SetTotalMass(float totalWeight)
        {
            foreach (var part in Parts)
            {
                part.Rigidbody.mass = part.Bone switch
                {
                    BoneName.Hips => totalWeight * 0.20f,
                    BoneName.LeftUpperLeg => totalWeight * 0.20f / 2,
                    BoneName.RightUpperLeg => totalWeight * 0.20f / 2,
                    BoneName.LeftLowerLeg => totalWeight * 0.15f / 2f,
                    BoneName.RightLowerLeg => totalWeight * 0.15f / 2f,
                    BoneName.LeftFoot => totalWeight * 0.05f / 2f,
                    BoneName.RightFoot => totalWeight * 0.05f / 2f,
                    BoneName.Chest => totalWeight * 0.20f,
                    BoneName.Head => totalWeight * 0.05f,
                    BoneName.LeftUpperArm => totalWeight * 0.08f / 2f,
                    BoneName.RightUpperArm => totalWeight * 0.08f / 2f,
                    BoneName.LeftLowerArm => totalWeight * 0.05f / 2f,
                    BoneName.RightLowerArm => totalWeight * 0.05f / 2f,
                    BoneName.LeftHand => totalWeight * 0.02f / 2f,
                    BoneName.RightHand => totalWeight * 0.02f / 2f,
                    _ => throw new ArgumentOutOfRangeException()
                };
            }
        }

#endregion
    }
}