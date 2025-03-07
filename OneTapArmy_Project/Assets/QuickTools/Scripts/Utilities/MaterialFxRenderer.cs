using UnityEngine;
namespace QuickTools.Scripts.Utilities
{
    public class MaterialFxHelpers
    {
        public class MaterialFxRenderer : MonoBehaviour
        {
//-------Public Variables-------//


//------Serialized Fields-------//


//------Private Variables-------//
            private Renderer _renderer;
            private Material[] _originalMaterials;
            private float[] _originalSaturations;
            private static readonly int Layer3Color = Shader.PropertyToID("_FlashColor");
            private static readonly int Layer3Alpha = Shader.PropertyToID("_FlashRange");
            private static readonly int Saturation = Shader.PropertyToID("_HSV_S");


#region UNITY_METHODS

            private void Start()
            {
                _renderer = GetComponent<Renderer>();
                _originalMaterials = _renderer.materials;
                _originalSaturations = new float[_originalMaterials.Length];
                for (var i = 0; i < _originalMaterials.Length; i++)
                {
                    _originalSaturations[i] = _originalMaterials[i].GetFloat(Saturation);
                }
            }

#endregion


#region PUBLIC_METHODS

            public void SetMaterials(Material m)
            {
                var mats = _renderer.materials;
                for (var i = 0; i < mats.Length; i++)
                {
                    mats[i] = m;
                }

                _renderer.materials = mats;
            }

            public void MakeVisible(bool flag)
            {
                _renderer.enabled = flag;
            }

            public void SetColorOverride(Color c, float alpha)
            {
                var mats = _renderer.materials;
                foreach (var t in mats)
                {
                    t.SetColor(Layer3Color, c);
                    t.SetFloat(Layer3Alpha, alpha);
                }
            }

            public void Revert()
            {
                _renderer.enabled = true;
                _renderer.materials = _originalMaterials;
                SetColorOverride(Color.white, 0f);
                RevertSaturation();
            }

            public void SetSaturation(float saturation)
            {
                var mats = _renderer.materials;
                foreach (var t in mats)
                {
                    t.SetFloat(Saturation, saturation);
                }
            }

            private void RevertSaturation()
            {
                var mats = _renderer.materials;
                for (var i = 0; i < mats.Length; i++)
                {
                    mats[i].SetFloat(Saturation, _originalSaturations[i]);
                }
            }

#endregion


#region PRIVATE_METHODS

#endregion
        }
    }
}