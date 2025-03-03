using UnityEngine;

namespace AI_StateSystem
{
    public abstract class AIBrain
    {
        public abstract class AIReferenceData
        {
            public readonly StateManager AI;
            public readonly Transform Transform;
            public readonly Rigidbody Rb;

            protected AIReferenceData(StateManager ai, Transform transform, Rigidbody rb)
            {
                AI = ai;
                Transform = transform;
                Rb = rb;
            }
        }
       
    }
}