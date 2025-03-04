using System.Collections.Generic;
using System.Linq;
using AI_Controllers.DataHolder.Core;
using MonKey.Extensions;
using Plugins.CW.LeanPool.Required.Scripts;
using QuickTools.Scripts.Utilities;
using Sirenix.OdinInspector;
using UI;
using UnityEngine;
using UpgradeCards.Data;
namespace AI_Controllers.Spawning
{
    public class AISpawnController : MonoBehaviour
    {
//-------Public Variables-------//


//------Serialized Fields-------//
        [SerializeField, BoxGroup("Design")] private Transform SpawnPoint;
        [SerializeField, BoxGroup("Design")] private bool IsSpawnIntervalDependsOnUpgradable;
        [SerializeField, BoxGroup("Design"), HideIf(nameof(IsSpawnIntervalDependsOnUpgradable))]
        private float SpawnInterval;
        [SerializeField, BoxGroup("Design"), Sirenix.OdinInspector.ShowIf(nameof(IsSpawnIntervalDependsOnUpgradable))]
        private CastleUpgradeCardSo CastleUpgradeCard;
        [SerializeField, BoxGroup("Design")] private bool SpawnFromUpgradableList;
        [SerializeField, BoxGroup("Design"), Sirenix.OdinInspector.ShowIf(nameof(SpawnFromUpgradableList))]
        private List<SoldierUpgradeCardSo> SoliderUpgradeCards;
        [SerializeField, BoxGroup("Design"), HideIf(nameof(SpawnFromUpgradableList))]
        private List<AIDataHolderCore> TargetSoldiers;
        [SerializeField, BoxGroup("Design")] private bool IsAllySpawner;
        [SerializeField, BoxGroup("References"), ShowIf(nameof(IsAllySpawner))]
        private ScriptableListAIDataHolderCore SpawnedAllySoldiers;
        [SerializeField, BoxGroup("References")] private ProgressBarController ProgressBar;

//------Private Variables-------//
        private AIWaitingPoints _waitingPoints;
        private bool _isActivate;
        private float _elapsedTime;

#region UNITY_METHODS

        private void Start()
        {
            TryGetComponent(out _waitingPoints);
        }

        private void Update()
        {
            if (!CanSpawn())
                return;
            SpawnSoldier();
        }

#endregion


#region PUBLIC_METHODS

        public void SetActivity(bool isActive)
        {
            _isActivate = isActive;
            if (isActive)
                _elapsedTime = TargetInterval();
        }

#endregion


#region PRIVATE_METHODS

        private bool CanSpawn()
        {
            if (!_isActivate)
                return false;
            if (!_waitingPoints.CanSpawn())
                return false;
            _elapsedTime += Time.deltaTime;
            var targetInterval = TargetInterval();
            ProgressBar.Progress = Mathf.InverseLerp(0f, targetInterval, _elapsedTime);
            if (_elapsedTime < targetInterval)
                return false;
            _elapsedTime = 0f;
            return true;
        }

        private void SpawnSoldier()
        {
            var targetSoldier = SpawnFromUpgradableList
                ? SoliderUpgradeCards.Where((s => s.CurrentCardLevel >= 1)).ToList().ConvertAll(c => c.SoldierPrefab)
                    .GetRandom()
                : TargetSoldiers.GetRandom();
            if (targetSoldier == null)
            {
                EditorDebug.Log("Soldier Not Found");
                return;
            }
            var spawned = LeanPool.Spawn(targetSoldier, SpawnPoint.position, SpawnPoint.rotation);
            spawned.TargetPosition = _waitingPoints.GetPoint();
            if (IsAllySpawner)
                SpawnedAllySoldiers.Add(spawned);
        }

        private float TargetInterval()
        {
            var targetInterval = IsSpawnIntervalDependsOnUpgradable
                ? CastleUpgradeCard.CurrentSpawnInterval
                : SpawnInterval;
            return targetInterval;
        }

#endregion
    }
}