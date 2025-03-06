using Castle;
using UnityEngine;
using Random = UnityEngine.Random;
namespace Enemy_Controllers
{
    public class EnemyCastleSelector : MonoBehaviour
    {
//-------Public Variables-------//


//------Serialized Fields-------//
        [SerializeField] private Vector2 SelectingTargetInterval;
        [SerializeField] private CastleDataHolder DataHolder;

//------Private Variables-------//
        private float _elapsedTime;
        private float _interval;

#region UNITY_METHODS

        private void Start()
        {
            SetInterval();
        }

        private void Update()
        {
            if (!CanSelect())
                return;
            var targetPos = TargetCastleFinder.Instance.GetRandomCastle(DataHolder);
            DataHolder.SpawnedAIList.ForEach(s => s.TargetPosition = targetPos.transform.position);
        }

#endregion


#region PUBLIC_METHODS

#endregion


#region PRIVATE_METHODS

        private void SetInterval() =>
            _interval = Random.Range(SelectingTargetInterval.x, SelectingTargetInterval.y);

        private bool CanSelect()
        {
            _elapsedTime += Time.deltaTime;
            var canSelect = _elapsedTime >= _interval;
            if (canSelect)
                _elapsedTime = 0f;
            return canSelect;
        }

#endregion
    }
}