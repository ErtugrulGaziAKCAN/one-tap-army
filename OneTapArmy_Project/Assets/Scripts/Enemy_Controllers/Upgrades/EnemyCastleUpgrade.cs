using Castle;
using UnityEngine;
namespace Enemy_Controllers.Upgrades
{
    public class EnemyCastleUpgrade : MonoBehaviour
    {
//-------Public Variables-------//


//------Serialized Fields-------//
        [SerializeField] private CastleUpgradeManager UpgradeManager;
        [SerializeField] private int MaxLevel = 5;

//------Private Variables-------//
#region UNITY_METHODS

#endregion


#region PUBLIC_METHODS

        public bool CanUpgrade() => UpgradeManager.GetCurrentLevel() < MaxLevel;
        
        public void UpdateCastle() => UpgradeManager.UpdateTheCastle();

#endregion


#region PRIVATE_METHODS

#endregion
    }
}