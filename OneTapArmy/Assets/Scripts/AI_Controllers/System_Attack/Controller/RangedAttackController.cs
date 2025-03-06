using AI_Controllers.DataHolder;
using AI_Controllers.System_Attack.Controller.Core;
using DG.Tweening;
using Plugins.CW.LeanPool.Required.Scripts;
using QuickTools.Scripts.Extensions;
using QuickTools.Scripts.HealthSystem;
using UnityEngine;
namespace AI_Controllers.System_Attack.Controller
{
    public class RangedAttackController : AIAttackControllerBase
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
            if (CanEnemyTakeAttack(targetEnemy))
                return;
            var targetPos = targetEnemy.transform.position;
            var distance = Vector3.Distance(targetPos, transform.position);
            if (distance > DataHolder.AttackDistance)
                return;
            var rangedData = DataHolder as RangedAIDataHolder;
            if (rangedData == null)
                return;
            SpawnProjectile(rangedData, targetPos, targetEnemy);
            base.OnAttacked();
        }

#endregion


#region PRIVATE_METHODS

        private void SpawnProjectile(RangedAIDataHolder rangedData, Vector3 targetPos, HealthCore targetEnemy)
        {
            var projectile = rangedData.Projectile;
            var spawnPoint = rangedData.ProjectileSpawnPoint;
            rangedData.LastProjectilePosition = spawnPoint.position;
            var spawned = LeanPool.Spawn(projectile, spawnPoint.position, spawnPoint.rotation);
            spawned.transform.DOJump(targetPos, .65f, 1, 1f).SetEase(Ease.Linear).SetSpeedBased().OnUpdate(() =>
            {
                var spawnedPos = spawned.transform.position;
                spawned.transform.rotation =
                    Quaternion.LookRotation(rangedData.LastProjectilePosition.DirectionTo(spawnedPos));
                rangedData.LastProjectilePosition = spawnedPos;
            }).OnComplete(() =>
            {
                LeanPool.Despawn(spawned);
                if (CanEnemyTakeAttack(targetEnemy))
                    return;
                targetEnemy.TakeDamage(DataHolder.AttackDamage);
            });
        }

        private bool CanEnemyTakeAttack(HealthCore targetEnemy)
        {
            if (targetEnemy == null)
                return true;
            if (targetEnemy.IsDead)
                return true;
            return false;
        }

#endregion
    }
}