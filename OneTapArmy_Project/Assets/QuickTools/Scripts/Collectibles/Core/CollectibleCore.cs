using DG.Tweening;
using Obvious.Soap;
using Plugins.CW.LeanPool.Required.Scripts;
using QuickTools.Epic_Toon_FX.Scripts;
using QuickTools.Scripts.ColliderSystem;
using QuickTools.Scripts.Extensions;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
namespace QuickTools.Scripts.Collectibles.Core
{
    public abstract class CollectibleCore : MonoBehaviour, ICollidable
    {
//-------Public Variables-------//


//------Serialized Fields-------//
        [SerializeField, SuffixLabel("Can be null")]
        private ParticleSystem CollectFx;
        [SerializeField, EnumToggleButtons] private EventType TargetEventType;
        [SerializeField, ShowIf("@TargetEventType==EventType.GameEvent")] private ScriptableEventNoParam CollectedEvent;
        [SerializeField, ShowIf("@TargetEventType==EventType.UnityEvent")] private UnityEvent CollectedUnityEvent;

        [SerializeField, Required] private Transform Model;
        [SerializeField] private bool DisableOnCollect = true;

        [SerializeField, ToggleGroup(nameof(HoverEffect))]
        private bool HoverEffect;

        [SerializeField, ToggleGroup(nameof(HoverEffect)), LabelText("Distance"),]
        private float HoverDistance;

        [SerializeField, ToggleGroup(nameof(HoverEffect)), LabelText("Duration"), SuffixLabel("sec")]
        private float HoverDuration;

        [SerializeField, ToggleGroup(nameof(Rotation))]
        private bool Rotation;

        [SerializeField, ToggleGroup(nameof(Rotation)), LabelText("Speed")]
        private float RotationSpeed;

//------Private Variables-------//
        private bool _isCollected;
        private enum EventType
        {
            GameEvent,
            UnityEvent
        }

#region UNITY_METHODS

        protected virtual void Start()
        {
            HoverFx();
            RotationFx();
        }

#endregion


#region PUBLIC_METHODS

        public virtual void OnCollide(GameObject collidedObject)
        {
            if (_isCollected)
                return;
            if (CollectFx != null)
                LeanPool.Spawn(CollectFx, transform.position.WithAddedY(.25f), CollectFx.transform.rotation);
            _isCollected = true;
            if (TargetEventType == EventType.GameEvent)
                CollectedEvent.Raise();
            else
                CollectedUnityEvent?.Invoke();
            if (DisableOnCollect)
                gameObject.SetActive(false);
        }

        public bool IsCollected() => _isCollected;

#endregion


#region PRIVATE_METHODS
        
        private void HoverFx()
        {
            if (!HoverEffect)
                return;
            Model.DOLocalMoveY(HoverDistance, HoverDuration)
                .SetEase(Ease.InOutSine)
                .SetRelative(true)
                .SetLoops(-1, LoopType.Yoyo);
        }

        private void RotationFx()
        {
            if (!Rotation)
                return;
            Model.TryGetComponent(out ETFXRotation rotator);
            if (!rotator)
                rotator = Model.gameObject.AddComponent<ETFXRotation>();
            rotator.rotateSpace = ETFXRotation.spaceEnum.World;
            rotator.rotateVector = Vector3.up.WithY(RotationSpeed);
        }

#endregion
    }
}