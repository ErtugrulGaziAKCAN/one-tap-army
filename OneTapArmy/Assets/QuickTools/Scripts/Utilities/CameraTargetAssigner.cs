using Cinemachine;
using Obvious.Soap;
using UnityEngine;
namespace QuickTools.Scripts.Utilities
{
    public class CameraTargetAssigner : MonoBehaviour
    {
//-------Public Variables-------//


//------Serialized Fields-------//
        [SerializeField] private TransformReference BodyTarget;
        [SerializeField] private TransformReference AimTarget;


//------Private Variables-------//
        private CinemachineVirtualCamera _virtualCamera;

#region UNITY_METHODS

        private void Awake()
        {
            _virtualCamera = GetComponent<CinemachineVirtualCamera>();
        }

        private void Start()
        {
            if (BodyTarget is not null)
                SetBodyTarget(BodyTarget);
            if (AimTarget is not null)
                SetAimTarget(AimTarget);
        }

#endregion


#region PUBLIC_METHODS

        public void SetBodyTarget(Transform target)
        {
            _virtualCamera.Follow = target;
        }

        public void SetAimTarget(Transform target)
        {
            _virtualCamera.LookAt = target;
        }

        public void SetBodyTarget(TransformReference target)
        {
            _virtualCamera.Follow = target.Value;
        }

        public void SetAimTarget(TransformReference target)
        {
            _virtualCamera.LookAt = target.Value;
        }

#endregion


#region PRIVATE_METHODS

#endregion
    }
}