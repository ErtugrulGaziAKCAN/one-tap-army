using System.Collections.Generic;
using System.Linq;
using MonKey.Extensions;
using Plugins.CW.LeanPool.Required.Scripts;
using QuickTools.Scripts.Utilities;
using scriptable_states.Runtime;
using Sirenix.OdinInspector;
using UI;
using UnityEngine;
using UpgradeCards.Data;
namespace Soldier.Spawning
{
    public class SoldierSpawnController : MonoBehaviour
    {
//-------Public Variables-------//


//------Serialized Fields-------//
        [SerializeField, BoxGroup("Design")] private Transform SpawnPoint;
        [SerializeField, BoxGroup("Design")] private bool IsSpawnIntervalDependsOnUpgradable;
        [SerializeField, BoxGroup("Design"), HideIf(nameof(IsSpawnIntervalDependsOnUpgradable))]
        private float SpawnInterval;
        [SerializeField, BoxGroup("Design"), ShowIf(nameof(IsSpawnIntervalDependsOnUpgradable))]
        private CastleUpgradeCardSo CastleUpgradeCard;
        [SerializeField, BoxGroup("Design")] private bool SpawnFromUpgradableList;
        [SerializeField, BoxGroup("Design"),ShowIf(nameof(SpawnFromUpgradableList))] private List<SoldierUpgradeCardSo> SoliderUpgradeCards;
        [SerializeField, BoxGroup("Design"), HideIf(nameof(SpawnFromUpgradableList))]
        private List<StateComponent> TargetSoldiers;
        [SerializeField, BoxGroup("References")] private ProgressBarController ProgressBar;
        
//------Private Variables-------//
        private bool _isActivate;
        private float _elapsedTime;

#region UNITY_METHODS

        private void Update()
        {
            if(!CanSpawn())
                return;
            SpawnSoldier();
        }

#endregion


#region PUBLIC_METHODS

        public void SetActivity(bool isActive) => _isActivate = isActive;

#endregion


#region PRIVATE_METHODS

        private bool CanSpawn()
        {
            if (!_isActivate)
                return false;
            _elapsedTime += Time.deltaTime;
            var targetInterval = IsSpawnIntervalDependsOnUpgradable
                ? CastleUpgradeCard.CurrentSpawnInterval
                : SpawnInterval;
            ProgressBar.Progress = Mathf.InverseLerp(0f, targetInterval, _elapsedTime);
            if (_elapsedTime < targetInterval)
                return false;
            _elapsedTime = 0f;
            return true;
        }

        private void SpawnSoldier()
        {
            var targetSoldier = SpawnFromUpgradableList
                ? SoliderUpgradeCards.Where((s => s.CurrentCardLevel >= 1)).ToList().ConvertAll(c=>c.SoldierPrefab).GetRandom()
                : TargetSoldiers.GetRandom();
            if (targetSoldier == null)
            {
                EditorDebug.Log("Soldier Not Found");
                return;
            }
            LeanPool.Spawn(targetSoldier, SpawnPoint);
        }
#endregion
    }
}