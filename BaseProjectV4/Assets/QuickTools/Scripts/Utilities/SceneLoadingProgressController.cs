using System.Collections;
using Nova;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
namespace Utilities
{
    public class SceneLoadingProgressController : MonoBehaviour
    {
        public ProgressBarController LoadingBar;

        public void LoadLevel(int levelIndex)
        {
            StartCoroutine(LoadSceneAsync(levelIndex));
        }

        IEnumerator LoadSceneAsync(int levelIndex)
        {
            AsyncOperation op = SceneManager.LoadSceneAsync(levelIndex);
            while (!op.isDone)
            {
                float progress = Mathf.Clamp01(op.progress / .9f);
                LoadingBar.Progress = progress;
                yield return null;
            }
        }
    }
}