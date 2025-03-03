using Obvious.Soap;
using QuickTools.QuickUpgradables.Scripts;
using QuickTools.Scripts.Utilities;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
namespace Player.Points
{
    public class PlayerXpController : MonoBehaviour
    {
//-------Public Variables-------//


//------Serialized Fields-------//
        [SerializeField] private IncrementalValue TargetXpPoints;
        [SerializeField] private FloatReference XpProgress;
        [SerializeField] private UnityEvent OnReachedTargetXp;

//------Private Variables-------//
        private int _currentXpStep = 1;
        private int _currentXp;

#region UNITY_METHODS

#endregion


#region PUBLIC_METHODS

        [Button]
        public void AddXp(int amount)
        {
            _currentXp += amount;
            var targetXp = TargetXpPoints.GetValueOnLevel(_currentXpStep);
            XpProgress.Value = Mathf.InverseLerp(0f, targetXp, _currentXp);
            EditorDebug.Log(XpProgress.Value);
            if (XpProgress.Value < 1f)
                return;
            OnReachedTargetXp?.Invoke();
            _currentXpStep++;
            _currentXp = 0;
            XpProgress.Value = 0f;
        }

#endregion


#region PRIVATE_METHODS

#endregion
    }
}