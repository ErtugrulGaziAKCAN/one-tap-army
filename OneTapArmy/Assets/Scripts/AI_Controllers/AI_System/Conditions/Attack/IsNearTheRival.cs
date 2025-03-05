using AI_Controllers.DataHolder.Core;
using QuickTools.Scripts.Utilities;
using scriptable_states.Runtime;
using UnityEngine;
namespace AI_Controllers.AI_System.Conditions.Attack
{
    [CreateAssetMenu(menuName = "Scriptable State Machine/Conditions/Attack/IsNearTheRival", fileName = "new IsNearTheRival")]
    public class IsNearTheRival : ScriptableCondition
    {
//-------Public Variables-------//


//------Serialized Fields-------//


//------Private Variables-------//

#region UNITY_METHODS

#endregion


#region PUBLIC_METHODS

        public override bool Verify(StateComponent statesComponent)
        {
            statesComponent.TryGetComponent(out AIDataHolderCore dataHolder);
            var distance = Vector3.Distance(dataHolder.AITransform.position,
                dataHolder.ClosestRivalHealth.transform.position);
            var isNear = distance <= dataHolder.AttackDistance;
            if(isNear)
                EditorDebug.Log("Found Attackable");
            return isNear;
        }

#endregion


#region PRIVATE_METHODS

#endregion
    }
}