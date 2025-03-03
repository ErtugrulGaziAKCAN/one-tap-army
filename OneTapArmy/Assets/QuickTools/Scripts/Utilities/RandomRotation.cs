using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;


namespace QuickTools.Scripts.Utilities
{
    public class RandomRotation : MonoBehaviour
    {
        /****************PUBLIC_VARIABLES****************/


        /**************SERIALIZED_VARIABLES**************/
        [SerializeField, HorizontalGroup("Axis", Title = "Rotate On", LabelWidth = 15, MaxWidth = .1f)]
        private bool X;

        [SerializeField, HorizontalGroup("Axis", LabelWidth = 15, MaxWidth = .1f)]
        private bool Y;

        [SerializeField, HorizontalGroup("Axis", LabelWidth = 15, MaxWidth = .1f)]
        private bool Z;
        
        [SerializeField] private bool RandomizeOnEnable;
        [SerializeField] private int Sensitivity = 90;

        /***************PRIVATE_VARIABLES****************/


#region UNITY_METHODS

        private void OnEnable()
        {
            if (!RandomizeOnEnable)
                return;
            Randomize();
        }

#endregion


#region PUBLIC_METHODS

        [Button]
        public void Randomize()
        {
            var rotation = transform.rotation;
            var newRot = new Vector3(
                X
                    ? Sensitivity * Mathf.FloorToInt(Random.Range(0f, 360f) / Sensitivity)
                    : rotation.eulerAngles.x,
                Y
                    ? Sensitivity * Mathf.FloorToInt(Random.Range(0f, 360f) / Sensitivity)
                    : rotation.eulerAngles.y,
                Z
                    ? Sensitivity * Mathf.FloorToInt(Random.Range(0f, 360f) / Sensitivity)
                    : rotation.eulerAngles.z);
            rotation = Quaternion.Euler(newRot);
            transform.rotation = rotation;
        }

#endregion


#region PRIVATE_METHODS

#endregion
    }
}