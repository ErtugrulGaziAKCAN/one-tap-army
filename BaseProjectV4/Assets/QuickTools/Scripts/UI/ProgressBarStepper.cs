using Nova;
using Sirenix.OdinInspector;
using UI;
using UnityEditor;
using UnityEngine;
namespace QuickTools.Scripts.UI
{
    public class ProgressBarStepper : ProgressBarController
    {
//-------Public Variables-------//


//------Serialized Fields-------//
        [SerializeField, Range(1, 20), OnValueChanged(nameof(CreateSteps))]
        private int MaxSteps = 5;

//------Private Variables-------//
        [SerializeField, FoldoutGroup("References")]
        private UIBlock StepperPrefab;

        [SerializeField, FoldoutGroup("References")]
        private UIBlock StepperContainer;

#region UNITY_METHODS

#endregion


#region PUBLIC_METHODS

        public void SetSteps(int steps)
        {
            if (steps > MaxSteps)
                steps = MaxSteps;
            Progress = (float)steps / MaxSteps;
        }

        public void SetMaxSteps(int steps)
        {
            MaxSteps = steps;
            CreateSteps();
        }

#endregion


#region PRIVATE_METHODS

        private void CreateSteps()
        {
            DestroyOldSteps();
            for (var i = 0; i < MaxSteps + 1; i++)
            {
                UIBlock stepper;
#if UNITY_EDITOR
                stepper = PrefabUtility.InstantiatePrefab(StepperPrefab, StepperContainer.transform) as UIBlock;
#else
                stepper = Instantiate(StepperPrefab, StepperContainer.transform);
#endif
                if (stepper != null)
                {
                    stepper.Visible = i != 0 && i != MaxSteps;
                }
            }
        }

        private void DestroyOldSteps()
        {
            var count = StepperContainer.transform.childCount;
            for (var i = count - 1; i >= 0; i--)
            {
#if UNITY_EDITOR
                DestroyImmediate(StepperContainer.transform.GetChild(i).gameObject);
#else
                Destroy(StepperContainer.transform.GetChild(i).gameObject);
#endif
            }
        }

#endregion
    }
}