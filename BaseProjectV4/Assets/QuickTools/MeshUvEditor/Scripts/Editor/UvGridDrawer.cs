using QuickTools.Scripts.Extensions;
using QuickTools.Scripts.Utilities;
using UnityEditor;
using UnityEngine;
namespace QuickTools.MeshUvEditor.Scripts.Editor
{
   public class UvGridDrawer
   {
      private readonly MeshUvEditorWindow _meshUvEditorWindow;

      public UvGridDrawer(MeshUvEditorWindow meshUvEditorWindow)
      {
         _meshUvEditorWindow = meshUvEditorWindow;
      }

      public void DrawGrid(Rect visualRect)
      {
         var cellSizeX = visualRect.width / _meshUvEditorWindow.CellCountOnX;
         var cellSizeY = visualRect.height / _meshUvEditorWindow.CellCountOnY;
         var subCellSizeY = cellSizeY / _meshUvEditorWindow.SubCellCountOnX;

         var zeroLabelRect = new Rect(visualRect.x - 18f, visualRect.y - 20f, 20f, 20f);
         GUI.Label(zeroLabelRect, "0", EditorStyles.boldLabel);

         for (var i = 0; i <= _meshUvEditorWindow.CellCountOnX; i++)
         {
            var begin = new Vector3(visualRect.x + i * cellSizeX, visualRect.y);
            var end = new Vector3(visualRect.x + i * cellSizeX, visualRect.y + visualRect.height);
            DrawCellLine(begin, end, i, new Vector2(-5f, -15f));
         }

         for (var i = 0; i <= _meshUvEditorWindow.CellCountOnY; i++)
         {
            var begin = new Vector3(visualRect.x, visualRect.y + i * cellSizeY);
            var end = new Vector3(visualRect.x + visualRect.width, visualRect.y + i * cellSizeY);
            DrawCellLine(begin, end, i, new Vector2(-15f, -5f));
         }

         for (var i = 0; i <= _meshUvEditorWindow.CellCountOnX - 1; i++)
         {
            for (var j = 0; j <= _meshUvEditorWindow.SubCellCountOnX; j++)
            {
               var begin = new Vector3(visualRect.x + i * cellSizeX + j * subCellSizeY, visualRect.y);
               var end = new Vector3(visualRect.x + i * cellSizeX + j * subCellSizeY, visualRect.y + visualRect.height);
               DrawSubCellLine(begin, end);
            }
         }
      }

      private void DrawSubCellLine(Vector3 begin, Vector3 end)
      {
         var color = QuickColors.DarkGrey;
         Handles.color = color;
         Handles.DrawLine(begin, end);
      }

      private static void DrawCellLine(Vector3 begin, Vector3 end, int i, Vector2 labelOffset)
      {
         Handles.color = QuickColors.LightGrey;
         Handles.DrawLine(begin, end);

         if (i == 0)
            return;
         var labelPosition = begin.WithAddedX(labelOffset.x).WithAddedY(labelOffset.y);
         Handles.Label(labelPosition, i.ToString(), EditorStyles.boldLabel);
      }
   }
}