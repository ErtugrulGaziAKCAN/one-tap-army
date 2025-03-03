using UnityEngine;
namespace QuickTools.Scripts.Utilities.RaycastVisualization._2D {
  public static partial class VisualPhysics2D {
    internal static ContactFilter2D CreateLegacyFilter(
      int layerMask,
      float minDepth,
      float maxDepth)
    {
      ContactFilter2D legacyFilter = new ContactFilter2D();
      legacyFilter.useTriggers = Physics2D.queriesHitTriggers;
      legacyFilter.SetLayerMask((LayerMask) layerMask);
      legacyFilter.SetDepth(minDepth, maxDepth);
      return legacyFilter;
    }
  }
}