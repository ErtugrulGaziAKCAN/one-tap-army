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
            dataHolder.Agent.stoppingDistance = 0f;
            dataHolder.AIAttackController.StartAttacking();
            dataHolder.IsAttacking = true;
        }

#endregion


#region PRIVATE_METHODS

#endregion
    }
}