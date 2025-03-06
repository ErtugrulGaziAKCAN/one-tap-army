using AI_Controllers.System_Attack.Controller.Core;
using DG.Tweening;
using UnityEngine;
namespace AI_Controllers.System_Attack.Controller
{
    public class CastleAttackController : RangedAttackController
    {
//-------Public Variables-------//


//------Serialized Fields-------//
        [SerializeField] private float AttackInterval;

//------Private Variables-------//

#region UNITY_METHODS

#endregion


#region PUBLIC_METHODS

#endregion


#region PRIVATE_METHODS

#endregion
        protected override void OnAttacked()
        {
            base.OnAttacked();
            DOVirtual.DelayedCall(AttackInterval, () => DataHolder.IsAttacking = false);
        }
    }
}