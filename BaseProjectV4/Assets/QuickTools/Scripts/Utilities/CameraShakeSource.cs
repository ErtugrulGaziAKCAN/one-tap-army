using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
namespace QuickTools.Scripts.Utilities
{
    [CreateAssetMenu(fileName = "New Shake", menuName = "QuickTools/Camera Shake", order = 0)]
    public class CameraShakeSource : ScriptableObject
    {
        [SerializeField] private float Amplitude = 1f;
        [SerializeField] private float Frequency = 2f;
        [SerializeField] private float Duration = 2f;
        
        private readonly List<CameraShakeListener> _listeners = new List<CameraShakeListener>();

        [Button]
        public void Shake()
        {
            for (var i = _listeners.Count - 1; i >= 0; i--)
            {
                _listeners[i].OnShake(Amplitude, Frequency, Duration);
            }
        }

        public void RegisterListener(CameraShakeListener listener)
        {
            _listeners.Add(listener);
        }

        public void UnregisterListener(CameraShakeListener listener)
        {
            _listeners.Remove(listener);
        }
    }
}