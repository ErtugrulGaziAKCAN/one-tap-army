using Obvious.Soap;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
namespace QuickTools.Scripts.UI
{
    [AddComponentMenu("QuickTools/UI/Percentage Text")]
    public class PercentageText : MonoBehaviour
    {
//-------Public Variables-------//


//------Serialized Fields-------//
        [SerializeField] private UseType Use;

        [SerializeField, LabelText("Ref Value"), SuffixLabel("0-100"), ShowIf("@Use==UseType.FloatRef")]
        private FloatReference FloatRefValue;

        [SerializeField, Range(0, 1)] private int DecimalPlaces = 0;

//------Private Variables-------//
        private enum UseType
        {
            FloatRef,
            Float01Ref
        }

        private TMP_Text _tmpText;
        private TextMeshProUGUI _guiText;
        private bool _isGui;


#region UNITY_METHODS

        private void Awake()
        {
            _tmpText = GetComponent<TMP_Text>();
            _guiText = GetComponent<TextMeshProUGUI>();
            _isGui = _guiText != null;
            if (_tmpText == null && _guiText == null)
                Debug.LogError("No Text Component Found");
        }

        private void OnEnable()
        {
            FloatRefValue.Variable.OnValueChanged += UpdateText;
            UpdateText(FloatRefValue.Value);
        }

        private void UpdateText(float value)
        {
            if (_isGui)
                _guiText.text = (value / 100f).ToString("P" + DecimalPlaces);
            else
                _tmpText.text = (value / 100f).ToString("P" + DecimalPlaces);
        }

#endregion


#region PUBLIC_METHODS

#endregion


#region PRIVATE_METHODS

#endregion
    }
}