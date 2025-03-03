using DG.Tweening;
using Plugins.CW.LeanPool.Required.Scripts;
using Sirenix.OdinInspector;
using UnityEngine;
namespace QuickTools.Scripts.Utilities
{
    [AddComponentMenu("QuickTools/Fx/Particle Despawner")]
    public class ParticleDespawner : MonoBehaviour
    {
//-------Public Variables-------//


//------Serialized Fields-------//
        [InfoBox("Please set Particle System's Stop Action to \"Callback\"", InfoMessageType.Error,
            "@DespawnOn == StopType.ParticleStop && !IsStopActionSetToCallback()")]
        [SerializeField]
        private StopType DespawnOn = StopType.ParticleStop;

        [SerializeField, ShowIf("@DespawnOn == StopType.TimeAfterSpawn"), SuffixLabel("sec"), MinValue(0f)]
        private float Time;

//------Private Variables-------//
        private enum StopType
        {
            ParticleStop,
            TimeAfterSpawn,
        }

        private ParticleSystem _ps;


#region UNITY_METHODS

        private void Awake()
        {
            _ps = GetComponent<ParticleSystem>();
        }

        private void OnEnable()
        {
            if (DespawnOn == StopType.TimeAfterSpawn)
                DOVirtual.DelayedCall(Time, () => LeanPool.Despawn(_ps));
        }

#endregion


#region PUBLIC_METHODS

        public void OnParticleSystemStopped()
        {
            if (DespawnOn == StopType.ParticleStop)
                LeanPool.Despawn(_ps);
        }

#endregion


#region PRIVATE_METHODS

        private bool IsStopActionSetToCallback()
        {
            if (!_ps)
                _ps = GetComponent<ParticleSystem>();
            return _ps.main.stopAction == ParticleSystemStopAction.Callback;
        }

#endregion
    }
}