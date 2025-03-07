using DG.Tweening;
using Obvious.Soap;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
namespace QuickTools.Scripts.Utilities
{
    [AddComponentMenu("QuickTools/Events/Event Raiser")]
    public class EventRaiser : MonoBehaviour
    {
//-------Public Variables-------//


//------Serialized Fields-------//
        [SerializeField] private ExecutionTime RaiseExecutionTime;
        [SerializeField, HideIf(nameof(RandomDelay))] private bool UseDelayWithRef;
        [SerializeField, ShowIf(nameof(UseDelayWithRef)), InfoBox("Can not Work With Awake ExecutionTime")]
        private FloatReference DelayRef;
        [SerializeField, HideIf(nameof(UseDelayWithRef))] private bool RandomDelay;
        [SerializeField, HideIf(nameof(HideDelayValue))] private float Delay;
        [SerializeField, HideIf(nameof(UseDelayWithRef)), ShowIf(nameof(RandomDelay))]
        private Vector2 RandomDelayDurations;
        [SerializeField] private bool UseUnScaledTime;
        [SerializeField, EnumToggleButtons] private EventType Type;

        [SerializeField, LabelText("Event"), Indent, ShowIf("@Type == EventType.GameEvent")]
        private ScriptableEventNoParam Event;

        [SerializeField, LabelText("Event"), Indent, ShowIf("@Type == EventType.UnityEvent")]
        private UnityEvent UnityEvent;

//------Private Variables-------//
        private enum ExecutionTime
        {
            Callback,
            Awake,
            OnEnable,
            OnDisable,
            Start,
        }

        private enum EventType
        {
            GameEvent,
            UnityEvent,
        }


#region UNITY_METHODS

        private void Awake()
        {
            if (RaiseExecutionTime == ExecutionTime.Awake)
                StartRaiseCoroutine();
        }

        private void OnEnable()
        {
            if (RaiseExecutionTime == ExecutionTime.OnEnable)
                StartRaiseCoroutine();
        }

        private void OnDisable()
        {
            if (RaiseExecutionTime == ExecutionTime.OnDisable)
                StartRaiseCoroutine();
        }

        private void Start()
        {
            if (UseDelayWithRef)
                Delay = DelayRef.Value;
            if (RaiseExecutionTime == ExecutionTime.Start)
                StartRaiseCoroutine();
        }

#endregion


#region PUBLIC_METHODS

        public void Raise()
        {
            if (RaiseExecutionTime == ExecutionTime.Callback)
                StartRaiseCoroutine();
        }

#endregion


#region PRIVATE_METHODS

        private void RaiseEvent()
        {
            if (Type == EventType.GameEvent)
                Event.Raise();
            else
                UnityEvent?.Invoke();
        }

        private void StartRaiseCoroutine()
        {
            if (!UseDelayWithRef && RandomDelay)
                Delay = Random.Range(RandomDelayDurations.x, RandomDelayDurations.y);
            if (Delay > 0)
                DOVirtual.DelayedCall(Delay, RaiseEvent).SetUpdate(UseUnScaledTime);
            else
                RaiseEvent();
        }

        private bool HideDelayValue() => UseDelayWithRef || RandomDelay;

#endregion
    }
}