using AI_Controllers.DataHolder.Core;
using scriptable_states.Runtime;
using UnityEngine;
namespace AI_Controllers.AI_System.Actions
{
    [CreateAssetMenu(menuName = "Scriptable State Machine/Actions/MoveToCollectibleAction",
        fileName = "new MoveToCollectibleAction")]
    public class MoveToCollectibleAction : ScriptableAction
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
            var collectiblePosition = dataHolder.ClosestCollectible.transform.position;
            dataHolder.Agent.SetDestination(collectiblePosition);
            var distance = Vector3.Distance(dataHolder.AITransform.position,
                collectiblePosition);
            dataHolder.Agent.stoppingDistance = 0f;
            if (distance > .3f)
                return;
            dataHolder.ClosestCollectible.OnCollide(dataHolder.gameObject);
        }

#endregion


#region PRIVATE_METHODS

#endregion
    }
}