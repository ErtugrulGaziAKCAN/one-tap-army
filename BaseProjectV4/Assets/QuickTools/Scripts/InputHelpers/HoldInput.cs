using System;
using Sirenix.OdinInspector;
using UnityEngine;
namespace QuickTools.Scripts.InputHelpers
{
    [AddComponentMenu("QuickTools/Input/Hold Input")]
    public class HoldInput : MonoBehaviour
    {
//-------Public Variables-------//
        public Action OnHoldStart;
        public Action OnHold;
        public Action OnHoldEnd;

//------Serialized Fields-------//


//------Private Variables-------//
        private bool _isActive;


#region UNITY_METHODS

        private void Update()
        {
            if (!_isActive)
                return;
            if (Input.GetMouseButtonDown(0))
            {
                OnHoldStart?.Invoke();
            }
            else if (Input.GetMouseButton(0))
            {
                OnHold?.Invoke();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                OnHoldEnd?.Invoke();
            }
        }

#endregion


#region PUBLIC_METHODS
        
        [Button]
        public void SetDetectionActive(bool isActive)
        {
            _isActive = isActive;
            if(!_isActive)
                OnHoldEnd?.Invoke();
        }

#endregion


#region PRIVATE_METHODS

#endregion
    }
}