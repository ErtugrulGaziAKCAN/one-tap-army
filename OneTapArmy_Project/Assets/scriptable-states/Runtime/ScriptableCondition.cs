using UnityEngine;
namespace scriptable_states.Runtime
{
    public abstract class ScriptableCondition : ScriptableObject
    {
        public abstract bool Verify(StateComponent statesComponent);
    }
}