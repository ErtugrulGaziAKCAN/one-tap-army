using AI_Controllers.DataHolder;
using AI_Controllers.DataHolder.Core;
using scriptable_states.Runtime;
using UnityEngine;
namespace AI_Controllers.AI_System.Conditions.Attack
{
    [CreateAssetMenu(menuName = "Scriptable State Machine/Conditions/Attack/IsRivalAvailableToAttack",
        fileName = "new IsRivalAvailableToAttack")]
    public class IsRivalAvailableToAttack : ScriptableCondition
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
            if (dataHolder.ClosestRivalHealth == null)
                return false;
            if (!dataHolder.ClosestRivalHealth.gameObject.activeInHierarchy)
                return false;
            if (dataHolder.ClosestRivalHealth.IsDead)
                return false;
            if (dataHolder.AIHealth.IsDead)
                return false;
            return true;
        }

#endregion


#region PRIVATE_METHODS

#endregion
    }
}