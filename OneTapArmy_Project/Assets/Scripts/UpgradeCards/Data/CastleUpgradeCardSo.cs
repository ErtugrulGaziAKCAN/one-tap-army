using QuickTools.QuickUpgradables.Scripts;
using Sirenix.OdinInspector;
using UnityEngine;
using UpgradeCards.Data.Base;
namespace UpgradeCards.Data
{
    [CreateAssetMenu(fileName = "CastleUpgradeCardSo", menuName = "Upgrade Cards/Create CastleUpgradeCardSo")]
    public class CastleUpgradeCardSo : UpgradeCardSo
    {
        [BoxGroup("Design")] public IncrementalValue Health;
        [BoxGroup("Design")] public IncrementalValue SpawnInterval;

        public float CurrentSpawnInterval => SpawnInterval.GetValueOnLevel(CurrentCardLevel);
    }
}