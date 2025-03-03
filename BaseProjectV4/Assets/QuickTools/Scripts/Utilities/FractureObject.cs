using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
namespace QuickTools.Scripts.Utilities
{
    public class FractureObject : MonoBehaviour
    {
//-------Public Variables-------//


//------Serialized Fields-------//
        [SerializeField, Required] private Transform SolidModel;
        [SerializeField, Required] private Transform FracturesRoot;
        [SerializeField] private bool DestroyPieces;
        [SerializeField, EnableIf(nameof(DestroyPieces)), Indent]
        private float PieceDestroyDelay = 3f;
        [SerializeField] private bool SetScaleAfterFractured;
        [SerializeField, ShowIf(nameof(SetScaleAfterFractured))] private float TargetUniformScale = 1f;

//------Private Variables-------//
#pragma warning disable 0414
        [ShowInInspector, ReadOnly, GUIColor(0f, 1f, 0f), ShowIf(nameof(_isInitialized))]
        private string _initializationStatusYes = "OK";

        [ShowInInspector, ReadOnly, GUIColor(1f, 0f, 0f), HideIf(nameof(_isInitialized))]
        private string _initializationStatusNo = "NOT READY";

        private bool _isInitialized;
        private Rigidbody[] _childBodies;


#region UNITY_METHODS

        private void Awake()
        {
            _childBodies = FracturesRoot.GetComponentsInChildren<Rigidbody>();
        }

#endregion


#region PUBLIC_METHODS

        [Button, ShowIf(nameof(_isInitialized))]
        public void Fracture()
        {
            SolidModel.gameObject.SetActive(false);
            FracturesRoot.gameObject.SetActive(true);
            foreach (var child in _childBodies)
            {
                if (SetScaleAfterFractured)
                {
                    child.transform.localScale = Vector3.one * TargetUniformScale;
                }
                child.isKinematic = false;
            }

            if (DestroyPieces)
                DestroyPiecesWithDelay();
        }

        [Button, ShowIf(nameof(_isInitialized))]
        public void Explode(Vector3 origin, float force = 500f, float radius = 10f)
        {
            Fracture();
            foreach (var body in _childBodies)
            {
                body.AddExplosionForce(force, origin, radius);
            }

            if (DestroyPieces)
                DestroyPiecesWithDelay();
        }

#endregion


#region PRIVATE_METHODS

        private void DestroyPiecesWithDelay()
        {
            foreach (var body in _childBodies)
            {
                DOVirtual.DelayedCall(PieceDestroyDelay, () =>
                {
                    body.isKinematic = true;
                    body.transform.DOScale(Vector3.zero, .2f)
                        .SetEase(Ease.InBack)
                        .OnComplete(() =>
                        {
                            body.transform.SetParent(FracturesRoot);
                            body.gameObject.SetActive(false);
                        });
                });
            }
        }

        [GUIColor(.2f, 1f, .2f), Button(ButtonSizes.Large), HideIf("@_isInitialized || _childBodies.Length > 0")]
        private void Initialize()
        {
            SetMeshReadable();
            foreach (Transform child in FracturesRoot)
            {
                child.TryGetComponent(out Rigidbody rb);
                if (!rb)
                    rb = child.gameObject.AddComponent<Rigidbody>();
                rb.isKinematic = true;
                child.TryGetComponent(out MeshCollider col);
                if (!col)
                    col = child.gameObject.AddComponent<MeshCollider>();
                col.convex = true;
            }

            SolidModel.gameObject.SetActive(true);
            FracturesRoot.gameObject.SetActive(false);
            _isInitialized = true;
        }

        [OnInspectorGUI]
        private void CheckIfInitialized()
        {
            if (!FracturesRoot || !SolidModel)
                return;
            _isInitialized = true;
            foreach (Transform child in FracturesRoot)
            {
                if (child.GetComponent<Rigidbody>() && child.GetComponent<Rigidbody>().isKinematic &&
                    child.GetComponent<MeshCollider>())
                    continue;

                _isInitialized = false;
                return;
            }

#if UNITY_EDITOR
            var path = AssetDatabase.GetAssetPath(SolidModel.GetComponent<MeshFilter>().sharedMesh);
            var fbx = (ModelImporter)AssetImporter.GetAtPath(path);
            if (fbx.isReadable)
                return;
#endif
            _isInitialized = false;
        }

        private void SetMeshReadable()
        {
#if UNITY_EDITOR
            var path = AssetDatabase.GetAssetPath(SolidModel.GetComponent<MeshFilter>().sharedMesh);
            var fbx = (ModelImporter)AssetImporter.GetAtPath(path);
            fbx.isReadable = true;
            AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
#endif
        }

#endregion
    }
}