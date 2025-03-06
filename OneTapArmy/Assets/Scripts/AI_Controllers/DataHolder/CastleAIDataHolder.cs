using AI_Controllers.DataHolder.Core;
using UnityEngine;
namespace AI_Controllers.DataHolder
{
    public class CastleAIDataHolder : AIDataHolderCore
    {

//-------Public Variables-------//
        public Transform ProjectileSpawnPoint;
        public GameObject Projectile;
        [HideInInspector] public Vector3 LastProjectilePosition;

//------Serialized Fields-------//


//------Private Variables-------//

#region UNITY_METHODS

#endregion


#region PUBLIC_METHODS

        public void SetAttacking(bool isAttacking) => IsAttacking = isAttacking;

#endregion


#region PRIVATE_METHODS

#endregion

    }
}