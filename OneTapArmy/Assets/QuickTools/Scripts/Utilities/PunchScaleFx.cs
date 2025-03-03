using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
namespace QuickTools.Scripts.Utilities
{
    [AddComponentMenu("QuickTools/Fx/Punch Scale")]
    public class PunchScaleFx : MonoBehaviour
    {
//-------Public Variables-------//


//------Serialized Fields-------//
        [SerializeField] private bool AutoOriginalScale = true;

        [SerializeField, DisableIf(nameof(AutoOriginalScale)), Indent]
        private Vector3 OriginalScale = Vector3.one;

        [SerializeField, Range(0f, 1f)] private float PunchScale = .2f;
        [SerializeField] private float Duration = .53f;

//------Private Variables-------//
        private Vector3 _autoOriginalScale;
        private Tween _scaleTween;


#region UNITY_METHODS

        private void Start()
        {
            if (AutoOriginalScale)
                _autoOriginalScale = transform.localScale;
        }

#endregion


#region PUBLIC_METHODS

        [Button]
        public void TriggerFx()
        {
            _scaleTween?.Kill();
            ResetToOriginalScale();
            _scaleTween = transform.DOPunchScale(Vector3.one * PunchScale, Duration, 7, .4f);
        }

#endregion


#region PRIVATE_METHODS

        private void ResetToOriginalScale()
        {
            transform.localScale = AutoOriginalScale ? _autoOriginalScale : OriginalScale;
        }

#endregion
    }
}