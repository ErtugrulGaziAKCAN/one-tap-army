using QuickTools.Scripts.InputHelpers;
using Sirenix.OdinInspector;
using UnityEngine;
namespace QuickTools.Scripts.Movement
{
    [AddComponentMenu("QuickTools/Movement/Swerve Movement")]
    public class SwerveMovement : MonoBehaviour
    {
//-------Public Variables-------//


//------Serialized Fields-------//
        [SerializeField, Required] private SwerveInput SwerveController;
        [SerializeField] private float SwerveSpeed = 1f;
        [SerializeField] private bool Boundaries = true;
        [SerializeField] private float MaxXPosition = 4f;

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

        [Button("Fix Swerve Controller", ButtonSizes.Large), GUIColor(.2f, .9f, .2f), ShowIf("@SwerveController == null"), PropertyOrder(-1)]
        private void AddSwerveInput()
        {
            TryGetComponent(out SwerveInput swerve);
            SwerveController = swerve ? swerve :  gameObject.AddComponent<SwerveInput>();
        }

        private void SwerveResponse(Vector2 swerveDelta)
        {
            var delta = swerveDelta.x * SwerveSpeed * Time.deltaTime;
            var finalPos = _transform.localPosition.x + delta;
            if (Boundaries)
            {
                if (finalPos < -MaxXPosition)
                    delta = -MaxXPosition - transform.localPosition.x;
                else if(finalPos > MaxXPosition)
                    delta = MaxXPosition - transform.localPosition.x;
            }
            _transform.localPosition += transform.right * delta;
        }

#endregion
    }
}