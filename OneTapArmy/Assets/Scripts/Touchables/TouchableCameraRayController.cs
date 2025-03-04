using InputControllers;
using QuickTools.Scripts.Utilities.RaycastVisualization._3D;
using UnityEngine;
namespace Touchables
{
    public class TouchableCameraRayController : MonoBehaviour
    {
//-------Public Variables-------//


//------Serialized Fields-------//


//------Private Variables-------//
        private InputActionRaiser _inputActionRaiser;
        private Camera _camera;

#region UNITY_METHODS

        private void Start()
        {
            _camera = Camera.main;
            _inputActionRaiser = InputActionRaiser.Instance;
            _inputActionRaiser.CheckHit += OnClicked;
        }

        private void OnDisable()
        {
            _inputActionRaiser.CheckHit -= OnClicked;
        }

#endregion


#region PUBLIC_METHODS

#endregion


#region PRIVATE_METHODS

        private void OnClicked()
        {
            if (!VisualPhysics.Raycast(_camera.ScreenPointToRay(_inputActionRaiser.InputPosition), out var hit))
                return;
            hit.collider.TryGetComponent(out ITouchable touchable);
            touchable?.OnTouched(hit.point);
        }

#endregion
    }
}