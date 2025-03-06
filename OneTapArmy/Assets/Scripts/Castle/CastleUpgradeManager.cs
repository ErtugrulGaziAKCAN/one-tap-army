using System.Collections;
using System.Collections.Generic;
using AI_Controllers.Spawning;
using Nova;
using QuickTools.Scripts.HealthSystem;
using QuickTools.Scripts.Utilities;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UpgradeCards.Data;
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
        [SerializeField, BoxGroup("References")] private AISpawnController Spawner;
        [SerializeField, BoxGroup("References")] private CastleUpgradeCardSo CastleUpgradeCard;
        [SerializeField, BoxGroup("References")] private HealthCore HealthCore;
        [SerializeField, BoxGroup("References")] private CastleDataHolder DataHolder;
        [SerializeField, BoxGroup("Events")] private UnityEvent OnUpgradedEvent;

//------Private Variables-------//
        private int _currentLevel = 0;
        private bool _isInitialized;

#region UNITY_METHODS

        private void OnEnable()
        {
            if (Spawner.IsAllySpawner)
                CastleUpgradeCard.OnUpgraded += UpdateTheCastle;
        }

        private void OnDisable()
        {
            if (Spawner.IsAllySpawner)
                CastleUpgradeCard.OnUpgraded -= UpdateTheCastle;
        }

        private IEnumerator Start()
        {
            UpdateTheCastle();
            yield return new WaitForEndOfFrame();
            _isInitialized = true;
        }

#endregion


#region PUBLIC_METHODS

        [Button]
        public void SetVisuals()
        {
            LevelText.Text = _currentLevel.ToString();
            ActivateTargetBuild();
            if (!_isInitialized)
                return;
            OnUpgradedEvent?.Invoke();
        }

        public void UpdateTheCastle()
        {
            EditorDebug.Log("CastleUpgraded", gameObject);
            _currentLevel++;
            DataHolder.CurrentCastleLevel = _currentLevel;
            SetVisuals();
            HealthCore.SetMaxHealth(CastleUpgradeCard.Health.GetValueOnLevel(_currentLevel));
            HealthCore.ResetHealth();
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
            Towers.ForEach((t) => t.SetActive(false));
            Towers[_currentLevel - 1].SetActive(true);
        }

#endregion
    }
}