using AI_Controllers.DataHolder;
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
            if (Time.frameCount % 10 != 0)
                return;
            statesComponent.TryGetComponent(out SoldierAIDataHolderCore dataHolder);
            var rivalPosition = dataHolder.ClosestRivalHealth.transform.position;
            dataHolder.Agent.SetDestination(rivalPosition);
            dataHolder.Agent.stoppingDistance = 0f;
        }

#endregion


#region PRIVATE_METHODS

#endregion
    }
}