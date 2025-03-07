using AI_Controllers.DataHolder;
using AI_Controllers.DataHolder.Core;
using scriptable_states.Runtime;
using UnityEngine;
namespace AI_Controllers.AI_System.Conditions.Attack
{
    [CreateAssetMenu(menuName = "Scriptable State Machine/Conditions/Attack/IsNearTheRival",
        fileName = "new IsNearTheRival")]
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
            if (Time.frameCount % 10 != 0)
                return dataHolder.IsAttacking;
            if (dataHolder.IsAttacking)
                return true;
            if (dataHolder.ClosestRivalHealth == null)
                return false;
            if (dataHolder.ClosestRivalHealth.IsDead)
                return false;
            var distance = Vector3.Distance(dataHolder.AITransform.position,
                dataHolder.ClosestRivalHealth.transform.position);
            var isNear = distance <= dataHolder.AttackDistance;
            return isNear;
        }

#endregion


#region PRIVATE_METHODS

#endregion
    }
}