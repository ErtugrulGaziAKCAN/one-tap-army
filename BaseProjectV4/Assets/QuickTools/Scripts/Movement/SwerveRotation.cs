using QuickTools.Scripts.InputHelpers;
using Sirenix.OdinInspector;
using UnityEngine;
namespace QuickTools.Scripts.Movement
{
    [AddComponentMenu("QuickTools/Movement/Swerve Rotation")]
    [TypeInfoBox("Use rotation on model, not parent object so movement will not be affected by rotation")]
    public class SwerveRotation : MonoBehaviour
    {
//-------Public Variables-------//


//------Serialized Fields-------//
        [SerializeField, Required] private SwerveInput SwerveController;
        [SerializeField] private float RotationFactor = 1f;
        [SerializeField] private float DampenSpeed = 20f;

//------Private Variables-------//
        private Transform _transform;


#region UNITY_METHODS

        private void Awake()
        {
            _transform = transform;
        }

        private void OnEnable()
        {
            SwerveController.OnSwerve += SwerveResponse;
        }

#endregion


#region PUBLIC_METHODS

#endregion


#region PRIVATE_METHODS

        private void SwerveResponse(Vector2 swerveDelta)
        {
            _transform.Rotate(transform.up, swerveDelta.x * RotationFactor);
            _transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, Time.deltaTime * DampenSpeed);
        }

#endregion
    }
}