using QuickTools.QuickUpgradables.Scripts;
using Sirenix.OdinInspector;
using Soldier.AI_System.Base;
using UnityEngine;
using UpgradeCards.Data.Base;
namespace UpgradeCards.Data
{
    [CreateAssetMenu(fileName = "SoldierUpgradeCardSo", menuName = "Upgrade Cards/Create SoldierUpgradeCards")]
    public class SoldierUpgradeCardSo : UpgradeCardSo
    {
        [BoxGroup("Design")] public IncrementalValue HealthValue;
        [BoxGroup("Design")] public IncrementalValue AttackValue;
        [BoxGroup("Design")] public IncrementalValue SpeedValue;
        [BoxGroup("References")] public SoldierBrainCore SoldierPrefab;
    }
}