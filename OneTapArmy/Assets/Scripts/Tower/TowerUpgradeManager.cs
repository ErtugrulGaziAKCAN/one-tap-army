using System.Collections.Generic;
using Nova;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
namespace Tower
{
    public class TowerUpgradeManager : MonoBehaviour
    {
//-------Public Variables-------//


//------Serialized Fields-------//
        [SerializeField, BoxGroup("References")] private List<MeshRenderer> TowerMeshes;
        [SerializeField, BoxGroup("References")] private TextBlock LevelText;
        [SerializeField, BoxGroup("Events")] private UnityEvent OnUpgraded;

//------Private Variables-------//
        private int _currentLevel;

#region UNITY_METHODS

        private void Start()
        {
            UpgradeTower(true);
        }

#endregion


#region PUBLIC_METHODS

        [Button]
        public void UpgradeTower(bool initializing)
        {
            _currentLevel++;
            LevelText.Text = _currentLevel.ToString();
            if (initializing)
                return;
            OnUpgraded?.Invoke();
        }

#endregion


#region PRIVATE_METHODS

#endregion
    }
}