using scriptable_states.Runtime;
namespace AI_Controllers.AI_System.Actions
{
    public class SoldierIdleAction : ScriptableAction
    {
//-------Public Variables-------//


//------Serialized Fields-------//

//------Private Variables-------//

#region UNITY_METHODS

#endregion


#region PUBLIC_METHODS

        public override void Act(StateComponent statesComponent)
        {
            statesComponent.TryGetComponent(out AIDataHolder.AIDataHolder dataHolder);
            dataHolder.AnimationController.SetAnimation("Idle");
        }

#endregion


#region PRIVATE_METHODS

#endregion
    }
}