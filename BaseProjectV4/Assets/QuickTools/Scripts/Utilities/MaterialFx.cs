using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
namespace QuickTools.Scripts.Utilities
{
    [AddComponentMenu("QuickTools/Fx/Material FX")]
    public class MaterialFx : MonoBehaviour
    {
//-------Public Variables-------//


//------Serialized Fields-------//


//------Private Variables-------//
        private List<MaterialFxHelpers.MaterialFxRenderer> _fxRenderers;
        private Tween _colorTween;

#region UNITY_METHODS

        private void Awake()
        {
            _fxRenderers = new List<MaterialFxHelpers.MaterialFxRenderer>();
            foreach (var r in GetAllMeshAndSkinnedMeshRenderersInChildren())
            {
                var fxRenderer = r.gameObject.AddComponent<MaterialFxHelpers.MaterialFxRenderer>();
                _fxRenderers.Add(fxRenderer);
            }
        }

#endregion


#region PUBLIC_METHODS

        [Button]
        public void SetMaterial(Material m)
        {
            foreach (var fxRenderer in _fxRenderers)
            {
                fxRenderer.SetMaterials(m);
            }
        }

        [Button]
        public void ChangeColor(Color c, int flickCount, float flickDuration, float alpha = 1f,
            bool returnToOriginal = true)
        {
            if (flickCount <= 0)
            {
                flickCount = 1;
                returnToOriginal = false;
            }

            CancelColorTween();
            _colorTween = DOVirtual.Float(0f, alpha, flickDuration, v => { SetOverrideColor(c, v); })
                .SetLoops(returnToOriginal ? flickCount * 2 : flickCount * 2 - 1)
                .SetEase(Ease.InOutQuad);
        }

        [Button]
        public void MakeInvisible(int flickCount, float interval, bool stayInvisible = false)
        {
            if (flickCount <= 0)
                flickCount = 1;
            foreach (var fxRenderer in _fxRenderers)
            {
                fxRenderer.MakeVisible(false);
            }

            if (flickCount == 1 && stayInvisible)
                return;

            if (!stayInvisible)
                flickCount = flickCount * 2 - 1;
            else
            {
                flickCount = flickCount * 2 - 2;
            }

            var visible = false;
            DOVirtual.DelayedCall(interval, () =>
                {
                    visible = !visible;
                    foreach (var fxRenderer in _fxRenderers)
                    {
                        fxRenderer.MakeVisible(visible);
                    }
                })
                .SetLoops(flickCount);
        }

        [Button]
        public void MakeGreyscale(float duration)
        {
            CancelColorTween();
            _colorTween = DOVirtual.Float(1f, 0f, duration, v =>
                {
                    foreach (var fxRenderer in _fxRenderers)
                    {
                        fxRenderer.SetSaturation(v);
                    }
                })
                .SetEase(Ease.InOutQuad);
        }

        [Button]
        public void RevertToOriginal()
        {
            CancelColorTween();
            foreach (var fxRenderer in _fxRenderers)
            {
                fxRenderer.Revert();
            }
        }

#endregion


#region PRIVATE_METHODS

        private void CancelColorTween()
        {
            _colorTween.Kill();
            SetOverrideColor(Color.white, 0f);
        }

        private void SetOverrideColor(Color c, float alpha)
        {
            foreach (var fxRenderer in _fxRenderers)
            {
                fxRenderer.SetColorOverride(c, alpha);
            }
        }

        private Renderer[] GetAllMeshAndSkinnedMeshRenderersInChildren()
        {
            var renderers = GetComponentsInChildren<Renderer>();
            return renderers.Where(r =>
                    r is MeshRenderer or SkinnedMeshRenderer &&
                    r.GetComponent<IgnoreMaterialFx>() == null)
                .ToArray();
        }

#endregion
    }
}