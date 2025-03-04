using UnityEngine;
namespace scriptable_states.Runtime
{
    public abstract class ScriptableAction : ScriptableObject
    {
        public abstract void Act(StateComponent statesComponent);
    }
}