using System;
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
        private Tween _delay;
        private bool _canSpawn = true;
        
#region UNITY_METHODS

        private void Start()
        {
            StartSpawning(true);
        }

        private void OnDisable()
        {
            _delay.Kill();
        }

#endregion


#region PUBLIC_METHODS

        [Button]
        public void StartSpawning(bool loop)
        {
            if(!_canSpawn)
                return;
            if (loop)
                _delay = DOVirtual.DelayedCall(SpawnInterval, () => SpawnObject(true));
            else
                SpawnObject(false);
        }

        public void CanSpawn(bool canSpawn) => _canSpawn = canSpawn;
#endregion


#region PRIVATE_METHODS

        private void SpawnObject(bool loop)
        {
            var targetPrefab = Prefabs.GetWeightedRandom(Weights, 10);
            var targetPos = SpawnPoints[_currentSpawnIndex % SpawnPoints.Count];
            var spawned = Instantiate(targetPrefab, targetPos);
            spawned.transform.localPosition = Vector3.zero;
            _currentSpawnIndex++;
            if (loop)
                StartSpawning(true);
        }

#endregion
    }
}