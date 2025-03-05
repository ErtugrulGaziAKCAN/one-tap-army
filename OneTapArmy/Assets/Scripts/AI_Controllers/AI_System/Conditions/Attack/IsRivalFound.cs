using AI_Controllers.DataHolder.Core;
using QuickTools.Scripts.Utilities;
using scriptable_states.Runtime;
using UnityEngine;
namespace AI_Controllers.AI_System.Conditions.Attack
{
    [CreateAssetMenu(menuName = "Scriptable State Machine/Conditions/Attack/IsRivalFound", fileName = "new IsRivalFound")]
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
            EditorDebug.Log("Searching");
            statesComponent.TryGetComponent(out AIDataHolderCore dataHolder);
            var hits = new Collider[5];
            var hitCount = Physics.OverlapSphereNonAlloc(dataHolder.AITransform.position,
                dataHolder.RivalSensorRange,
                hits, dataHolder.RivalLayer);
            if (hitCount == 0)
                return false;
            var closestDistance = 999f;
            Collider closestRival = null;
            foreach (var hit in hits)
            {
               if(hit==null)
                   continue;
               var distance= Vector3.Distance(hit.transform.position, dataHolder.AITransform.position);
               if(distance>=closestDistance)
                   continue;
               closestDistance = distance;
               closestRival = hit;
            }
            var rivalHealth = closestRival.GetComponent<AIHealthController>();
            if (rivalHealth.IsDead)
                return false;
            dataHolder.ClosestRivalHealth = rivalHealth;
            EditorDebug.Log("Found");
            return true;
        }

#endregion


#region PRIVATE_METHODS

#endregion
    }
}