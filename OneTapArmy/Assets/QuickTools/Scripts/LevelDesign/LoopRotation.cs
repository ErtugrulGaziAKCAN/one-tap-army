using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
namespace QuickTools.Scripts.LevelDesign
{
    [AddComponentMenu("QuickTools/Level Design/Loop Rotation")]
    public class LoopRotation : MonoBehaviour
    {
        private enum Axis
        {
            X,
            Y,
            Z
        }

//-------Public Variables-------//


//------Serialized Fields-------//
        [SerializeField, EnumToggleButtons] private Axis RotationAxis = Axis.Y;

        [SerializeField, BoxGroup("Angles")] private float BeginAngle;

        [SerializeField, BoxGroup("Angles")] private float EndAngle;

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

        [SerializeField] private UnityEvent OnStartTween;
        [SerializeField] private UnityEvent OnCompleteTween;

//------Private Variables-------//
        private Sequence _sequence;

#region UNITY_METHODS

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
            var initialEuler = transform.eulerAngles;
            var targetAngle = initialEuler;
            switch (RotationAxis)
            {
                case Axis.X:
                    initialEuler.x = BeginAngle;
                    targetAngle.x = EndAngle;
                    break;
                case Axis.Y:
                    initialEuler.y = BeginAngle;
                    targetAngle.y = EndAngle;
                    break;
                case Axis.Z:
                    initialEuler.z = BeginAngle;
                    targetAngle.z = EndAngle;
                    break;
            }

            transform.rotation = Quaternion.Euler(initialEuler);
            _sequence = DOTween.Sequence();
            _sequence.InsertCallback(0, () => { OnStartTween?.Invoke(); }).SetDelay(WaitAfterComeBack);
            _sequence.Append(
                transform.DORotate(targetAngle, TravelTime)
                    .SetEase(TravelCurve));
            _sequence.Append(
                transform.DORotate(initialEuler, TravelTime)
                    .SetEase(ComeBackCurve)
                    .SetDelay(WaitAfterEnd)
                    .OnComplete(() => { OnCompleteTween?.Invoke(); }));
            _sequence.SetLoops(-1, LoopType.Restart);
        }

#endregion
    }
}