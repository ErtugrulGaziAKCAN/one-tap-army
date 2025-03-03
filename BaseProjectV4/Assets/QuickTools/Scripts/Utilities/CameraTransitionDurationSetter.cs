using Cinemachine;
using Sirenix.OdinInspector;
using UnityEngine;
namespace QuickTools.Scripts.Utilities
{
    public class CameraTransitionDurationSetter : MonoBehaviour
    {
//-------Public Variables-------//


//------Serialized Fields-------//

//------Private Variables-------//
        private CinemachineBrain _brain;

#region UNITY_METHODS

        private void Awake()
        {
            TryGetComponent(out _brain);
        }

#endregion


#region PUBLIC_METHODS

        [Button]
        public void SetTransitionDuration(float targetDuration) => _brain.m_DefaultBlend.m_Time = targetDuration;

#endregion


#region PRIVATE_METHODS

#endregion
    }
}