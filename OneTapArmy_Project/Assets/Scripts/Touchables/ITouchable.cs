using UnityEngine;
namespace Touchables
{
    public interface ITouchable
    {
        public void OnTouched(Vector3 touchPoint);
    }
}