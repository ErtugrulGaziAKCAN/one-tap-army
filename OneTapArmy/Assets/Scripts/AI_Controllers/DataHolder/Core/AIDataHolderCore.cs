using AnimationControllers;
using scriptable_states.Runtime;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;
namespace AI_Controllers.DataHolder.Core
{
    public abstract class AIDataHolderCore : MonoBehaviour
    {

//-------Public Variables-------//
        [BoxGroup("References")] public Transform AITransform;
        [BoxGroup("References")] public NavMeshAgent Agent;
        [BoxGroup("References")] public AIHealthController AIHealth;
        [BoxGroup("References")] public StateComponent StateComponentAccess;
        [BoxGroup("AnimationData")] public FastAnimationController AnimationController;
        [BoxGroup("Config"), ReadOnly] public Vector3 TargetPosition;
        
//------Serialized Fields-------//


//------Private Variables-------//

#region UNITY_METHODS

#endregion


#region PUBLIC_METHODS

#endregion


#region PRIVATE_METHODS

#endregion

    }
}