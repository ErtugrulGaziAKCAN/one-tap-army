using UnityEngine;
namespace QuickTools.Scripts.Utilities
{
    public class FpsLimiter : MonoBehaviour
    {
//-------Public Variables-------//


//------Serialized Fields-------//

//------Private Variables-------//


#region UNITY_METHODS

        private void Awake()
        {
#if UNITY_EDITOR
            Application.targetFrameRate = 120;
#elif UNITY_ANDROID
            Application.targetFrameRate = 30;
#else
            Application.targetFrameRate = 60;

#endif
        }

#endregion


#region PUBLIC_METHODS

#endregion


#region PRIVATE_METHODS

#endregion
    }
}