using AI_Controllers.DataHolder.Core;
using scriptable_states.Runtime;
using UnityEngine;
namespace AI_Controllers.AI_System.Actions.Attack
{
    [CreateAssetMenu(menuName = "Scriptable State Machine/Actions/Attack/MoveToAttackAction",
        fileName = "new MoveToAttackAction")]
    public class MoveToAttackAction : ScriptableAction
    {
//-------Public Variables-------//


//------Serialized Fields-------//


//------Private Variables-------//

#region UNITY_METHODS

#endregion


#region PUBLIC_METHODS

        public override void Act(StateComponent statesComponent)
        {
            statesComponent.TryGetComponent(out AIDataHolderCore dataHolder);
            var collectiblePosition = dataHolder.ClosestRivalHealth.transform.position;
            dataHolder.Agent.SetDestination(collectiblePosition);
        }

#endregion


#region PRIVATE_METHODS

#endregion
    }
}