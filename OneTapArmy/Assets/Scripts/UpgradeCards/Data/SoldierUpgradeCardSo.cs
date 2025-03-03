using QuickTools.QuickUpgradables.Scripts;
using Sirenix.OdinInspector;
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
    }
}