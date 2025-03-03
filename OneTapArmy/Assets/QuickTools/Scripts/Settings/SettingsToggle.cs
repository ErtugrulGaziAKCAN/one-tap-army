using Obvious.Soap;
using Plugins.SaveGameFree.Scripts;
using UnityEngine;
using UnityEngine.UI;
namespace QuickTools.Scripts.Settings
{
    public class SettingsToggle : MonoBehaviour
    {
//-------Public Variables-------//

//------Serialized Fields-------//
        [SerializeField] private string SaveKey;
        [SerializeField] private GameObject OnIcon;
        [SerializeField] private GameObject OffIcon;
        [SerializeField] private BoolReference IsOn;

//------Private Variables-------//
        private Button _button;


#region UNITY_METHODS

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(Toggle);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(Toggle);
        }

#endregion


#region PUBLIC_METHODS

        public void Load()
        {
            IsOn.Value = !SaveGame.Load<bool>(SaveKey, true);
            Toggle();
        }

        public void Save()
        {
            SaveGame.Save<bool>(SaveKey, IsOn.Value);
        }

#endregion


#region PRIVATE_METHODS

        private void Toggle()
        {
            IsOn.Value = !IsOn.Value;
            OnIcon.SetActive(IsOn.Value);
            OffIcon.SetActive(!IsOn.Value);
            Save();
        }

#endregion
    }
}