using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
namespace QuickTools.MeshUvEditor.Scripts.Editor
{
   public class UvIsland
   {
//-------Public Variables-------//
      [HideInInspector] public MeshUvEditor Editor;
      [HideInInspector] public int Index;
      [HideInInspector] public List<UvVertex> UVList = new List<UvVertex>();

      [ShowInInspector, ReadOnly, LabelWidth(100)]
      public Color IslandColor { get; private set; }


//------Serialized Fields-------//


//------Private Variables-------//


#region UNITY_METHODS

      public UvIsland()
      {
         UVList = new List<UvVertex>();
         IslandColor = Color.white;
      }

#endregion


#region PUBLIC_METHODS

      public void AddUv(UvVertex uv)
      {
         UVList.Add(uv);
         FindIslandColor();
      }

      public bool Contains(Vector2 pos)
      {
         return UVList.Exists(x => x.Pos == pos);
      }

#endregion


#region PRIVATE_METHODS

      public void ShiftUp()
      {
         Editor.Shift(Index, 0, 1);
      }

      public void ShiftLeft()
      {
         Editor.Shift(Index, -1, 0);
      }

      public void ShiftDown()
      {
         Editor.Shift(Index, 0, -1);
      }

      public void ShiftRight()
      {
         Editor.Shift(Index, 1, 0);
      }


      private void FindIslandColor()
      {
         Color color = Color.black;
         foreach (var uv in UVList)
         {
            color += uv.VertexColor;
         }

         color /= UVList.Count;
         IslandColor = color;
      }

#endregion

      public UvVertex GetHighestUv()
      {
         if (UVList != null)
         {
            if (UVList.Count > 0)
            {
               UvVertex highest = UVList[0];

               foreach (var uv in UVList)
               {
                  if (uv.Pos.y > highest.Pos.y)
                  {
                     highest = uv;
                  }
               }

               return highest;
            }
         }

         return null;
      }

      public UvVertex GetLowestUv()
      {
         UvVertex lowest = UVList[0];
         foreach (var uv in UVList)
         {
            if (uv.Pos.y < lowest.Pos.y)
            {
               lowest = uv;
            }
         }

         return lowest;
      }

      public bool IsInsideTexture()
      {
         foreach (var uv in UVList)
         {
            if (uv.Pos.x < 0 || uv.Pos.x > 1 || uv.Pos.y < 0 || uv.Pos.y > 1)
            {
               return false;
            }
         }

         return true;
      }
   }
}