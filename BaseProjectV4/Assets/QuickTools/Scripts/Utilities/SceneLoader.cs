using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
namespace QuickTools.Scripts.Utilities
{
    [AddComponentMenu("QuickTools/Utilities/Scene Loader")]
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] private int SceneIndex = 2;
        private Button _button;

        public void LoadScene()
        {
            SceneManager.LoadScene(SceneIndex);
        }
    }
}