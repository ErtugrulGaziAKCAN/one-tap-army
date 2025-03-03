using UnityEditor;
using UnityEngine;
namespace QuickTools.Scripts.Utilities
{
    [ExecuteInEditMode]
    public class SceneVisibilityHandler : MonoBehaviour
    {
//-------Public Variables-------//


//------Serialized Fields-------//
        [SerializeField] private bool DisablePicking = true;
        [SerializeField] private bool HideInScene;

//------Private Variables-------//


#region UNITY_METHODS

        private void Update()
        {
#if UNITY_EDITOR
            if (Application.isPlaying)
                return;
            UpdateStatus();
#endif
        }

#endregion


#region PUBLIC_METHODS

#endregion


#region PRIVATE_METHODS

        private void UpdateStatus()
        {
#if UNITY_EDITOR
            if (DisablePicking)
                SceneVisibilityManager.instance.DisablePicking(gameObject, true);
            else
                SceneVisibilityManager.instance.EnablePicking(gameObject, true);

            if (HideInScene)
                SceneVisibilityManager.instance.Hide(gameObject, true);
            else
                SceneVisibilityManager.instance.Show(gameObject, true);
#endif
        }

#endregion
    }
}