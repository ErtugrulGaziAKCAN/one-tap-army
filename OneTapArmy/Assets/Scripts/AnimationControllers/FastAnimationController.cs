using System.Collections.Generic;
using Obvious.Soap.Attributes;
using Sirenix.OdinInspector;
using UnityEngine;
namespace AnimationControllers
{
    public class FastAnimationController : MonoBehaviour
    {
//-------Public Variables-------//

//------Serialized Fields-------//
        [SerializeField] private bool UseConnectedAnimations;
        [SerializeField, Sirenix.OdinInspector.ShowIf(nameof(HasConnectedAnimations))]
        private List<Animator> ConnectedAnimators;

//------Private Variables-------//
        private Animator _animator;

#region UNITY_METHODS

        private void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
        }

#endregion


#region PUBLIC_METHODS

        [Button]
        public void SetAnimation(AnimationClip clip)
        {
            SetAnimation(clip.name);
        }

        [Button]
        public void SetAnimation(string clip)
        {
            _animator.CrossFade(clip, .3f);
        }

        public void SetAnimationImmediately(AnimationClip clip) => _animator.CrossFade(clip.name, 0f);

        public Animator GetAnimator() => _animator;

        public bool HasConnectedAnimations(out List<Animator> connectedAnimators)
        {
            if (UseConnectedAnimations)
            {
                connectedAnimators = this.ConnectedAnimators;
                return true;
            }
            connectedAnimators = null;
            return false;
        }

#endregion


#region PRIVATE_METHODS

#endregion
    }
}