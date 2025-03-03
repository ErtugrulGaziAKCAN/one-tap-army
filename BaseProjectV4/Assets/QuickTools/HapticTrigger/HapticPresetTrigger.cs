using Lofelt.NiceVibrations;
using UnityEngine;
namespace QuickTools.HapticTrigger
{
    public class HapticPresetTrigger : MonoBehaviour
    {
//-------Public Variables-------//


//------Serialized Fields-------//
        [SerializeField] private HapticPatterns.PresetType Preset;

//------Private Variables-------//



#region UNITY_METHODS

#endregion


#region PUBLIC_METHODS

        public void Trigger()
        {
            HapticPatterns.PlayPreset(Preset);
        }

#endregion


#region PRIVATE_METHODS

#endregion
    }
}