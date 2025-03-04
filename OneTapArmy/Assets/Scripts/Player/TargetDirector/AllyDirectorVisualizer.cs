using DG.Tweening;
using QuickTools.Scripts.Extensions;
using UnityEngine;
namespace Player.TargetDirector
{
    public class AllyDirectorVisualizer : MonoBehaviour
    {
        [SerializeField] private Transform PointA;
        [SerializeField] private Transform LineSprite;
        [SerializeField] private Transform PointSprite;
        [SerializeField] private float LineThickness;
        [SerializeField] private ScriptableListAIDataHolderCore SpawnedAllies;
        private Tween _lenghtTween;

        private void OnEnable()
        {
            SpawnedAllies.OnItemCountChanged += DeActivateLine;
        }

        private void OnDisable()
        {
            SpawnedAllies.OnItemCountChanged -= DeActivateLine;
        }

        public void DrawWorldLine(Vector3 pointB)
        {
            var aPosition = PointA.position;
            var midpoint = (aPosition + pointB) / 2f;
            LineSprite.position = midpoint;

            var length = Vector3.Distance(aPosition, pointB);
            _lenghtTween.Kill();
            _lenghtTween = DOVirtual.Float(0f, length -.3f, .1f,
                (l) => LineSprite.localScale = new Vector3(LineThickness, LineThickness, l)).OnComplete(() =>
            {
                PointSprite.transform.position = pointB;
                PointSprite.gameObject.SetActive(false);
                PointSprite.gameObject.SetActive(true);
            });

            var direction = pointB - aPosition;
            LineSprite.rotation = Quaternion.LookRotation(direction);
        }

        private void DeActivateLine()
        {
            _lenghtTween.Kill();
            _lenghtTween = DOVirtual.Float(LineSprite.localScale.z, 0f, .1f,
                (l) => LineSprite.localScale = new Vector4(LineThickness, LineThickness, l));
            PointSprite.gameObject.SetActive(false);
        }
    }
}