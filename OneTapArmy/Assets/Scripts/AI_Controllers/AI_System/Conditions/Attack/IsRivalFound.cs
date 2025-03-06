using System.Collections.Generic;
using System.Linq;
using AI_Controllers.DataHolder.Core;
using QuickTools.Scripts.HealthSystem;
using QuickTools.Scripts.Utilities.RaycastVisualization._3D;
using scriptable_states.Runtime;
using UnityEngine;
namespace AI_Controllers.AI_System.Conditions.Attack
{
    [CreateAssetMenu(menuName = "Scriptable State Machine/Conditions/Attack/IsRivalFound",
        fileName = "new IsRivalFound")]
    public class IsRivalFound : ScriptableCondition
    {
//-------Public Variables-------//


//------Serialized Fields-------//


//------Private Variables-------//

#region UNITY_METHODS

#endregion


#region PUBLIC_METHODS

        public override bool Verify(StateComponent statesComponent)
        {
            if (Time.frameCount % 10 != 0)
                return false;
            statesComponent.TryGetComponent(out AIDataHolderCore dataHolder);
            if (!CheckHits(dataHolder, out var targets))
                return false;
            if (targets.Count == 0)
                return false;
            var closestRival = FindClosestRival(targets, dataHolder);
            var rivalHealth = closestRival.GetComponent<HealthCore>();
            if (rivalHealth == null)
                return false;
            if (rivalHealth.IsDead)
                return false;
            dataHolder.ClosestRivalHealth = rivalHealth;
            return true;
        }

        private static bool CheckHits(AIDataHolderCore dataHolder, out List<Collider> targets)
        {
            var hits = new Collider[30];
            var hitCount = VisualPhysics.OverlapSphereNonAlloc(dataHolder.AITransform.position,
                dataHolder.RivalSensorRange,
                hits, dataHolder.RivalLayer);
            if (hitCount == 0)
            {
                targets = null;
                return false;
            }
            targets = hits.Where((h) =>
            {
                if (h == null)
                    return false;
                var rivalData = h.GetComponent<HealthCore>();
                return rivalData.HealthID != dataHolder.AIHealth.HealthID;
            }).ToList();
            return true;
        }

        private static Collider FindClosestRival(List<Collider> targets, AIDataHolderCore dataHolder)
        {
            var closestDistance = 999f;
            Collider closestRival = null;
            foreach (var target in targets)
            {
                if (target == null)
                    continue;
                var distance = Vector3.Distance(target.transform.position, dataHolder.AITransform.position);
                if (distance >= closestDistance)
                    continue;
                closestDistance = distance;
                closestRival = target;
            }
            return closestRival;
        }

#endregion


#region PRIVATE_METHODS

#endregion
    }
}