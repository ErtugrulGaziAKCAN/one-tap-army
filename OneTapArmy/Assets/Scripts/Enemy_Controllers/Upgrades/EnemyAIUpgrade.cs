using System;
using System.Collections.Generic;
using System.Linq;
using AI_Controllers.DataHolder.Core;
using Castle;
using MonKey.Extensions;
using Sirenix.OdinInspector;
using UnityEngine;
using UpgradeCards.Data;
using Random = UnityEngine.Random;
namespace Enemy_Controllers.Upgrades
{
    public class EnemyAIUpgrade : MonoBehaviour
    {
//-------Public Variables-------//


//------Serialized Fields-------//
        [SerializeField] private List<EnemyAIUpgradesData> UpgradesData;
        [SerializeField] private int MaxUpgradeLevel = 5;
        [SerializeField] private CastleDataHolder DataHolder;

//------Private Variables-------//

#region UNITY_METHODS

        private void Start()
        {
            UpgradesData.ForEach((u) => u.CurrentLevel = 0);
            UpgradesData[Random.Range(0, UpgradesData.Count)].CurrentLevel = 1;
        }

#endregion


#region PUBLIC_METHODS

        public AIDataHolderCore GetSpawnableSoldierPrefab() =>
            UpgradesData.Where((u) => u.CurrentLevel > 0).ToList().GetRandom().EnemyPrefab;

        public bool CanUpgrade() => UpgradesData.Any((u) => u.CurrentLevel < MaxUpgradeLevel);

        public void UpgradeAI()
        {
            var selectUpgradeIndex =
                UpgradesData.IndexOf(UpgradesData.Where((u) => u.CurrentLevel < MaxUpgradeLevel).ToList().GetRandom());
            UpgradesData[selectUpgradeIndex].CurrentLevel++;
            DataHolder.SpawnedAIList.ForEach((s) =>
            {
                var upgradeController = s.UpgradeController;
                if (upgradeController.TargetSoldierSo != UpgradesData[selectUpgradeIndex].SampleUpgrade)
                    return;
                upgradeController.ApplyUpgrades(UpgradesData[selectUpgradeIndex].CurrentLevel);
            });
        }

        public int GetSoldierLevel(SoldierUpgradeCardSo targetUpgrade) =>
            UpgradesData.FirstOrDefault((s) => s.SampleUpgrade == targetUpgrade)!.CurrentLevel;

#endregion


#region PRIVATE_METHODS

#endregion
    }
    [Serializable]
    public class EnemyAIUpgradesData
    {
        public SoldierUpgradeCardSo SampleUpgrade;
        public AIDataHolderCore EnemyPrefab;
        [ReadOnly] public int CurrentLevel;
    }
}