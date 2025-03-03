using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
namespace QuickTools.QuickUpgradables.Scripts
{
    [Serializable]
    public class IncrementalValue
    {
        public enum IncrementType
        {
            ManualEntry,
            Linear,
            Exponential
        }

        [EnumToggleButtons] public IncrementType Type;

        [ShowIf("@Type==IncrementType.ManualEntry")] public List<float> ManualValues = new List<float>();

        [LabelText("Base Value"), ShowIf("@Type==IncrementType.Linear")]
        public float BaseValueLinear;

        [LabelText("Increment"), ShowIf("@Type==IncrementType.Linear")]
        public float IncrementLinear;

        [LabelText("Base Value"), ShowIf("@Type==IncrementType.Exponential")]
        public float BaseValueExpo;

        [LabelText("Increment"), ShowIf("@Type==IncrementType.Exponential")]
        public float IncrementExpo;


        public float GetValueOnLevel(int level)
        {
            return Type switch
            {
                IncrementType.ManualEntry => ManualValues.Count > 0
                    ? ManualValues[Mathf.Clamp(level - 1, 0, ManualValues.Count - 1)]
                    : 0,
                IncrementType.Linear => BaseValueLinear + IncrementLinear * (level - 1),
                IncrementType.Exponential => BaseValueExpo * level * Mathf.Pow(IncrementExpo, level - 1),
                _ => 0
            };
        }
    }
}