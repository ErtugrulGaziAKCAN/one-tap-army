using System.Collections.Generic;
using DG.Tweening;
using MonKey.Extensions;
using Sirenix.OdinInspector;
using UnityEngine;
namespace Utilities
{
    public class RandomObjectSpawner : MonoBehaviour
    {
//-------Public Variables-------//


//------Serialized Fields-------//
        [SerializeField] private List<GameObject> Prefabs;
        [SerializeField, InfoBox("Total Value of weights should be 10"), SuffixLabel("Spawn Chances")]
        private List<float> Weights;
        [SerializeField] private List<Transform> SpawnPoints;
        [SerializeField] private float SpawnInterval = 10f;

//------Private Variables-------//
        private int _currentSpawnIndex;

#region UNITY_METHODS

        private void Start()
        {
            StartSpawning(true);
        }

#endregion


#region PUBLIC_METHODS

        [Button]
        public void StartSpawning(bool loop)
        {
            if (_currentSpawnIndex >= SpawnPoints.Count)
                return;
            if (loop)
                DOVirtual.DelayedCall(SpawnInterval, () => SpawnObject(true));
            else
                SpawnObject(false);
        }

#endregion


#region PRIVATE_METHODS

        private void SpawnObject(bool loop)
        {
            var targetPrefab = Prefabs.GetWeightedRandom(Weights, 10);
            var targetPos = SpawnPoints[_currentSpawnIndex];
            var spawned= Instantiate(targetPrefab, targetPos);
            spawned.transform.localPosition = Vector3.zero;
            _currentSpawnIndex++;
            if (loop)
                StartSpawning(true);
        }

#endregion
    }
}