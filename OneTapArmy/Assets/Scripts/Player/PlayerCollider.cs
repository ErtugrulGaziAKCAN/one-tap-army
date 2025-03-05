using QuickTools.Scripts.ColliderSystem;
using UnityEngine;

namespace Player
{
    public class PlayerCollider : MonoBehaviour
    {
//-------Public Variables-------//


//------Serialized Fields-------//


//------Private Variables-------//


#region UNITY_METHODS

        private void OnTriggerEnter(Collider other)
        {
            other.TryGetComponent(out ICollidable interactable);
            interactable?.OnCollide(gameObject);
        }

        private void OnCollisionEnter(Collision other)
        {
            other.collider.TryGetComponent(out ICollidable interactable);
            interactable?.OnCollide(gameObject);
        }

#endregion


#region PUBLIC_METHODS

#endregion


#region PRIVATE_METHODS

#endregion
    }
}