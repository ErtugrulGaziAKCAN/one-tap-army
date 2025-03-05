using Obvious.Soap;
using scriptable_states.Runtime;
using UnityEngine;
namespace AI_Controllers.AI_System.Conditions
{
    [CreateAssetMenu(menuName = "Scriptable State Machine/Conditions/IsLevelEnd",
        fileName = "new IsLevelEnd")]
    public class IsLevelEnd : ScriptableCondition
    {
//-------Public Variables-------//


//------Serialized Fields-------//
        [SerializeField] private BoolReference IsLevelEndRef;

//------Private Variables-------//

#region UNITY_METHODS

#endregion


#region PUBLIC_METHODS

        public override bool Verify(StateComponent statesComponent)
        {
            return IsLevelEndRef.Value;
        }

#endregion


#region PRIVATE_METHODS

#endregion
    }
}