using Obvious.Soap;
using scriptable_states.Runtime;
using Touchables;
using UnityEngine;
namespace Player
{
    public class AllyDirector : MonoBehaviour, ITouchable
    {
//-------Public Variables-------//


//------Serialized Fields-------//
        [SerializeField] private ScriptableListAIDataHolderCore SpawnedAllySoldiers;
        [SerializeField] private ScriptableEventNoParam OnTouchedToDirectTheAllies;
        [SerializeField] private ScriptableState MovementState;
//------Private Variables-------//

#region UNITY_METHODS

#endregion


#region PUBLIC_METHODS

        public void OnTouched(Vector3 touchPoint)
        {
            SpawnedAllySoldiers.ForEach((a) =>
            {
                if (a.GetComponent<StateComponent>().CurrentState == MovementState)
                    a.TargetPosition = touchPoint;
            });
            OnTouchedToDirectTheAllies.Raise();
        }

#endregion


#region PRIVATE_METHODS

#endregion
    }
}