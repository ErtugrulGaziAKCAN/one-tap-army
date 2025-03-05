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
        [SerializeField ]private HealthCore HealthCore;

//------Private Variables-------//
        private CastleUpgradeManager _castleUpgradeManager;

#region UNITY_METHODS

        private void OnEnable()
        {
            TryGetComponent(out _castleUpgradeManager);
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
            HealthCore.SetMaxHealth(CastleUpgradeCard.Health.GetValueOnLevel(CastleUpgradeCard.CurrentCardLevel));
            HealthCore.ResetHealth();
        }

#endregion
    }
}