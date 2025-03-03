using System.Collections.Generic;
using System.Linq;
using QuickTools.Scripts.Utilities;
using UnityEditor;
using UnityEngine;
namespace QuickTools.MeshUvEditor.Scripts.Editor
{
   public class MeshUvEditor
   {
//-------Public Variables-------//


//------Serialized Fields-------//
      public List<UvIsland> UVIslands;
      public int IslandCount;

//------Private Variables-------//
      private const string TEXTURE_NAME = "QuickTools_Gradient";
      private Texture2D _texture;
      private int _cellCountOnX;
      private int _subCellCountOnX;
      private int _cellCountOnY;
      private readonly Mesh _mesh;


#region UNITY_METHODS

      public MeshUvEditor(MeshFilter filter, int cellCountOnX = 4, int cellCountOnY = 4, int subCellCountOnX = 4)
      {
         FindTexture();
         _mesh = filter.sharedMesh;
         _cellCountOnX = cellCountOnX;
         _cellCountOnY = cellCountOnY;
         _subCellCountOnX = subCellCountOnX;
         UVIslands = new List<UvIsland>();
         IslandCount = 0;
         FindIslands();
      }

      public Texture GetTexture() => _texture;

#endregion


#region PUBLIC_METHODS

      public void UpdateMeshData(int cellCountOnX, int subCellCountOnX, int cellCountOnY)
      {
         _cellCountOnX = cellCountOnX;
         _cellCountOnY = cellCountOnY;
         _subCellCountOnX = subCellCountOnX;
         FindIslands();
      }

      public void Shift(int islandIndex, int shiftX, int shiftY)
      {
         var island = UVIslands[islandIndex];
         var allUvs = _mesh.uv.ToList();
         var newUvs = new List<UvVertex>();
         foreach (var allUv in allUvs)
         {
            newUvs.Add(new UvVertex(new Vector2(allUv.x, allUv.y), GetColorFromUv(allUv)));
         }

         var newIsland = new UvIsland();

         for (var i = 0; i < allUvs.Count; i++)
         {
            var oldUv = allUvs[i];
            if (!island.Contains(oldUv))
               continue;

            var xShift = (float) shiftX / (_cellCountOnX * _subCellCountOnX);
            var yShift = (float) shiftY / _cellCountOnY;
            var newPos = new Vector2(oldUv.x + xShift, oldUv.y + yShift);
            var newUv = new UvVertex(newPos, GetColorFromUv(newPos));
            newUvs[i] = newUv;
            newIsland.Index = island.Index;
            newIsland.Editor = this;
            newIsland.AddUv(newUv);
         }

         var canShift = newIsland.IsInsideTexture();
         if (!canShift)
            return;

         UVIslands[islandIndex] = newIsland;
         _mesh.uv = newUvs.Select(u => u.Pos).ToArray();
      }

      public float GetBrightness(float lowestUvNormalizedYInCell, float highestUvNormalizedYInCell)
      {
         return (lowestUvNormalizedYInCell + highestUvNormalizedYInCell) / 2f;
      }

      public void SetBrightness(float normalizedBrightness, int islandIndex, float lowestNormYInCell,
         float highestNormYInCell)
      {
         var island = UVIslands[islandIndex];
         var center = (lowestNormYInCell + highestNormYInCell) / 2f;
         var range = FindYRangeContainsUvs(island.UVList);
         var globalCenter = range.x + center / _cellCountOnY;
         var asd = (1 - (highestNormYInCell - lowestNormYInCell)) / 2f;

         normalizedBrightness = Mathf.Clamp(normalizedBrightness, .5f - asd, .5f + asd);
         EditorDebug.Log($"norBri: {normalizedBrightness}, asd: {asd}, center: {center}");
         var intendedCenter = normalizedBrightness;

         var allUvs = _mesh.uv.ToList();
         var newUvs = new List<UvVertex>();
         foreach (var allUv in allUvs)
         {
            newUvs.Add(new UvVertex(new Vector2(allUv.x, allUv.y), GetColorFromUv(allUv)));
         }

         var newIsland = new UvIsland();

         EditorDebug.Log($"range: {range}");
         for (var i = 0; i < allUvs.Count; i++)
         {
            var oldUv = allUvs[i];
            if (!island.Contains(oldUv))
               continue;
            var oldY = oldUv.y;
            var distanceOfCenter = oldY - globalCenter;
            EditorDebug.Log($"oldY: {oldY}, distanceOfCenter: {distanceOfCenter}");

            var newPos = new Vector2(oldUv.x, range.x + (intendedCenter + distanceOfCenter) / _cellCountOnY);
            EditorDebug.Log($"newPos: {newPos}");
            
            var newUv = new UvVertex(newPos, GetColorFromUv(newPos));
            newUvs[i] = newUv;
            newIsland.Index = island.Index;
            newIsland.Editor = this;
            newIsland.AddUv(newUv);
         }

         UVIslands[islandIndex] = newIsland;
         _mesh.uv = newUvs.Select(u => u.Pos).ToArray();
      }

      public float GetGradient(float lowestNormYInCell, float highestNormYInCell)
      {
         return highestNormYInCell - lowestNormYInCell;
      }

      public void SetGradient(float normalizedGradient, int selectedIslandIndex, float lowestNormYInCell,
         float highestNormYInCell)
      {
         const float minDistanceToEdges = 0.01f;
         var island = UVIslands[selectedIslandIndex];
         var center = (lowestNormYInCell + highestNormYInCell) / 2f;
         var allUvs = _mesh.uv.ToList();
         var newUvs = allUvs.Select(allUv => new UvVertex(new Vector2(allUv.x, allUv.y),
            GetColorFromUv(allUv))).ToList();

         var newIsland = new UvIsland();
         var range = FindYRangeContainsUvs(island.UVList);

         normalizedGradient = Mathf.Clamp(normalizedGradient, minDistanceToEdges, 1f - minDistanceToEdges);

         for (var i = 0; i < allUvs.Count; i++)
         {
            var oldUv = allUvs[i];
            if (!island.Contains(oldUv))
               continue;

            var globalYOfLowestNormY = range.x + lowestNormYInCell * (range.y - range.x);
            var globalYOfHighestNormY = range.x + highestNormYInCell * (range.y - range.x);
            var normYInIsland = Mathf.InverseLerp(globalYOfLowestNormY, globalYOfHighestNormY, oldUv.y);

            var newTop = center + normalizedGradient / 2f;
            var newBottom = center - normalizedGradient / 2f;

            var newGlobalTop = range.x + newTop * (range.y - range.x);
            var newGlobalBottom = range.x + newBottom * (range.y - range.x);

            var newGlobalY = Mathf.Lerp(newGlobalBottom, newGlobalTop, normYInIsland);
            newGlobalY = Mathf.Clamp(newGlobalY, range.x + minDistanceToEdges, range.y - minDistanceToEdges);
            var newPos = new Vector2(oldUv.x, newGlobalY);

            var newUv = new UvVertex(newPos, GetColorFromUv(newPos));
            newUvs[i] = newUv;
            newIsland.Index = island.Index;
            newIsland.Editor = this;
            newIsland.AddUv(newUv);
         }

         UVIslands[selectedIslandIndex] = newIsland;
         _mesh.uv = newUvs.Select(u => u.Pos).ToArray();
      }

      public float Remap(float value, float from1, float to1, float from2, float to2)
      {
         return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
      }

      public Vector2 FindYRangeContainsUvs(List<UvVertex> uvs)
      {
         var minY = uvs.Min(uv => uv.Pos.y);
         var maxY = uvs.Max(uv => uv.Pos.y);

         for (int i = 0; i < _cellCountOnY; i++)
         {
            var yRange = new Vector2(i / (float) _cellCountOnY, (i + 1) / (float) _cellCountOnY);
            if (minY >= yRange.x && maxY <= yRange.y)
            {
               return yRange;
            }
         }

         return Vector2.zero;
      }

#endregion


#region PRIVATE_METHODS

      private void FindIslands()
      {
         UVIslands = new List<UvIsland>();


         for (var i = 0; i < _cellCountOnX * _subCellCountOnX; i++)
         {
            for (var j = 0; j < _cellCountOnY; j++)
            {
               Vector2 xRange = new Vector2(i / (float) (_cellCountOnX * _subCellCountOnX),
                  (i + 1) / (float) (_cellCountOnX * _subCellCountOnX));
               Vector2 yRange = new Vector2(j / (float) _cellCountOnY, (j + 1) / (float) _cellCountOnY);

               var islandUvs = FindUvsInRange(xRange, yRange);
               if (islandUvs.Count <= 0)
                  continue;
               var uvIsland = new UvIsland();
               foreach (var uv in islandUvs)
               {
                  uvIsland.AddUv(uv);
               }

               uvIsland.Editor = this;
               uvIsland.Index = UVIslands.Count;
               UVIslands.Add(uvIsland);
            }
         }

         IslandCount = UVIslands.Count;
      }

      private List<UvVertex> FindUvsInRange(Vector2 xRange, Vector2 yRange)
      {
         var uvs = _mesh.uv;
         var suitableUvs = new List<UvVertex>();

         foreach (var uv in uvs)
         {
            if (uv.x >= xRange.x && uv.x < xRange.y && uv.y >= yRange.x && uv.y < yRange.y)
            {
               suitableUvs.Add(new UvVertex(uv, GetColorFromUv(uv)));
            }
         }

         return suitableUvs;
      }

      private Color GetColorFromUv(Vector2 pos)
      {
         return _texture.GetPixel((int) (pos.x * _texture.width), (int) (pos.y * _texture.height));
      }

      private void FindTexture()
      {
         var guids = AssetDatabase.FindAssets(TEXTURE_NAME);
         if (guids.Length <= 0)
         {
            Debug.LogError("Texture not found");
            return;
         }

         var path = AssetDatabase.GUIDToAssetPath(guids[0]);
         _texture = AssetDatabase.LoadAssetAtPath<Texture2D>(path);
      }

#endregion
   }
}