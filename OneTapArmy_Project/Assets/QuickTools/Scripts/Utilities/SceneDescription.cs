using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace QuickTools.Scripts.Utilities
{
    public class SceneDescription : MonoBehaviour
    {
#if UNITY_EDITOR

//-------Public Variables-------//


//------Serialized Fields-------//
        [SerializeField, TextArea] private string Description;
        [SerializeField] private SceneAsset TargetScene;

        //------Private Variables-------//
        private bool _isNotificationShown;

#region UNITY_METHODS

#endregion


#region PUBLIC_METHODS

#endregion


#region PRIVATE_METHODS

        private SceneDescription()
        {
            UnityEditor.SceneManagement.EditorSceneManager.sceneOpened += OnSceneOpened;
        }

        private void OnSceneOpened(UnityEngine.SceneManagement.Scene scene,
            UnityEditor.SceneManagement.OpenSceneMode mode)
        {
#if UNITY_EDITOR
            if (_isNotificationShown)
                return;
            if (TargetScene.name == SceneManager.GetActiveScene().name)
            {
                _isNotificationShown = true;
                EditorUtility.DisplayDialog(
                    "Scene Description", Description,
                    "Okay"
                );
            }
#endif
        }

#endregion
#endif
    }
}