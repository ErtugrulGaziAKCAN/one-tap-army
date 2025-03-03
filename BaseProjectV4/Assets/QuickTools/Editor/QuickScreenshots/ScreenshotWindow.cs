using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using Unity.EditorCoroutines.Editor;
using UnityEditor;
using UnityEngine;
namespace QuickTools.Editor.QuickScreenshots
{
    public class ScreenshotWindow : OdinEditorWindow
    {
//-------Public Variables-------//

//------Serialized Fields-------//

//------Private Variables-------//
        private static bool _isTaking;
        private static int _customSizeIndex;
        private const string TEMPORARY_RESOLUTION_LABEL = "TEMP";

        private static object SizeHolder =>
            GetType("GameViewSizes").FetchProperty("instance").FetchProperty("currentGroup");

        private static EditorWindow GameView => GetWindow(GetType("GameView"));
        private static string _rootFolder => Path.GetDirectoryName(Application.dataPath);
        private static int _originalSizeIndex;

        private static List<ScreenshotResolution> _resolutions = new List<ScreenshotResolution>()
        {
            new(1242, 2688, "iPhone 6.5"),
            new(1242, 2208, "iPhone 5.5"),
            new(2048, 2732, "iPad"),
        };


#region UNITY_METHODS

        private void Update()
        {
            if (_isTaking)
                return;
            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.S))
                TakeScreenshots();
        }

#endregion


#region PUBLIC_METHODS
        
        [Button("Take Screenshots (LeftCtrl+S)", ButtonSizes.Large),
        InfoBox("Make sure Unity Editor is running on fullscreen mode to prevent losing screenshot window's focus")]
        [InfoBox("Take 5 screenshots", InfoMessageType.Warning)]
        [InfoBox("Include different levels and environments", InfoMessageType.Warning)]
        public static void TakeScreenshots()
        {
            _isTaking = true;
            EditorCoroutineUtility.StartCoroutineOwnerless(ScreenshotCoroutine());
        }

        private static IEnumerator ScreenshotCoroutine()
        {
            _customSizeIndex = 0;
            _originalSizeIndex = (int)GameView.FetchProperty("selectedSizeIndex");
            var originalTimescale = Time.timeScale;
            Time.timeScale = 0;
            foreach (var resolution in _resolutions)
            {
                SetGameViewResolution(resolution);
                yield return new EditorWaitForSeconds(.1f);
                ScreenshotTaker.TakeScreenshot(resolution.Label);
                yield return new EditorWaitForSeconds(.1f);
                SizeHolder.CallMethod("RemoveCustomSize", _customSizeIndex);
            }

            Time.timeScale = originalTimescale;
            _isTaking = false;
            SetGameViewResolutionWithIndex(_originalSizeIndex);
        }

        [MenuItem("QuickTools/Screenshots")]
        public static void OpenWindow()
        {
            var window = GetWindow<ScreenshotWindow>();
            window.titleContent = new GUIContent("Screenshots", EditorIcons.ImageCollection.Active);
            window.maxSize = new Vector2(300, 300);
            window.minSize = window.maxSize;
            window.Show();
        }

        [Button(ButtonSizes.Large)]
        [PropertySpace(30)]
        public void OpenScreenshotsFolder()
        {
            EditorUtility.RevealInFinder(_rootFolder + "/Screenshots");
        }

#endregion


#region PRIVATE_METHODS

        private static void SetGameViewResolution(ScreenshotResolution resolution)
        {
            var customSize = GetFixedResolution(resolution.Width, resolution.Height);
            SizeHolder.CallMethod("AddCustomSize", customSize);
            _customSizeIndex = (int)SizeHolder.CallMethod("IndexOf", customSize) +
                               (int)SizeHolder.CallMethod("GetBuiltinCount");

            SetGameViewResolutionWithIndex(_customSizeIndex);
        }

        private static void SetGameViewResolutionWithIndex(int i)
        {
            GameView.CallMethod("SizeSelectionCallback", i, null);
            GameView.Repaint();
        }


        private static Type GetType(string type)
        {
            return typeof(EditorWindow).Assembly.GetType("UnityEditor." + type);
        }


        private static object GetFixedResolution(int width, int height)
        {
            object sizeType = Enum.Parse(GetType("GameViewSizeType"), "FixedResolution");
            return GetType("GameViewSize").CreateInstance(sizeType, width, height, TEMPORARY_RESOLUTION_LABEL);
        }
    }

#endregion
}