using AI_Controllers.System_Attack.Controller.Core;
using UnityEngine;
namespace AI_Controllers.System_Attack.Controller
{
    public class MeleeAttackController : AIAttackControllerBase
    {
//-------Public Variables-------//


//------Serialized Fields-------//


//------Private Variables-------//

#region UNITY_METHODS

#endregion


#region PUBLIC_METHODS

        protected override void OnAttacked()
        {
            var targetEnemy = DataHolder.ClosestRivalHealth;
            if (targetEnemy == null)
                return;
            if (targetEnemy.IsDead)
                return;
            var distance = Vector3.Distance(targetEnemy.transform.position, transform.position);
            if (distance > DataHolder.AttackDistance)
                return;
            targetEnemy.TakeDamage(DataHolder.AttackDamage);
            base.OnAttacked();
        }

#endregion


#region PRIVATE_METHODS

#endregion
    }
}