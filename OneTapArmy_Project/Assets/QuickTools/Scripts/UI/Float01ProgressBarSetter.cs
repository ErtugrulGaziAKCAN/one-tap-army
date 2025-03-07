using Obvious.Soap;
using UnityEngine;
namespace UI
{
    public class Float01ProgressBarSetter : MonoBehaviour
    {
//-------Public Variables-------//


//------Serialized Fields-------//
        [SerializeField] private FloatReference TargetValue;
        [SerializeField] private ProgressBarController TargetBar;

//------Private Variables-------//

#region UNITY_METHODS

        private void OnEnable()
        {
            TargetValue.Variable.OnValueChanged += OnValueChanged;
        }

        private void OnDisable()
        {
            TargetValue.Variable.OnValueChanged -= OnValueChanged;
        }

#endregion


#region PUBLIC_METHODS

#endregion


#region PRIVATE_METHODS

        private void OnValueChanged(float value)
        {
            TargetBar.Progress = value;
        }

#endregion
    }
}