#if UNITY_EDITOR
using System.Runtime.CompilerServices;
using UnityEngine;
namespace QuickTools.Scripts.Utilities.RaycastVisualization.Shapes {
  internal struct Ray {
    public Vector3 from;
    public Vector3 direction;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Draw(Color color) {
      Debug.DrawRay(from, direction, color, 0, true);
    }
  }
}
#endif