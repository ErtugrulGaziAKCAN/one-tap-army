using UnityEngine;
namespace Player.TargetDirector
{
    public class AdaptiveTiling : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer SpriteRenderer;
        [SerializeField] private Transform ScaledParent;
        [SerializeField] private float TilingSpeed;
        private static readonly int MainTex = Shader.PropertyToID("_MainTex");
        private float _currentTiling;
        void Update()
        {

            Vector2 newTiling =
                new Vector2(1, ScaledParent.localScale.z * 4f);
            SpriteRenderer.material.SetTextureScale(MainTex, newTiling);
            _currentTiling += Time.deltaTime * TilingSpeed;
            if (_currentTiling >= 1f)
                _currentTiling = 0f;
            SpriteRenderer.material.SetTextureOffset(MainTex, new Vector2(0f, _currentTiling));

        }
    }
}