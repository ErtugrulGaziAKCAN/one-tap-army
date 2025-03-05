using UnityEngine;
namespace QuickTools.Scripts.ColliderSystem
{
    public interface ICollidable
    {
        void OnCollide(GameObject collidedObject);
    }
}