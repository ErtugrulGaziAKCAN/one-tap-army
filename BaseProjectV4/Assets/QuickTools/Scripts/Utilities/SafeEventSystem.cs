using UnityEngine;
using UnityEngine.EventSystems;
namespace QuickTools.Scripts.Utilities
{
    public class SafeEventSystem : MonoBehaviour
    {
//-------Public Variables-------//


//------Serialized Fields-------//


//------Private Variables-------//



#region UNITY_METHODS

        private void Awake()
        {
            var thisSystem = GetComponent<EventSystem>();
            var eventSystems = FindObjectsOfType<EventSystem>();
            foreach (var system in eventSystems)
            {
                if(system != thisSystem)
                    Destroy(gameObject);
            }
        }

#endregion


#region PUBLIC_METHODS

#endregion


#region PRIVATE_METHODS

#endregion

        
    }
}