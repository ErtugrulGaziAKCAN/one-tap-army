using Obvious.Soap;
using QuickTools.QuickUpgradables.Scripts;
using QuickTools.Scripts.Utilities;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
namespace PowerPoints
{
    public class XpController : QuickSingleton<XpController>
    {
//-------Public Variables-------//
        public IncrementalValue TargetXpPoints;

//------Serialized Fields-------//
        [SerializeField] private FloatReference XpProgress;
        [SerializeField] private UnityEvent OnReachedTargetXp;
        [SerializeField] private IntReference CurrentXpStep;
        [SerializeField] private IntReference CollectedXp;
        [SerializeField] private IntReference TargetXp;

//------Private Variables-------//

#region UNITY_METHODS

#endregion


#region PUBLIC_METHODS

        [Button]
        public void AddXp(int amount)
        {
            CollectedXp.Value += amount;
            TargetXp.Value = (int)TargetXpPoints.GetValueOnLevel(CurrentXpStep.Value);
            XpProgress.Value = Mathf.InverseLerp(0f, TargetXp.Value, CollectedXp.Value);
            if (XpProgress.Value < 1f)
                return;
            OnReachedTargetXp?.Invoke();
            CurrentXpStep.Value++;
            CollectedXp.Value = 0;
            XpProgress.Value = 0f;
        }

#endregion


#region PRIVATE_METHODS

#endregion
    }
}