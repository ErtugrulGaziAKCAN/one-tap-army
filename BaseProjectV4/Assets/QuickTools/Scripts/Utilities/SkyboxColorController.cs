using System;
using Obvious.Soap;
using Sirenix.OdinInspector;
using UnityEngine;
namespace QuickTools.Scripts.Utilities
{
    public class SkyboxColorController : MonoBehaviour
    {
//-------Public Variables-------//


//------Serialized Fields-------//
        [SerializeField, Required] private IntReference CurrentLevelRef;
        [SerializeField] private SkyboxDetail[] SkyboxDetails;

        //------Private Variables-------//
        private int Index => (CurrentLevelRef.Value - 1) % SkyboxDetails.Length;


#region UNITY_METHODS

        private void Start()
        {
            UpdateColors();
        }

#endregion


#region PUBLIC_METHODS

#endregion


#region PRIVATE_METHODS

        private void UpdateColors()
        {
            var currentSkybox = SkyboxDetails[Index];
            RenderSettings.skybox = currentSkybox.SkyboxMaterial;
            RenderSettings.fogColor = currentSkybox.FogColor;
        }

        [PropertySpace(25), Button(ButtonSizes.Large)]
        private void TestOnLevel(int index)
        {
            var currentSkybox = SkyboxDetails[(index - 1) % SkyboxDetails.Length];
            RenderSettings.skybox = currentSkybox.SkyboxMaterial;
            RenderSettings.fogColor = currentSkybox.FogColor;
        }

#endregion
    }

    [Serializable]
    public class SkyboxDetail
    {
        [PreviewField(ObjectFieldAlignment.Center), HideLabel, HorizontalGroup("Details")]
        public Material SkyboxMaterial;

        [Space, VerticalGroup("Details/Fog"), LabelWidth(60)]
        public Color FogColor;

        private static readonly int TopColor = Shader.PropertyToID("_TopColor");
        private static readonly int BottomColor = Shader.PropertyToID("_BottomColor");
        private static readonly int MiddleColor = Shader.PropertyToID("_MiddleColor");

        [Button, VerticalGroup("Details/Fog"), LabelText("Get Auto")]
        private void AutoFogColor()
        {
            var currentSkybox = SkyboxMaterial;
            var hasMiddleColor = currentSkybox.HasProperty("_MiddleColor");
            if (hasMiddleColor)
            {
                FogColor = currentSkybox.GetColor(MiddleColor);
                return;
            }

            var hasTopColor = currentSkybox.HasProperty(TopColor);
            var hasBottomColor = currentSkybox.HasProperty(BottomColor);
            if (!hasTopColor || !hasBottomColor)
                return;
            var topColor = currentSkybox.GetColor(TopColor);
            var bottomColor = currentSkybox.GetColor(BottomColor);
            var fogColor = Color.Lerp(topColor, bottomColor, 0.45f);
            fogColor.a = 1;
            FogColor = fogColor;
        }
    }
}