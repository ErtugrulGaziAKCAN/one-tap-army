using Nova;
using UnityEngine;
using UnityEngine.Rendering.Universal;
namespace QuickTools.Scripts.UI
{
    public class UIMouseDistanceScaleFX : MonoBehaviour
    {
//-------Public Variables-------//


//------Serialized Fields-------//
        [SerializeField] private Vector3 DefaultScale = Vector3.one;
        [SerializeField] private Vector3 MaxScale = Vector3.one * 1.5f;
        [SerializeField] private float MinDistance;
        [SerializeField] private float MaxDistance = 2f;
        [SerializeField] private LayerMask UILayer;

//------Private Variables-------//
        private Transform _transform;
        private Camera _camera;
        private bool _isSelected;

#region UNITY_METHODS

        private void Awake()
        {
            _transform = transform;
        }

        private void Start()
        {
            _camera = Camera.main.GetUniversalAdditionalCameraData().cameraStack[0];
        }

        private void Update()
        {
            SetScale();
        }

#endregion


#region PUBLIC_METHODS

#endregion


#region PRIVATE_METHODS

        private void SetScale()
        {
            var foundUI = Interaction.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out var blockHit,
                Mathf.Infinity,
                UILayer);
            if (!foundUI)
                return;
            var distance = Vector3.Distance(blockHit.Position, _transform.position);
            var inversedLerp = Mathf.InverseLerp(MinDistance, MaxDistance, distance);
            var targetScale = Vector3.Lerp(MaxScale, DefaultScale, inversedLerp);
            _transform.localScale = targetScale;
        }

#endregion
    }
}