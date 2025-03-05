using AI_Controllers.DataHolder.Core;
using QuickTools.Scripts.Collectibles.Core;
using scriptable_states.Runtime;
using UnityEngine;
namespace AI_Controllers.AI_System.Conditions
{
    [CreateAssetMenu(menuName = "Scriptable State Machine/Conditions/IsCollectibleFound",
        fileName = "new IsCollectibleFound")]
    public class IsCollectibleFound : ScriptableCondition
    {
//-------Public Variables-------//
        public LayerMask CollectibleLayer;

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
                hits, CollectibleLayer);
            if (hitCount == 0)
                return OnNotFound(dataHolder);
            var collectible = hits[0].GetComponent<CollectibleCore>();
            if (collectible.IsCollected())
                return OnNotFound(dataHolder);
            
            dataHolder.ClosestCollectible = collectible;
            return true;
        }

#endregion


#region PRIVATE_METHODS

        private bool OnNotFound(AIDataHolderCore dataHolder)
        {
            dataHolder.Agent.stoppingDistance = dataHolder.AgentStoppingDistance;
            return false;
        }

#endregion
    }
}