using QuickTools.QuickUpgradables.Scripts;
using Sirenix.OdinInspector;
using UnityEngine;
using UpgradeCards.Base;
namespace UpgradeCards
{
    [CreateAssetMenu(fileName = "SoldierUpgradeCardSo", menuName = "Upgrade Cards/Create SoldierUpgradeCards")]
    public class SoldierUpgradeCardSo : UpgradeCardSo
    {
        [BoxGroup("Design")] public IncrementalValue HealthValue;
        [BoxGroup("Design")] public IncrementalValue AttackValue;
        [BoxGroup("Design")] public IncrementalValue SpeedValue;
    }
}