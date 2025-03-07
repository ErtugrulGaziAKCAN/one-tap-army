using System;
using QuickTools.Scripts.Utilities;
using Sirenix.OdinInspector;
using UnityEngine;
namespace InputControllers
{
    public class InputActionRaiser : QuickSingleton<InputActionRaiser>
    {
//-------Public Variables-------//
        public Action OnInputRelease;
        public Action CheckHit;
        [ReadOnly] public Vector3 InputPosition;
        
//------Serialized Fields-------//


//------Private Variables-------//

#region UNITY_METHODS

        private void Update()
        {
            if (UnityEngine.Input.GetMouseButtonDown(0))
                CheckHit?.Invoke();
            InputPosition = UnityEngine.Input.mousePosition;
            if (!UnityEngine.Input.GetMouseButtonUp(0))
                return;
            OnInputRelease?.Invoke();
        }

#endregion


#region PUBLIC_METHODS

#endregion


#region PRIVATE_METHODS

#endregion
    }
}