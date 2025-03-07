
namespace scriptable_states.Runtime
{
    [System.Serializable]
    public struct StateTransition
    {
        public ScriptableState originState;
        public ScriptableCondition condition;
        public ScriptableState trueState;
        public ScriptableState falseState;
    }
}