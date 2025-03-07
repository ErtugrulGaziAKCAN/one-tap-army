using AI_Controllers.DataHolder.Core;
using scriptable_states.Runtime;
using UnityEngine;
namespace AI_Controllers.AI_System.Conditions
{
    [CreateAssetMenu(menuName = "Scriptable State Machine/Conditions/IsDead",
        fileName = "new IsDead")]
    public class IsDead : ScriptableCondition
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
            return dataHolder.AIHealth.IsDead;
        }

#endregion


#region PRIVATE_METHODS

#endregion
    }
}