using Castle;
using PowerPoints;
using QuickTools.Scripts.Utilities;
using Sirenix.OdinInspector;
using UnityEngine;
namespace Enemy_Controllers.Upgrades
{
    public class EnemyUpgradeController : MonoBehaviour
    {
//-------Public Variables-------//


//------Serialized Fields-------//
        [SerializeField, Range(0f, 1f), TabGroup("Design")] private float AIUpgradeChance;
        [SerializeField, TabGroup("References")] private CastleDataHolder DataHolder;
        [SerializeField, TabGroup("References")] private EnemyAIUpgrade AIUpgrade;
        [SerializeField, TabGroup("References")] private EnemyCastleUpgrade CastleUpgrade;

//------Private Variables-------//
        private XpController _xpController;
        private int _currentXpPhase;
        private int _killCount;

#region UNITY_METHODS

        private void OnEnable()
        {
            DataHolder.OnMemberKilledRival += OnMemberKilledRival;
        }

        private void OnDisable()
        {
            DataHolder.OnMemberKilledRival -= OnMemberKilledRival;
        }

        private void Start()
        {
            _xpController = XpController.Instance;
        }

#endregion


#region PUBLIC_METHODS

        private void Update()
        {
            if (!CanUpgrade())
                return;
            var chance = Random.Range(0f, 1f);
            if (chance <= AIUpgradeChance && AIUpgrade.CanUpgrade())
                AIUpgrade.UpgradeAI();
            else if (CastleUpgrade.CanUpgrade())
                CastleUpgrade.UpdateCastle();
            _killCount = 0;
            _currentXpPhase++;
            EditorDebug.Log("Worked");
        }
        
#endregion


#region PRIVATE_METHODS

        private bool CanUpgrade()
        {
            var targetXp = (int)_xpController.TargetXpPoints.GetValueOnLevel(_currentXpPhase + 1);
            var progress = Mathf.InverseLerp(0f, targetXp, _killCount);
            if (progress < 1f)
                return false;
            return true;
        }

        private void OnMemberKilledRival()
        {
            _killCount++;
        }

#endregion
    }
}