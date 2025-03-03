using UnityEngine;
namespace QuickTools.Scripts.Extensions
{
    public enum VectorPlane
    {
        XY,
        XZ
    }

    public static class Vector3Extensions
    {
        public static Vector2 ToVector2(this Vector3 v, VectorPlane plane)
        {
            return new Vector2(v.x, plane == VectorPlane.XY ? v.y : v.z);
        }

        public static Vector3 WithX(this Vector3 v, float x)
        {
            return new Vector3(x, v.y, v.z);
        }

        public static Vector3 WithY(this Vector3 v, float y)
        {
            return new Vector3(v.x, y, v.z);
        }

        public static Vector3 WithZ(this Vector3 v, float z)
        {
            return new Vector3(v.x, v.y, z);
        }

        public static Vector3 WithAddedX(this Vector3 v, float x)
        {
            return v + new Vector3(x, 0f, 0f);
        }

        public static Vector3 WithAddedY(this Vector3 v, float y)
        {
            return v + new Vector3(0f, y, 0f);
        }

        public static Vector3 WithAddedZ(this Vector3 v, float z)
        {
            return v + new Vector3(0f, 0f, z);
        }

        public static Vector3 DirectionTo(this Vector3 v, Vector3 target)
        {
            return (target - v).normalized;
        }

        public static Vector3 FromThisTo(this Vector3 v, Vector3 target)
        {
            return target - v;
        }
    }
}