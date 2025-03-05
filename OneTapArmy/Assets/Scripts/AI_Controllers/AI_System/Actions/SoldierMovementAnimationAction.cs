using AI_Controllers.DataHolder.Core;
using scriptable_states.Runtime;
using UnityEngine;
namespace AI_Controllers.AI_System.Actions
{
    [CreateAssetMenu(menuName = "Scriptable State Machine/Actions/SoldierMovementAnimationAction",
        fileName = "new SoldierMovementAnimationAction")]
    public class SoldierMovementAnimationAction : ScriptableAction
    {
//-------Public Variables-------//


//------Serialized Fields-------//


//------Private Variables-------//
        private static readonly int IsWalking = Animator.StringToHash("IsWalking");

#region UNITY_METHODS

#endregion


#region PUBLIC_METHODS

        public override void Act(StateComponent statesComponent)
        {
            statesComponent.TryGetComponent(out AIDataHolderCore dataHolder);
            var isMoving = dataHolder.Agent.velocity.magnitude >= .1f;
            dataHolder.AnimationController.GetAnimator().SetBool(IsWalking, isMoving);
            if (!dataHolder.AnimationController.HasConnectedAnimations(out var connectedAnimators))
                return;
            connectedAnimators.ForEach((c) => c.SetBool(IsWalking, isMoving));
        }

#endregion


#region PRIVATE_METHODS

#endregion
    }
}