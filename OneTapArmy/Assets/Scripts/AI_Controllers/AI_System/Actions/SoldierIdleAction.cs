using AI_Controllers.DataHolder.Core;
using scriptable_states.Runtime;
using UnityEngine;
namespace AI_Controllers.AI_System.Actions
{
    [CreateAssetMenu(menuName = "Scriptable State Machine/Actions/SoldierIdleAction", fileName = "new SoldierIdleAction")]
    public class SoldierIdleAction : ScriptableAction
    {
        private static readonly int IsWalking = Animator.StringToHash("IsWalking");
        //-------Public Variables-------//


//------Serialized Fields-------//

//------Private Variables-------//

#region UNITY_METHODS

#endregion


#region PUBLIC_METHODS

        public override void Act(StateComponent statesComponent)
        {
            statesComponent.TryGetComponent(out AIDataHolderCore dataHolder);
            dataHolder.AnimationController.GetAnimator().SetBool(IsWalking,false);
            if (dataHolder.AnimationController.HasConnectedAnimations(out var connectedAnimations))
            {
                connectedAnimations.ForEach((a) => a.SetBool(IsWalking,false));
            }
            dataHolder.Agent.SetDestination(dataHolder.AITransform.position);
        }

#endregion


#region PRIVATE_METHODS

#endregion
    }
}