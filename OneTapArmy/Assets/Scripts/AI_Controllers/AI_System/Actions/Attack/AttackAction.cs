using AI_Controllers.DataHolder;
using AI_Controllers.DataHolder.Core;
using scriptable_states.Runtime;
using UnityEngine;
namespace AI_Controllers.AI_System.Actions.Attack
{
    [CreateAssetMenu(menuName = "Scriptable State Machine/Actions/Attack/AttackAction",
        fileName = "new AttackAction")]
    public class AttackAction : ScriptableAction
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
            if (dataHolder.IsAttacking)
                return;
            dataHolder.AIAttackController.StartAttacking();
            dataHolder.IsAttacking = true;
            var soldierData = dataHolder as SoldierAIDataHolderCore;
            if (soldierData == null)
                return;
            soldierData.Agent.isStopped = true;
            soldierData.Agent.stoppingDistance = 0f;
        }

#endregion


#region PRIVATE_METHODS

#endregion
    }
}