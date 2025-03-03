using UnityEngine;
namespace QuickTools.Scripts.Extensions
{
    public static class TransformExtensions
    {
        public static void SetX(this Transform t, float x)
        {
            t.position = t.position.WithX(x);
        }
        
        public static void SetY(this Transform t, float y)
        {
            t.position = t.position.WithY(y);
        }
        
        public static void SetZ(this Transform t, float z)
        {
            t.position = t.position.WithZ(z);
        }
        
        public static void AddX(this Transform t, float x)
        {
            t.position = t.position.WithAddedX(x);
        }
        
        public static void AddY(this Transform t, float y)
        {
            t.position = t.position.WithAddedY(y);
        }
        
        public static void AddZ(this Transform t, float z)
        {
            t.position = t.position.WithAddedZ(z);
        }
    }
}