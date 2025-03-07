using UnityEngine;
namespace QuickTools.Scripts.Utilities
{
    [AddComponentMenu("QuickTools/Utilities/Look At Camera")]
    public class LookAtCamera : MonoBehaviour
    {
        /****************PUBLIC_VARIABLES****************/


        /**************SERIALIZED_VARIABLES**************/


        /***************PRIVATE_VARIABLES****************/

        private Camera _mainCamera;

#region UNITY_METHODS

        private void Start()
        {
            _mainCamera = Camera.main;
        }

        private void Update()
        {
            transform.rotation = _mainCamera.transform.rotation * Quaternion.Euler(0f, -1f, 0f);
        }

#endregion



#region PUBLIC_METHODS

#endregion



#region PRIVATE_METHODS

#endregion
    }
}
