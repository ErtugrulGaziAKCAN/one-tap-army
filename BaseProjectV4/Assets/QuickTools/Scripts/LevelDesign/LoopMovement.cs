using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
namespace QuickTools.Scripts.LevelDesign
{
    [AddComponentMenu("QuickTools/Level Design/Loop Movement")]
    public class LoopMovement : MonoBehaviour
    {
//-------Public Variables-------//


//------Serialized Fields-------//

        [SerializeField, BoxGroup("Local Positions")]
        private Vector3 MoveVector;

        [SerializeField, BoxGroup("Animation times in seconds")]
        private float TravelTime;

        [SerializeField, BoxGroup("Animation times in seconds")]
        private float WaitAfterEnd;

        [SerializeField, BoxGroup("Animation times in seconds")]
        private float WaitAfterComeBack;

        [SerializeField, BoxGroup("Animation times in seconds")]
        private bool RandomDelay = true;

        [SerializeField, BoxGroup("Animation times in seconds"), ShowIf(nameof(RandomDelay)), Indent]
        private float MinDelay = .2f;

        [SerializeField, BoxGroup("Animation times in seconds"), ShowIf(nameof(RandomDelay)), Indent]
        private float MaxDelay = 1f;


        [SerializeField, BoxGroup("Animation curves")]
        private Ease TravelCurve;

        [SerializeField, BoxGroup("Animation curves")]
        private Ease ComeBackCurve;


//------Private Variables-------//

        private Sequence _sequence;
        private Vector3 _beginPosition;

#region UNITY_METHODS

        private void Awake()
        {
            _beginPosition = transform.localPosition;
        }

        private void Start()
        {
            Invoke(nameof(Activate), RandomDelay ? Random.Range(MinDelay, MaxDelay) : 0f);
        }

#endregion

#region PUBLIC_METHODS

#endregion


#region PRIVATE_METHODS

        private void Activate()
        {
            _sequence = DOTween.Sequence();
            _sequence.Append(
                transform.DOLocalMove(MoveVector, TravelTime)
                    .SetEase(TravelCurve)
                    .SetRelative()
                    .SetDelay(WaitAfterComeBack));
            _sequence.Append(
                transform.DOLocalMove(_beginPosition, TravelTime)
                    .SetEase(ComeBackCurve)
                    .SetDelay(WaitAfterEnd));
            _sequence.SetLoops(-1, LoopType.Restart);
        }

#endregion
    }
}