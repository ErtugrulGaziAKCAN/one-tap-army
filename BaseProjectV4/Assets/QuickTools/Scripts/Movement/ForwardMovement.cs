using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
namespace QuickTools.Scripts.Movement
{
    [AddComponentMenu("QuickTools/Movement/Forward Movement")]
    public class ForwardMovement : MonoBehaviour
    {
//-------Public Variables-------//


//------Serialized Fields-------//
        [SerializeField] private float MoveSpeed = 10f;

//------Private Variables-------//
        private bool _isMoving;
        private Tween _speedChange;
        private float _currentSpeed;
        private Transform _transform;
        private float _currentMultiplier = 1f;


#region UNITY_METHODS

        private void Awake()
        {
            _transform = transform;
        }

        private void Update()
        {
            if (!_isMoving)
                return;
            _transform.position += _transform.forward * (_currentSpeed * Time.deltaTime);
        }

#endregion


#region PUBLIC_METHODS

        [Button]
        public void SetSpeedMultiplier(float multiplier = 1f)
        {
            _currentMultiplier = multiplier;
            var from = _currentSpeed;
            _speedChange.Kill();
            _speedChange = DOVirtual.Float(from, MoveSpeed * _currentMultiplier, .5f, SetCurrentSpeed);
        }

        [Button]
        public void StartMovement()
        {
            _isMoving = true;
            var from = _currentSpeed;
            _speedChange.Kill();
            _speedChange = DOVirtual.Float(from, MoveSpeed * _currentMultiplier, .5f, SetCurrentSpeed);
        }

        [Button]
        public void StopMovement()
        {
            var from = _currentSpeed;
            _speedChange.Kill();
            _speedChange = DOVirtual.Float(from, 0f, .5f, SetCurrentSpeed)
                .OnComplete(() => _isMoving = false);
        }

#endregion


#region PRIVATE_METHODS

        private void SetCurrentSpeed(float speed)
        {
            _currentSpeed = speed;
        }

#endregion
    }
}