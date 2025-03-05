using System;
using UnityEngine;
namespace AnimationControllers
{
    public class AIAnimationEventHandler : MonoBehaviour
    {
//-------Public Variables-------//
        public Action OnAttacked;
        public Action OnEndOfAnimation;
//------Serialized Fields-------//


//------Private Variables-------//

#region UNITY_METHODS

#endregion


#region PUBLIC_METHODS

        public void Attacked() => OnAttacked?.Invoke();

        public void End() => OnEndOfAnimation?.Invoke();

#endregion


#region PRIVATE_METHODS

#endregion
    }
}