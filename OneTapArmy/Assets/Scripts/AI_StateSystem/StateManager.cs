using System;
using System.Collections.Generic;
using QuickTools.Scripts.Utilities;
using Sirenix.OdinInspector;
using UnityEngine;

namespace AI_StateSystem
{
    public abstract class StateManager : MonoBehaviour
    {
//-------Public Variables-------//


//------Serialized Fields-------//
        [FoldoutGroup("Debug"), SerializeField, PropertyOrder(-9999)]
        private bool DebugState;

//------Private Variables-------//
        private readonly Dictionary<Enum, State> _allStates = new Dictionary<Enum, State>();
        private State _currentState;
        protected Enum CurrentStateEnum;
        private bool _isActive;
        private int _stateChangeCount;

#region UNITY_METHODS

        protected virtual void Start()
        {
            Init();
        }

#endregion

#region ABSTRACT_METHODS

        protected abstract void Init();

        private void Update()
        {
            if (!_isActive)
                return;
            _currentState?.Tick();
        }

        private void FixedUpdate()
        {
            if (!_isActive)
                return;
            _currentState?.FixedTick();
        }

#endregion

#region PUBLIC_METHODS

        [Button]
        public virtual void SetActive(bool isActive)
        {
            _isActive = isActive;
        }


        public virtual void ChangeState(Enum stateName)
        {
            _currentState?.End();
            var targetState = GetState(stateName);
            if (targetState == null)
                EditorDebug.LogWarning(stateName + " was not found");
            _currentState = targetState;
            CurrentStateEnum = stateName;
            _stateChangeCount++;
            if (DebugState)
                EditorDebug.Log("AI____" + stateName + "____count: " + _stateChangeCount);
            _currentState?.Start();
        }

        public bool CurrentStateIs(Enum stateName)
        {
            return Equals(CurrentStateEnum, stateName);
        }

        public void RegisterState(Enum stateName, State state)
        {
            _allStates.Add(stateName, state);
        }

#endregion


#region PRIVATE_METHODS

        private State GetState(Enum stateName)
        {
            _allStates.TryGetValue(stateName, out var value);
            return value;
        }

#endregion

#region PROTECTED_METHODS

        protected void UnRegisterState(Enum stateName)
        {
            _allStates.Remove(stateName);
        }

        [Button]
        private void DebugCurrentState()
        {
            EditorDebug.Log("AI____" + CurrentStateEnum + "____count: " + _stateChangeCount);
        }

#endregion
    }
}