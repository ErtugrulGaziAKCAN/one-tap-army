using UnityEngine;
namespace QuickTools.Scripts.Extensions
{
    public static class LayerMaskExtensions
    {
        public static bool Contains(this LayerMask mask, int layer)
        {
            
            return ((1 << layer) & mask) != 0;
        }
    }
}