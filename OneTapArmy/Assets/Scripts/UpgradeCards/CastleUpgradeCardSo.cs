using QuickTools.QuickUpgradables.Scripts;
using Sirenix.OdinInspector;
using UnityEngine;
using UpgradeCards.Base;
namespace UpgradeCards
{
    [CreateAssetMenu(fileName = "CastleUpgradeCardSo", menuName = "Upgrade Cards/Create CastleUpgradeCardSo")]
    public class CastleUpgradeCardSo : UpgradeCardSo
    {
        [BoxGroup("Design")] public IncrementalValue Health;
        [BoxGroup("Design")] public IncrementalValue SpawnRate;
    }
}