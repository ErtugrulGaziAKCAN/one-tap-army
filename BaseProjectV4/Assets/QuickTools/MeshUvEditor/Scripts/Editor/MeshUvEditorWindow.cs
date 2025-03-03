using System.Collections.Generic;
using QuickTools.Editor;
using QuickTools.Scripts.Utilities;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;
namespace QuickTools.MeshUvEditor.Scripts.Editor
{
   public class MeshUvEditorWindow : OdinEditorWindow
   {
//-------Public Variables-------//


//------Serialized Fields-------//
      [OnValueChanged("CreateMeshData")] public MeshFilter TargetMesh;

      [OnValueChanged("UpdateMeshData"), BoxGroup("Config", ShowLabel = false, CenterLabel = true),
       HorizontalGroup("Config/CellCount"), LabelWidth(150)]
      public int CellCountOnX = 4;

      [OnValueChanged("UpdateMeshData"), BoxGroup("Config", ShowLabel = false, CenterLabel = true),
       HorizontalGroup("Config/CellCount"), LabelWidth(150)]
      public int CellCountOnY = 4;

      [OnValueChanged("UpdateMeshData"), BoxGroup("Config", ShowLabel = false, CenterLabel = true), LabelWidth(150)]
      public int SubCellCountOnX = 4;

      [PropertySpace(600f)]
      [ListDrawerSettings(Expanded = true, HideRemoveButton = true, HideAddButton = true, DraggableItems = false)]
      [HideInInspector]
      public List<UvIsland> UvIslands;

//------Private Variables-------//
      private int _selectedIslandIndex = 0;
      private float _highestUvNormalizedYInCell;
      private float _lowestUvNormalizedYInCell;
      private float _brightness;
      private float _gradient;
      private MeshUvEditor _uvEditor;
      private UvGridDrawer _uvGridDrawer;
      private const float DRAW_AREA_SIZE = 300f;
      private const float TOP_PADDING = 120f;
      private const float SLIDER_AREA_HEIGHT = 60f;
      private float _elapsedReadInputTime;


#region UNITY_METHODS

      [MenuItem("QuickTools/Mesh UV Editor")]
      private static void OpenWindow()
      {
         var window = GetWindow<MeshUvEditorWindow>();
         window.titleContent = new GUIContent("Mesh UV Editor");
         window.Show();
      }

      protected override void OnEnable()
      {
         base.OnEnable();
         _uvGridDrawer = new UvGridDrawer(this);
         UvIslands = new List<UvIsland>();
      }


      protected override void OnGUI()
      {
         base.OnGUI();
         if (TargetMesh == null)
         {
            QuickToolsEditorHelper.DrawHelpBox("Please select a mesh filter to edit its UVs.", MessageType.Info);
            return;
         }

         if (_uvEditor == null)
         {
            CreateMeshData();
            UpdateMeshData();
            UpdateNormalizedY();
            UpdateGradient();
            UpdateBrightness();
            return;
         }

         var visualRect = GetUvVisualRect();
         DrawTexture(visualRect);
         _uvGridDrawer.DrawGrid(visualRect);
         DrawUvIslands(visualRect);
         DrawIslandsList();
         ShiftInputHolder();

         UpdateNormalizedY();
         DrawGradientSlider();
         DrawBrightnessSlider();
      }

#endregion


#region PUBLIC_METHODS

#endregion


#region PRIVATE_METHODS

      private void DrawGradientSlider()
      {
         var drawBeginY = DRAW_AREA_SIZE + TOP_PADDING + 30f + SLIDER_AREA_HEIGHT / 2f;
         var centeredX = position.width / 2f;
         var sliderRect = new Rect(centeredX - 100f, drawBeginY, 200f, SLIDER_AREA_HEIGHT / 2f);
         var labelRect = new Rect(centeredX - 100f, drawBeginY - 20f, 200f, SLIDER_AREA_HEIGHT / 2f);
         EditorGUI.LabelField(labelRect, "Gradient");


         const float minDistanceToEdges = 0.05f;
         _gradient = EditorGUI.Slider(sliderRect, _gradient, minDistanceToEdges, 1 - minDistanceToEdges);

         _uvEditor.SetGradient(_gradient, _selectedIslandIndex, _lowestUvNormalizedYInCell,
            _highestUvNormalizedYInCell);
      }

      private void DrawBrightnessSlider()
      {
         var drawBeginY = DRAW_AREA_SIZE + TOP_PADDING + 30f;
         var centeredX = position.width / 2f;
         var sliderRect = new Rect(centeredX - 100f, drawBeginY, 200f, SLIDER_AREA_HEIGHT / 2f);
         var labelRect = new Rect(centeredX - 100f, drawBeginY - 20f, 200f, SLIDER_AREA_HEIGHT / 2f);
         EditorGUI.LabelField(labelRect, "Brightness");

         var leftValue = _gradient / 2f;
         var rightValue = 1f - leftValue;

         _brightness = Mathf.Clamp(_brightness, leftValue, rightValue);

         _brightness = EditorGUI.Slider(sliderRect, _brightness, leftValue, rightValue);
         _uvEditor.SetBrightness(_brightness, _selectedIslandIndex, _lowestUvNormalizedYInCell,
            _highestUvNormalizedYInCell);
      }

      private void UpdateNormalizedY()
      {
         var island = UvIslands[_selectedIslandIndex];
         var highestUv = island.GetHighestUv();
         var lowestUv = island.GetLowestUv();

         var cellYRange = _uvEditor.FindYRangeContainsUvs(island.UVList);
         _highestUvNormalizedYInCell = Mathf.InverseLerp(cellYRange.x, cellYRange.y, highestUv.Pos.y);
         _lowestUvNormalizedYInCell = Mathf.InverseLerp(cellYRange.x, cellYRange.y, lowestUv.Pos.y);
      }

      private void ShiftInputHolder()
      {
         var current = Event.current;
         if (current == null || current.type != EventType.KeyDown || !current.isKey)
            return;
         switch (current.keyCode)
         {
            case KeyCode.LeftArrow:
            case KeyCode.A:
               UvIslands[_selectedIslandIndex].ShiftLeft();
               break;
            case KeyCode.RightArrow:
            case KeyCode.D:
               UvIslands[_selectedIslandIndex].ShiftRight();
               break;
            case KeyCode.UpArrow:
            case KeyCode.W:
               UvIslands[_selectedIslandIndex].ShiftUp();
               break;
            case KeyCode.DownArrow:
            case KeyCode.S:
               UvIslands[_selectedIslandIndex].ShiftDown();
               break;
         }

         Repaint();
      }

      private void DrawIslandsList()
      {
         var beginY = DRAW_AREA_SIZE + TOP_PADDING + SLIDER_AREA_HEIGHT + 35f;
         var uvIslands = _uvEditor.UVIslands;
         for (int i = 0; i < uvIslands.Count; i++)
         {
            DrawIslandData(i, uvIslands[i], beginY);
         }
      }

      private void DrawIslandData(int i, UvIsland uvIsland, float beginY)
      {
         var windowX = position.width;
         var colorX = windowX * 0.7f;

         var islandIndexRect = new Rect(windowX / 2f - (colorX / 2f), beginY + i * 30f, colorX, 20f);
         if (_selectedIslandIndex == i)
         {
            var selectedRect = new Rect(islandIndexRect.x - 3f, islandIndexRect.y - 3f, islandIndexRect.width + 6f,
               islandIndexRect.height + 6f);
            var borderColor = QuickColors.Green;
            if (uvIsland.IslandColor.g > 0.8f)
               borderColor = QuickColors.Red;

            EditorGUI.DrawRect(selectedRect, borderColor);
         }

         EditorGUI.DrawRect(islandIndexRect, uvIsland.IslandColor);
         var labelStyle = new GUIStyle(GUI.skin.label)
         {
            alignment = TextAnchor.MiddleCenter,
            fontStyle = FontStyle.Bold,
            normal = {textColor = Color.white},
            fontSize = 15,
         };
         EditorGUI.LabelField(islandIndexRect, uvIsland.Index.ToString(), labelStyle);

         if (GUI.Button(islandIndexRect, "", GUIStyle.none))
         {
            _selectedIslandIndex = i;
            UpdateNormalizedY();
            UpdateGradient();
            UpdateBrightness();
         }
      }

      private void UpdateBrightness()
      {
         _brightness = _uvEditor.GetBrightness(_lowestUvNormalizedYInCell, _highestUvNormalizedYInCell);
      }

      private void UpdateGradient()
      {
         _gradient = _uvEditor.GetGradient(_lowestUvNormalizedYInCell, _highestUvNormalizedYInCell);
      }

      private void DrawUvIslands(Rect visualRect)
      {
         var uvIslands = _uvEditor.UVIslands;
         var uvIslandCount = uvIslands.Count;

         for (var i = 0; i < uvIslandCount; i++)
         {
            var uvIsland = uvIslands[i];
            var uvList = uvIsland.UVList;
            var uvCount = uvList.Count;

            UvVertex maxYVertex = null;
            UvVertex minYVertex = null;

            for (var j = 0; j < uvCount; j++)
            {
               var uvVertex = uvList[j];
               if (maxYVertex == null || uvVertex.Pos.y > maxYVertex.Pos.y)
                  maxYVertex = uvVertex;

               if (minYVertex == null || uvVertex.Pos.y < minYVertex.Pos.y)
                  minYVertex = uvVertex;
            }

            const float rectWidth = 5f;
            const float rectHeight = 5f;
            var color = FindContrastColor(uvIsland.IslandColor);

            var topRect = new Rect(
               maxYVertex.Pos.x * visualRect.width - (rectWidth / 2f)
               , (maxYVertex.Pos.y * visualRect.height) - (rectHeight / 2f)
               , rectWidth, rectHeight);
            topRect.y = visualRect.height - topRect.y - topRect.height;
            topRect.position += visualRect.position;
            EditorGUI.DrawRect(topRect, color);


            var bottomRect = new Rect(
               minYVertex.Pos.x * visualRect.width - (rectWidth / 2f)
               , (minYVertex.Pos.y * visualRect.height) - (rectHeight / 2f)
               , rectWidth, rectHeight);
            bottomRect.y = visualRect.height - bottomRect.y - bottomRect.height;
            bottomRect.position += visualRect.position;
            EditorGUI.DrawRect(bottomRect, color);

            Handles.color = color;
            Handles.DrawLine(topRect.center, bottomRect.center);
         }
      }

      private static Color FindContrastColor(Color color)
      {
         var hue = color.r;
         if (color.g > hue)
            hue = color.g;

         if (color.b > hue)
            hue = color.b;

         var contrastColor = Color.black;
         if (hue < 0.5f)
            contrastColor = Color.white;

         return contrastColor;
      }

      private void DrawTexture(Rect visualRect)
      {
         var texture = _uvEditor.GetTexture();
         GUI.DrawTexture(visualRect, texture, ScaleMode.ScaleToFit, true);
      }

      private Rect GetUvVisualRect()
      {
         var rect = new Rect(0, 0, DRAW_AREA_SIZE, DRAW_AREA_SIZE);
         var leftRightPadding = (position.width - rect.width) / 2f;
         rect.x = leftRightPadding;
         rect.y = TOP_PADDING;
         return rect;
      }

      private void CreateMeshData()
      {
         if (TargetMesh == null)
            return;
         _uvEditor = new MeshUvEditor(TargetMesh, CellCountOnX, SubCellCountOnX, CellCountOnY);
         UvIslands = _uvEditor.UVIslands;
      }

      private void UpdateMeshData()
      {
         if (_uvEditor == null)
            return;
         _uvEditor.UpdateMeshData(CellCountOnX, SubCellCountOnX, CellCountOnY);
         UvIslands = _uvEditor.UVIslands;
      }

#endregion
   }
}