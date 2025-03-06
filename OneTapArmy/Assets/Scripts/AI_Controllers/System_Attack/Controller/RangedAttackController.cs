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
            SpawnProjectile(targetPos, targetEnemy);
            base.OnAttacked();
        }

#endregion


#region PRIVATE_METHODS

        private void SpawnProjectile(Vector3 targetPos, HealthCore targetEnemy)
        {
            var projectile = InitProperties(out var spawnPoint);
            var spawned = LeanPool.Spawn(projectile, spawnPoint.position, spawnPoint.rotation);
            Vector3 direction=Vector3.zero;
            spawned.transform.DOJump(targetPos, .65f, 1, 1f).SetEase(Ease.Linear).SetSpeedBased().OnUpdate(() =>
            {
                var spawnedPos = spawned.transform.position;
                if (DataHolder is RangedSoldierAIDataHolder ranged)
                {
                     direction = ranged.LastProjectilePosition.DirectionTo(spawnedPos);
                     ranged.LastProjectilePosition = spawnedPos;
                }
                else if (DataHolder is CastleAIDataHolder castle)
                {
                    direction = castle.LastProjectilePosition.DirectionTo(spawnedPos);
                    castle.LastProjectilePosition = spawnedPos;
                }
                if (direction != Vector3.zero)
                    spawned.transform.rotation = Quaternion.LookRotation(direction);
            }).OnComplete(() =>
            {
                LeanPool.Despawn(spawned);
                if (CanEnemyTakeAttack(targetEnemy))
                    return;
                targetEnemy.TakeDamage(DataHolder.AttackDamage);
            });
        }
        
        private GameObject InitProperties(out Transform spawnPoint)
        {
            GameObject projectile = null;
            spawnPoint = null;

            if (DataHolder is RangedSoldierAIDataHolder rangedHolder)
            {
                projectile = rangedHolder.Projectile;
                spawnPoint = rangedHolder.ProjectileSpawnPoint;
                rangedHolder.LastProjectilePosition = spawnPoint.position;
            }
            else if (DataHolder is CastleAIDataHolder castleHolder)
            {
                projectile = castleHolder.Projectile;
                spawnPoint = castleHolder.ProjectileSpawnPoint;
                castleHolder.LastProjectilePosition = spawnPoint.position;
            }
            return projectile;
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