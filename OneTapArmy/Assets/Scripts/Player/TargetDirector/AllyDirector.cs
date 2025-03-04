using Obvious.Soap;
using scriptable_states.Runtime;
using Touchables;
using UnityEngine;
namespace Player.TargetDirector
{
    public class AllyDirector : MonoBehaviour, ITouchable
    {
//-------Public Variables-------//


//------Serialized Fields-------//
        [SerializeField] private ScriptableListAIDataHolderCore SpawnedAllySoldiers;
        [SerializeField] private ScriptableEventNoParam OnTouchedToDirectTheAllies;
        [SerializeField] private ScriptableState MovementState;
//------Private Variables-------//
        private AllyDirectorVisualizer _directorVisualizer;

#region UNITY_METHODS

        private void Awake()
        {
            TryGetComponent(out _directorVisualizer);
        }

#endregion


#region PUBLIC_METHODS

        public void OnTouched(Vector3 touchPoint)
        {
            SpawnedAllySoldiers.ForEach((a) =>
            {
                if (a.StateComponentAccess.CurrentState == MovementState)
                    a.TargetPosition = touchPoint;
            });
            OnTouchedToDirectTheAllies.Raise();
            _directorVisualizer.DrawWorldLine(touchPoint);
        }

#endregion


#region PRIVATE_METHODS

#endregion
    }
}