using QuickTools.Scripts.HealthSystem;
using UnityEngine;
using UpgradeCards.Data;
namespace Castle
{
    public class PlayerCastleController : MonoBehaviour
    {
//-------Public Variables-------//


//------Serialized Fields-------//
        [SerializeField] private CastleUpgradeCardSo CastleUpgradeCard;

//------Private Variables-------//
        private CastleUpgradeManager _castleUpgradeManager;
        private HealthCore _healthCore;

#region UNITY_METHODS

        private void OnEnable()
        {
            TryGetComponent(out _castleUpgradeManager);
            TryGetComponent(out _healthCore);
            CastleUpgradeCard.OnUpgraded += OnUpgraded;
        }

        private void OnDisable()
        {
            CastleUpgradeCard.OnUpgraded -= OnUpgraded;
        }

#endregion


#region PUBLIC_METHODS

#endregion


#region PRIVATE_METHODS

        private void OnUpgraded()
        {
            _castleUpgradeManager.UpgradeTower(false);
            _healthCore.SetMaxHealth(CastleUpgradeCard.Health.GetValueOnLevel(CastleUpgradeCard.CurrentCardLevel));
            _healthCore.ResetHealth();
        }

#endregion
    }
}