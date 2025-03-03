using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace QuickTools.Scripts.Utilities
{
    public class RandomChildOnEnable : MonoBehaviour
    {
        /**PUBLIC_VARIABLES**/


        /**SERIALIZED_VARIABLES**/
#pragma warning disable 0649
    
#pragma warning restore 0649
        /***PRIVATE_VARIABLES**/


#region UNITY_METHODS
        private void OnEnable()
        {
            ActivateRandomChild();
        }
        
#endregion



#region PUBLIC_METHODS

#endregion



#region PRIVATE_METHODS
        [Button]
        private void ActivateRandomChild()
        {
            var rand = Random.Range(0, transform.childCount);
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(child.GetSiblingIndex() == rand);
            }
        }
#endregion
    }
}