using DG.Tweening;
using UnityEngine;
namespace QuickTools.Scripts.UI
{
    public class UseCountIcon : MonoBehaviour
    {
//-------Public Variables-------//


//------Serialized Fields-------//
        [SerializeField] private GameObject FullImage;

//------Private Variables-------//


#region UNITY_METHODS

#endregion


#region PUBLIC_METHODS

        public void SetFullAnimated(bool flag)
        {
            if (flag)
            {
                if (!FullImage.activeSelf)
                {
                    FullImage.transform.localScale = Vector3.zero;
                    FullImage.transform.DOScale(1f, .25f)
                        .SetEase(Ease.OutBack)
                        .OnStart(() => { FullImage.gameObject.SetActive(true); });
                }
            }
            else if (FullImage.activeSelf)
                FullImage.transform.DOScale(0f, .25f)
                    .SetEase(Ease.InBack)
                    .OnComplete(() => FullImage.gameObject.SetActive(false));
        }

        public void SetFullImmediate(bool flag)
        {
            if (flag)
            {
                if (!FullImage.activeSelf)
                {
                    FullImage.gameObject.SetActive(true);
                    FullImage.transform.localScale = Vector3.one;
                }
            }
            else if (FullImage.activeSelf)
                FullImage.gameObject.SetActive(false);
        }

#endregion


#region PRIVATE_METHODS

#endregion
    }
}