using UnityEngine;
namespace QuickTools.Scripts.Utilities
{
    public static class EditorDebug
    {
        public static void Log(object obj)
        {
#if UNITY_EDITOR
            Debug.Log(obj);
#endif
        }

        public static void Log(object obj, GameObject gObj)
        {
#if UNITY_EDITOR
            Debug.Log(obj, gObj);
#endif
        }

        public static void LogWarning(object obj)
        {
#if UNITY_EDITOR
            Debug.LogWarning(obj);
#endif
        }

        public static void LogWarning(object obj, GameObject gObj)
        {
#if UNITY_EDITOR
            Debug.LogWarning(obj, gObj);
#endif
        }

        public static void LogError(object obj)
        {
#if UNITY_EDITOR
            Debug.LogError(obj);
#endif
        }

        public static void LogError(object obj, GameObject gObj)
        {
#if UNITY_EDITOR
            Debug.LogError(obj, gObj);
#endif
        }
    }
}