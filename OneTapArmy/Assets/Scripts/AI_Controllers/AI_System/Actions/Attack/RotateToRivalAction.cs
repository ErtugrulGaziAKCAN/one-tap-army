using AI_Controllers.DataHolder;
using QuickTools.Scripts.Extensions;
using scriptable_states.Runtime;
using UnityEngine;
namespace AI_Controllers.AI_System.Actions.Attack
{
    [CreateAssetMenu(menuName = "Scriptable State Machine/Actions/Attack/RotateToRivalAction",
        fileName = "new RotateToRivalAction")]
    public class RotateToRivalAction : ScriptableAction
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
            var aiTransform = dataHolder.AITransform;
            aiTransform.rotation = Quaternion.Slerp(aiTransform.rotation, Quaternion.LookRotation(aiTransform.position
                .WithY(0f)
                .DirectionTo(dataHolder.ClosestRivalHealth.transform.position.WithY(0f))), Time.deltaTime * 30f);
        }

#endregion


#region PRIVATE_METHODS

#endregion
    }
}