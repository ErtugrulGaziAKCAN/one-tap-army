using AnimationControllers;
using Sirenix.OdinInspector;
using UnityEngine;
namespace AI_Controllers.AIDataHolder
{
    public class AIDataHolder : MonoBehaviour
    {

//-------Public Variables-------//
        [BoxGroup("References")] public Rigidbody AIBody;
        [BoxGroup("References")] public AIHealthController AIHealth;
        [BoxGroup("AnimationData")] public FastAnimationController AnimationController;
        
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