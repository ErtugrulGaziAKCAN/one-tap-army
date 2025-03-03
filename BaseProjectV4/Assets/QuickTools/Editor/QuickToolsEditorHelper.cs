using UnityEditor;
using UnityEngine;
namespace QuickTools.Editor
{
   public static class QuickToolsEditorHelper
   {
      public static void DrawHeader(string labelText)
      {
         EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
         EditorGUILayout.LabelField(labelText, EditorStyles.centeredGreyMiniLabel);
         EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
      }

      public static void DrawHelpBox(string text, MessageType type)
      {
         EditorGUILayout.HelpBox(text, type);
         EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
         GUILayout.FlexibleSpace();
      }
   }
}