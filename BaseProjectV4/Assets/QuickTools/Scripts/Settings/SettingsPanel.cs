using UnityEngine;
namespace QuickTools.Scripts.Settings
{
    public class SettingsPanel : MonoBehaviour
    {
//-------Public Variables-------//


//------Serialized Fields-------//


//------Private Variables-------//
        private SettingsToggle[] _settingsToggles;
        private Animator _animator;
        private static readonly int Open = Animator.StringToHash("Open");


#region UNITY_METHODS

        private void Start()
        {
            _settingsToggles = GetComponentsInChildren<SettingsToggle>();
            _animator = GetComponent<Animator>();
            Load();
        }

        private void OnDestroy()
        {
            Save();
        }

#endregion


#region PUBLIC_METHODS

        public void TogglePanel()
        {
            var isOpen = _animator.GetBool(Open);
            _animator.SetBool(Open, !isOpen);
        }

#endregion


#region PRIVATE_METHODS

        private void Load()
        {
            foreach (var toggle in _settingsToggles)
            {
                toggle.Load();
            }
        }

        private void Save()
        {
            foreach (var toggle in _settingsToggles)
            {
                toggle.Save();
            }
        }

#endregion
    }
}