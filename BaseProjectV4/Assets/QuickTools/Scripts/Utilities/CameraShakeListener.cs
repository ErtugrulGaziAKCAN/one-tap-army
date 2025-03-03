using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using UnityEngine;
namespace QuickTools.Scripts.Utilities
{
    [AddComponentMenu("QuickTools/Utilities/Camera Shake Listener")]
    public class CameraShakeListener : MonoBehaviour
    {
//-------Public Variables-------//


//------Serialized Fields-------//
        [SerializeField] private List<CameraShakeSource> Sources;

//------Private Variables-------//
        private CinemachineVirtualCamera _virtualCamera;
        private CinemachineBasicMultiChannelPerlin _noiseChannel;


#region UNITY_METHODS

        private void Awake()
        {
            _virtualCamera = GetComponent<CinemachineVirtualCamera>();
            if (!_virtualCamera)
                Debug.LogError("Please make sure there is Cinemachine Virtual Camera on this camera shake listener",
                    gameObject);
            _noiseChannel = _virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            if (!_noiseChannel)
                Debug.LogError("Please make sure there is a valid noise channel on this virtual camera",
                    gameObject);
            if(!_noiseChannel.m_NoiseProfile)
                Debug.LogError("Please select \"6D Shake\" noise profile", gameObject);
        }

        private void OnEnable()
        {
            foreach (var source in Sources)
                source.RegisterListener(this);
        }

        private void OnDisable()
        {
            foreach (var source in Sources)
                source.UnregisterListener(this);
        }

#endregion


#region PUBLIC_METHODS

        public void OnShake(float amp, float freq, float duration)
        {
            var startAmp = amp;
            var startFreq = freq;
            SetAmplitude(startAmp);
            SetFrequency(startFreq);
            DOVirtual.Float(startAmp, 0f, duration, SetAmplitude)
                .SetEase(Ease.OutQuad);
            DOVirtual.Float(startFreq, 0f, duration, SetFrequency)
                .SetEase(Ease.OutQuad);
        }

#endregion


#region PRIVATE_METHODS

        private void SetAmplitude(float amp)
        {
            _noiseChannel.m_AmplitudeGain = amp;
        }

        private void SetFrequency(float freq)
        {
            _noiseChannel.m_FrequencyGain = freq;
        }

#endregion
    }
}