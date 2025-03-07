using Sirenix.OdinInspector;
using UnityEngine;
namespace QuickTools.Scripts.Utilities.RaycastVisualization
{
    public class CastTester : MonoBehaviour
    {
//-------Public Variables-------//

//------Serialized Fields-------//

       [SerializeField] private bool BoxCast;
       [SerializeField] private bool BoxCastAll;
        [SerializeField] private bool BoxCastNonAlloc;
        [SerializeField] private bool CapsuleCast;
        [SerializeField] private bool CapsuleCastAll;
        [SerializeField] private bool CapsuleCastNonAlloc;
        [SerializeField] private bool CheckBox;
        [SerializeField] private bool CheckCapsule;
        [SerializeField] private bool CheckSphere;
        [SerializeField] private bool Linecast;
        [SerializeField] private bool OverlapBox;
        [SerializeField] private bool OverlapBoxNonAlloc;
        [SerializeField] private bool OverlapCapsule;
        [SerializeField] private bool OverlapCapsuleNonAlloc;
        [SerializeField] private bool OverlapSphere;
        [SerializeField] private bool OverlapSphereNonAlloc;
        [SerializeField] private bool Raycast;
        [SerializeField] private bool RaycastAll;
        [SerializeField] private bool RaycastNonAlloc;
        [SerializeField] private bool SphereCast;
        [SerializeField] private bool SphereCastAll;
        [SerializeField] private bool SphereCastNonAlloc;

        [SerializeField] private LayerMask TargetLayer;


//------Private Variables-------//


#region UNITY_METHODS

        private void Update()
        {
            if (BoxCast)
            {
                _3D.VisualPhysics.BoxCast(transform.position, Vector3.one, transform.forward, transform.rotation, 10f,
                    TargetLayer);
            }

            if (BoxCastAll)
            {
                _3D.VisualPhysics.BoxCastAll(transform.position, Vector3.one, transform.forward, transform.rotation, 10f,
                    TargetLayer);
            }

            if (BoxCastNonAlloc)
            {
                _3D.VisualPhysics.BoxCastNonAlloc(transform.position, Vector3.one, transform.forward, new RaycastHit[10],
                    transform.rotation, 10f, TargetLayer);
            }

            if (CapsuleCast)
            {
                _3D.VisualPhysics.CapsuleCast(transform.position, transform.position + transform.forward * 0f, 1f,
                    transform.forward, 10f, TargetLayer);
            }

            if (CapsuleCastAll)
            {
                _3D.VisualPhysics.CapsuleCastAll(transform.position, transform.position + transform.forward * 10f, 1f,
                    transform.forward, 0f, TargetLayer);
            }

            if (CapsuleCastNonAlloc)
            {
                _3D.VisualPhysics.CapsuleCastNonAlloc(transform.position, transform.position + transform.forward * 10f, 1f,
                    transform.forward,
                    new RaycastHit[10], 10f, TargetLayer);
            }

            if (CheckBox)
            {
                _3D.VisualPhysics.CheckBox(transform.position, Vector3.one, transform.rotation, TargetLayer);
            }

            if (CheckCapsule)
            {
                _3D.VisualPhysics.CheckCapsule(transform.position, Vector3.one, 1f, TargetLayer);
            }

            if (CheckSphere)
            {
                _3D.VisualPhysics.CheckSphere(transform.position, 1f, TargetLayer);
            }

            if (Linecast)
            {
                _3D.VisualPhysics.Linecast(transform.position, transform.position + transform.forward * 10f, TargetLayer);
            }

            if (OverlapBox)
            {
                _3D.VisualPhysics.OverlapBox(transform.position, Vector3.one, transform.rotation, TargetLayer);
            }

            if (OverlapBoxNonAlloc)
            {
                _3D.VisualPhysics.OverlapBoxNonAlloc(transform.position, Vector3.one, new Collider[10], transform.rotation,
                    TargetLayer);
            }

            if (OverlapCapsule)
            {
                _3D.VisualPhysics.OverlapCapsule(transform.position, Vector3.one, 1f, TargetLayer);
            }

            if (OverlapCapsuleNonAlloc)
            {
                _3D.VisualPhysics.OverlapCapsuleNonAlloc(transform.position, Vector3.one, 1f, new Collider[10],
                    TargetLayer);
            }

            if (OverlapSphere)
            {
                _3D.VisualPhysics.OverlapSphere(transform.position, 1f, TargetLayer);
            }

            if (OverlapSphereNonAlloc)
            {
                _3D.VisualPhysics.OverlapSphereNonAlloc(transform.position, 1f, new Collider[10], TargetLayer);
            }

            if (Raycast)
            {
                _3D.VisualPhysics.Raycast(transform.position, transform.forward, 10f, TargetLayer);
            }

            if (RaycastAll)
            {
                _3D.VisualPhysics.RaycastAll(transform.position, transform.forward, 10f, TargetLayer);
            }

            if (RaycastNonAlloc)
            {
                _3D.VisualPhysics.RaycastNonAlloc(transform.position, transform.forward, new RaycastHit[10], 10f);
            }

            if (SphereCast)
            {
                var ray = new Ray(transform.position, transform.forward);
                _3D.VisualPhysics.SphereCast(ray, 1f, 10f, TargetLayer);
            }

            if (SphereCastAll)
            {
                var ray = new Ray(transform.position, transform.forward);
                _3D.VisualPhysics.SphereCastAll(ray, 1f, 10f, TargetLayer);
            }

            if (SphereCastNonAlloc)
            {
                var ray = new Ray(transform.position, transform.forward);
                _3D.VisualPhysics.SphereCastNonAlloc(ray, 1f, new RaycastHit[10], 10f, TargetLayer);
            }
        }

#endregion


#region PUBLIC_METHODS

#endregion


#region PRIVATE_METHODS

        [Button]
        private void OpenReadMeFile()
        {
            Application.OpenURL("https://github.com/nomnomab/RaycastVisualization");
        }

#endregion
    }
}