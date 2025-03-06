using Castle;
using PowerPoints;
using UnityEngine;
namespace Enemy_Controllers
{
    public class EnemyTowerUpgrade : MonoBehaviour
    {
//-------Public Variables-------//


//------Serialized Fields-------//
        [SerializeField] private CastleUpgradeManager UpgradeManager;
        [SerializeField] private CastleDataHolder DataHolder;
        [SerializeField] private int MaxLevel = 5;

//------Private Variables-------//
        private int _currentLevel = 1;
        private XpController _xpController;
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
        
        private void Update()
        {
            if (CanUpdate())
                return;
            _currentLevel++;
            _killCount = 0;
            if (_currentLevel >= MaxLevel)
            {
                enabled = false;
                return;
            }
            UpgradeManager.UpdateTheCastle();
        }
       

#endregion


#region PUBLIC_METHODS

#endregion


#region PRIVATE_METHODS
        private bool CanUpdate()
        {
            var targetXp = (int)_xpController.TargetXpPoints.GetValueOnLevel(_currentLevel + 1);
            var progress = Mathf.InverseLerp(0f, targetXp, _killCount);
            if (progress < 1f)
                return true;
            return false;
        }
        
        private void OnMemberKilledRival()
        {
            _killCount++;
        }
#endregion
    }
}