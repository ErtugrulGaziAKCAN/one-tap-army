using Obvious.Soap;
using UnityEngine;
using UnityEngine.Events;
namespace QuickTools.Scripts.Utilities
{
    public class KeyboardEventRaiser : MonoBehaviour
    {
//-------Public Variables-------//


//------Serialized Fields-------//
        [SerializeField] private KeyCode Key;
        [SerializeField] private ScriptableEventNoParam Event;
        [SerializeField] private UnityEvent UnityEvent;

//------Private Variables-------//


#region UNITY_METHODS

#if UNITY_EDITOR
        private void Update()
        {
            if (!Input.GetKeyDown(Key))
                return;

            if (Event != null)
                Event.Raise();
            UnityEvent?.Invoke();
        }
#endif

#endregion


#region PUBLIC_METHODS

#endregion


#region PRIVATE_METHODS

#endregion
    }
}