using Obvious.Soap;
using Plugins.CW.LeanPool.Required.Scripts;
using QuickTools.Scripts.ColliderSystem;
using QuickTools.Scripts.Extensions;
using UnityEngine;
namespace QuickTools.Scripts.Obstacles.Core
{
    public abstract class ObstacleCore : MonoBehaviour, ICollidable
    {
//-------Public Variables-------//


//------Serialized Fields-------//
        [SerializeField] private ParticleSystem HitFx;
        [SerializeField] private ScriptableEventNoParam HitEvent;


//------Private Variables-------//
        private bool _isHarmless;


#region UNITY_METHODS

#endregion


#region PUBLIC_METHODS

        public void OnCollide(GameObject collidedObject)
        {
            if (_isHarmless)
                return;
            if (HitFx)
                LeanPool.Spawn(HitFx, transform.position.WithAddedY(.25f), HitFx.transform.rotation);
            _isHarmless = true;
            HitEvent.Raise();
            AfterHitting();
        }

#endregion


#region PRIVATE_METHODS

        protected abstract void AfterHitting();

#endregion
    }
}