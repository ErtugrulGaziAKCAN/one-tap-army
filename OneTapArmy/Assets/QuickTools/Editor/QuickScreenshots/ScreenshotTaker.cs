using System.IO;
using UnityEngine;
namespace QuickTools.Editor.QuickScreenshots
{
    public static class ScreenshotTaker
    {
//-------Public Variables-------//


//------Serialized Fields-------//


//------Private Variables-------//
        private static string _screenshotsFolder => Path.GetDirectoryName(Application.dataPath) + "/Screenshots";


#region UNITY_METHODS

#endregion


#region PUBLIC_METHODS

#endregion


#region PRIVATE_METHODS

        public static void TakeScreenshot(string label)
        {
            CheckDirectory();
            var fileName = GetFileName(label);
            ScreenCapture.CaptureScreenshot(fileName);
        }

        private static string GetFileName(string label)
        {
            var suffix = 1;
            var fileName = _screenshotsFolder + "/" + label + "_" + suffix + ".png";

            while (File.Exists(fileName))
            {
                suffix++;
                fileName = _screenshotsFolder + "/" + label + "_" + suffix + ".png";
            }

            return fileName;
        }

        private static void CheckDirectory()
        {
            if (!Directory.Exists(_screenshotsFolder))
            {
                Directory.CreateDirectory(_screenshotsFolder);
            }
        }

#endregion
    }
}