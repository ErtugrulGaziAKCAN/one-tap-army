using UnityEngine;
namespace QuickTools.Scripts.UI
{
    public class HandCursorInputController : MonoBehaviour
    {
//-------Public Variables-------//


//------Serialized Fields-------//
        [SerializeField] private RectTransform Default;
        [SerializeField] private RectTransform Tapped;
        [SerializeField] private bool IsActive;

//------Private Variables-------//
        private Canvas _mainCanvas;

#region UNITY_METHODS

        private void Awake()
        {
            transform.parent.TryGetComponent(out _mainCanvas);
        }

        private void Update()
        {
            if (!IsActive)
                return;
            SetCursorActive();
        }

#endregion


#region PUBLIC_METHODS

#endregion


#region PRIVATE_METHODS

        private void SetCursorActive()
        {
            var targetImage = Input.GetMouseButton(0) ? Tapped : Default;
            var otherImage = Input.GetMouseButton(0) ? Default : Tapped;
            targetImage.gameObject.SetActive(true);
            otherImage.gameObject.SetActive(false);
            targetImage.anchoredPosition = Input.mousePosition / _mainCanvas.scaleFactor;
        }

#endregion
    }
}