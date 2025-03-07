using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
namespace QuickTools.Scripts.UI
{
    public class UseCountContainer : MonoBehaviour
    {
//-------Public Variables-------//


//------Serialized Fields-------//
        [SerializeField] private UseCountIcon IconPrefab;

//------Private Variables-------//
        private int _maxCount = 3;
        private int _currentCount;
        private List<UseCountIcon> _icons = new List<UseCountIcon>();

#region UNITY_METHODS

#endregion


#region PUBLIC_METHODS

        [Button]
        public void SetMaxCount(int max)
        {
            _maxCount = max;
            Initialize();
        }

        [Button]
        public void SetRemainingCount(int remaining)
        {
            _currentCount = Mathf.Clamp(remaining, 0, _maxCount);
            UpdateUiImmediately();
        }

        [Button]
        public void AddOne()
        {
            _currentCount = Mathf.Clamp(_currentCount + 1, 0, _maxCount);
            UpdateUiAnimated();
        }

        [Button]
        public void UseOne()
        {
            _currentCount = Mathf.Clamp(_currentCount - 1, 0, _maxCount);
            UpdateUiAnimated();
        }

#endregion


#region PRIVATE_METHODS

        private void Initialize()
        {
            foreach (var icon in _icons)
            {
                DestroyImmediate(icon.gameObject);
            }

            _icons = new List<UseCountIcon>();
            for (var i = 0; i < _maxCount; i++)
            {
                var icon = Instantiate(IconPrefab, transform);
                icon.SetFullImmediate(false);
                _icons.Add(icon);
            }
        }

        private void UpdateUiAnimated()
        {
            for (var i = 0; i < _icons.Count; i++)
            {
                _icons[i].SetFullAnimated(i < _currentCount);
            }
        }

        private void UpdateUiImmediately()
        {
            for (var i = 0; i < _icons.Count; i++)
            {
                _icons[i].SetFullImmediate(i < _currentCount);
            }
        }

#endregion
    }
}