using AI_Controllers.DataHolder;
using QuickTools.Scripts.Extensions;
using scriptable_states.Runtime;
using UnityEngine;
namespace AI_Controllers.AI_System.Actions
{
    [CreateAssetMenu(menuName = "Scriptable State Machine/Actions/MoveToTargetPosition",
        fileName = "new MoveToTargetPosition")]
    public class MoveToTargetPosition : ScriptableAction
    {
//-------Public Variables-------//


//------Serialized Fields-------//


//------Private Variables-------//

#region UNITY_METHODS

#endregion


#region PUBLIC_METHODS

        public override void Act(StateComponent statesComponent)
        {
            if (Time.frameCount % 10 != 0)
                return;
            statesComponent.TryGetComponent(out SoldierAIDataHolderCore dataHolder);
            var targetPos = dataHolder.TargetPosition.WithY(0f);
            dataHolder.Agent.isStopped = false;
            dataHolder.Agent.SetDestination(targetPos);

        }

#endregion


#region PRIVATE_METHODS

#endregion
    }
}