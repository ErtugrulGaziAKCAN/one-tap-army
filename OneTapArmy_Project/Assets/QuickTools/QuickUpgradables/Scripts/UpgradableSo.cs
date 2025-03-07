using System;
using Obvious.Soap;
using Plugins.SaveGameFree.Scripts;
using Sirenix.OdinInspector;
using UnityEngine;
namespace QuickTools.QuickUpgradables.Scripts
{
    [CreateAssetMenu(fileName = "New Upgrade Type", menuName = "Upgradables/Upgrade Type", order = 0)]
    public class UpgradableSo : ScriptableObject
    {
        //-------Public Variables-------//
        public Action OnLevelChanged;
        [TitleGroup("Fundamentals")] public bool EditSaveKey;

        [TitleGroup("Fundamentals"), Indent, Required("Enter a unique key for saving", InfoMessageType.Error), EnableIf(nameof(EditSaveKey))]
        public string UniqueSaveKey;

        [TitleGroup("Fundamentals"), Required("Enter a name for this upgrade", InfoMessageType.Error)]
        public string Name;

        [TitleGroup("Fundamentals")]
        public Sprite Icon;

        
        public int Level
        {
            get => _level;
            private set
            {
                _level = value;
                OnLevelChanged?.Invoke();
            }
        }

        [TitleGroup("Config"), MinValue(1)] 
        public int MaximumLevel = 9999;

        [TitleGroup("Debug"), ReadOnly, ShowInInspector]
        public float CurrentValue => ValueDetails.GetValueOnLevel(Level);

        [TitleGroup("Debug"), ReadOnly, ShowInInspector]
        public int UpgradeCost => Mathf.RoundToInt(CostDetails.GetValueOnLevel(Level));

        [TitleGroup("Debug"), ReadOnly, ShowInInspector]
        public bool IsMaxedOut;
        
//------Serialized Fields-------//
        [TitleGroup("Fundamentals"), SerializeField] private ScriptableEventNoParam UpgradeEvent;
        [TitleGroup("Fundamentals"), SerializeField] private IntReference CurrentGameLevel;
        [TitleGroup("Config"), SerializeField] private IncrementalValue CostDetails;
        [TitleGroup("Config"), SerializeField] private IncrementalValue ValueDetails;
        
        [TitleGroup("Debug"), ShowInInspector, MinValue(1)]
        private int _level = 1;

        //------Private Variables-------//

        
        public void Upgrade()
        {
            if (IsMaxedOut)
                return;

            _level++;
            UpgradeEvent.Raise();
            CheckIfMaxedOut();
            Save();
            OnLevelChanged?.Invoke();
        }

        public void Load()
        {
            _level = SaveGame.Load<int>(UniqueSaveKey + "_Level", 1);
            CheckIfMaxedOut();
        }

        public void Save()
        {
            SaveGame.Save<int>(UniqueSaveKey + "_Level", Level);
        }

        public void ClearData()
        {
            SaveGame.Save<int>(UniqueSaveKey + "_Level", 1);
        }
        
        private void CheckIfMaxedOut()
        {
            if (_level >= MaximumLevel)
            {
                _level = MaximumLevel;
                IsMaxedOut = true;
                return;
            }

            IsMaxedOut = false;
        }
    }
}