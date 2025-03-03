using System;
using Nova;
using Sirenix.OdinInspector;
using UnityEngine;
namespace UI
{
    public class PersistentInfoTextWriter : MonoBehaviour
    {
//-------Public Variables-------//


//------Serialized Fields-------//
        [SerializeField] private GameObject Visualize;
        [SerializeField] private TextBlock TargetText;
        [SerializeField] private PersistentInfoTextWriterVariable Variable;
        
//------Private Variables-------//


#region UNITY_METHODS

        private void Start()
        {
            Variable.Value = this;
        }

#endregion


#region PUBLIC_METHODS
        public void SetText(string message)
        {
            TargetText.Text = message;
            Visualize.SetActive(true);
        }
        
        public void SetPersistentTextActivity(bool isActive)
        {
            Visualize.SetActive(isActive);
        }

#endregion


#region PRIVATE_METHODS

#endregion
    }
}