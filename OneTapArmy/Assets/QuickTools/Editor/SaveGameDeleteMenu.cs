using Plugins.SaveGameFree.Scripts;
using UnityEditor;
using UnityEngine;
namespace QuickTools.Editor
{
    public class SaveGameDeleteMenu : MonoBehaviour
    {
#if UNITY_EDITOR
        [MenuItem("QuickTools/Delete Save Data &#D")]
        static void DeleteSaveData()
        {
            if (EditorUtility.DisplayDialog("Deleting Saved Data",
                "Are you sure you want to delete all saved game data",
                "Delete",
                "Cancel"))
            {
                SaveGame.DeleteAll();
                PlayerPrefs.DeleteAll();
            }
        }
#endif
    }
}