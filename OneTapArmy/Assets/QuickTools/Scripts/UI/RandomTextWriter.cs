using System.Collections.Generic;
using MonKey.Extensions;
using Nova;
using TMPro;
using UnityEngine;
namespace QuickTools.Scripts.UI
{
    public class RandomTextWriter : MonoBehaviour
    {
//-------Public Variables-------//


//------Serialized Fields-------//
        [SerializeField] private TextType TargetTextType;
        [SerializeField] private List<string> Texts;
        [SerializeField] private bool InitOnEnable = true;

//------Private Variables-------//
        private enum TextType
        {
            UnityTmp,
            Nova,
            None
        }
        private TextMeshProUGUI _tmp;
        private TextBlock _tBlock;
        private bool _isInit;

#region UNITY_METHODS

        private void Awake()
        {
            if (TargetTextType == TextType.UnityTmp)
                _tmp = GetComponent<TextMeshProUGUI>();
            else
                _tBlock = GetComponent<TextBlock>();
            if (InitOnEnable)
                SetRandomText();
            _isInit = true;
        }

        private void OnEnable()
        {
            if (!InitOnEnable)
                return;
            if (!_isInit)
                return;
            SetRandomText();
        }

#endregion


#region PUBLIC_METHODS

#endregion


#region PRIVATE_METHODS

        private void SetRandomText()
        {
            var randomText = Texts.GetRandom();
            if (TargetTextType == TextType.UnityTmp)
                _tmp.SetText(randomText);
            else
                _tBlock.Text = randomText;
        }

#endregion
    }
}