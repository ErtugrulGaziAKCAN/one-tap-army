using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
namespace AI_Controllers.Spawning
{
    public class AIWaitingPoints : MonoBehaviour
    {
//-------Public Variables-------//


//------Serialized Fields-------//
        [SerializeField] private List<Transform> WaitingTransforms;

//------Private Variables-------//
        private int _spawnedCount;

#region UNITY_METHODS

#endregion


#region PUBLIC_METHODS

        public Vector3 GetPoint()
        {
            var targetPoint = WaitingTransforms[_spawnedCount % WaitingTransforms.Count].position;
            _spawnedCount++;
            return targetPoint;
        }

        public void ClearData() => _spawnedCount = 0;

        public bool CanSpawn() => _spawnedCount < WaitingTransforms.Count;
        
        [Button]
        private void ReverseList() => WaitingTransforms.Reverse();

#endregion


#region PRIVATE_METHODS

#endregion
    }
}