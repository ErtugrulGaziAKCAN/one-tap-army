using System.Collections.Generic;
using Nova;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
namespace Castle
{
    public class CastleUpgradeManager : MonoBehaviour
    {
//-------Public Variables-------//


//------Serialized Fields-------//
        [SerializeField, BoxGroup("Design"), OnValueChanged(nameof(SetTowerMaterials))]
        private Material TowerMaterial;
        [SerializeField, BoxGroup("References")] private List<MeshRenderer> TowerMeshes;
        [SerializeField, BoxGroup("References")] private List<GameObject> Towers;
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
            ActivateTargetBuild();
        }

#endregion


#region PRIVATE_METHODS

        [ExecuteAlways]
        private void SetTowerMaterials()
        {
            if (TowerMaterial == null)
                return;
            TowerMeshes.ForEach((t) => t.sharedMaterial = TowerMaterial);
        }

        private void ActivateTargetBuild()
        {
            Towers.ForEach((t)=>t.SetActive(false));
            Towers[_currentLevel-1].SetActive(true);
        }
#endregion
    }
}