using System;
using Sirenix.OdinInspector;
using UnityEngine;
namespace QuickTools.Scripts.InputHelpers
{
    [AddComponentMenu("QuickTools/Input/Swerve Input")]
    public class SwerveInput : MonoBehaviour
    {
//-------Public Variables-------//
        public Action<Vector2> OnSwerve;

//------Serialized Fields-------//

//------Private Variables-------//
        private const float SPEED_CORRECTION = .5f;
        private bool _isActive;
        private Vector2 _previousMousePos;
        private Vector2 _mousePosDelta;


#region UNITY_METHODS

        private void Update()
        {
            if (!_isActive)
                return;
            var mousePos = Input.mousePosition;
            if (Input.GetMouseButtonDown(0))
            {
                _previousMousePos = mousePos;
            }
            else if (Input.GetMouseButton(0))
            {
                _mousePosDelta = (Vector2) Input.mousePosition - _previousMousePos;
                _mousePosDelta *= (1242f / Screen.width) * SPEED_CORRECTION;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                _mousePosDelta = Vector2.zero;
            }

            _previousMousePos = mousePos;
            OnSwerve?.Invoke(_mousePosDelta);
        }

#endregion


#region PUBLIC_METHODS

        [Button]
        public void StartDetection()
        {
            _previousMousePos = Input.mousePosition;
            _isActive = true;
        }

        [Button]
        public void StopDetection()
        {
            _mousePosDelta = Vector2.zero;
            _isActive = false;
        }

#endregion


#region PRIVATE_METHODS

#endregion
    }
}