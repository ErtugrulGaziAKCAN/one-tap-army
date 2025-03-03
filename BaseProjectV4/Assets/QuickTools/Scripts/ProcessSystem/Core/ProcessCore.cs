using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
namespace QuickTools.Scripts.ProcessSystem.Core
{
    public abstract class ProcessCore : MonoBehaviour
    {
        //-------Public Variables-------//
        [ShowInInspector, ReadOnly] public bool IsWorking { get; private set; }
        public UnityEvent OnProcessEndAction;

//------Serialized Fields-------//
        [SerializeField] protected float ProcessDuration;

//------Private Variables-------//
        protected float CurrentProcessPercentage;
        private Tween _processTween;


#region UNITY_METHODS

#endregion


#region PUBLIC_METHODS
        
        public void SetDuration(float duration)
        {
            ProcessDuration = duration;
        }

        public void StartProcessing()
        {
            if (IsWorking)
            {
                Debug.LogWarning("Process is already working", gameObject);
                return;
            }

            IsWorking = true;
            OnProcessStart(StartProduction);
        }

        public void ForceStopProcessing()
        {
            if (!IsWorking)
                return;
            IsWorking = false;
            _processTween.Kill();
            _processTween = null;
            CurrentProcessPercentage = 0f;
            CurrentProcessPercentage = 0f;
        }

#endregion


#region PRIVATE_METHODS

        private void StartProduction()
        {
            _processTween = DOVirtual.Float(0f, 1f, ProcessDuration, v =>
                {
                    CurrentProcessPercentage = v;
                    OnProcess();
                })
                .SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    CurrentProcessPercentage = 0f;
                    OnProcessEndAction?.Invoke();
                    if(IsWorking == false)
                        return;
                    IsWorking = false;
                    OnProcessEnd();
                });
        }

        protected abstract void OnProcessStart(Action onPreparationCompleted);
        protected abstract void OnProcess();
        protected abstract void OnProcessEnd();

#endregion
    }
}