using AI_Controllers.DataHolder;
using scriptable_states.Runtime;
using UnityEngine;
namespace AI_Controllers.AI_System.Actions.Attack
{
    [CreateAssetMenu(menuName = "Scriptable State Machine/Actions/Attack/AttackExitAction",
        fileName = "new AttackExitAction")]
    public class AttackExitAction : ScriptableAction
    {
//-------Public Variables-------//


//------Serialized Fields-------//


//------Private Variables-------//

#region UNITY_METHODS

#endregion


#region PUBLIC_METHODS

        public override void Act(StateComponent statesComponent)
        {
            statesComponent.TryGetComponent(out SoldierAIDataHolderCore dataHolder);
            dataHolder.IsAttacking = false;
            dataHolder.Agent.stoppingDistance = dataHolder.AgentStoppingDistance;
            dataHolder.Agent.isStopped = false;
        }

#endregion


#region PRIVATE_METHODS

#endregion
    }
}