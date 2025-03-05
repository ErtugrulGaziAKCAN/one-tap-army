using AI_Controllers.DataHolder.Core;
using scriptable_states.Runtime;
using UnityEngine;
namespace AI_Controllers.AI_System.Conditions
{
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
            statesComponent.TryGetComponent(out AIDataHolderCore dataHolder);
            var hits = new Collider[1];
            var hitCount = Physics.OverlapSphereNonAlloc(dataHolder.AITransform.position,
                dataHolder.CollectibleSensorRange,
                hits, dataHolder.RivalLayer);
            if (hitCount == 0)
                return false;
            var rivalHealth = hits[0].GetComponent<AIHealthController>();
            if (rivalHealth.IsDead)
                return false;
            dataHolder.ClosestRivalHealth = rivalHealth;
            return true;
        }

#endregion


#region PRIVATE_METHODS

#endregion
    }
}