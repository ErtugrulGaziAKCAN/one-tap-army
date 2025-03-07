using System;
using Obvious.Soap;
using Sirenix.OdinInspector;
using UnityEngine;
namespace Plugins.Obvious.Soap.Core.Runtime.Bindings
{
    [AddComponentMenu("Soap/Bindings/BindValueSetter")]
    public class BindValueSetter : MonoBehaviour
    {
        private enum SetTime
        {
            Awake,
            OnEnable,
            Start,
        }
//-------Public Variables-------//


//------Serialized Fields-------//
        public AllVariableType Type = AllVariableType.None;
        [SerializeField, ShowIf("@Type==AllVariableType.Bool")] private BoolReference _boolComparer = null;
        [SerializeField, ShowIf("@Type==AllVariableType.Int")] private IntReference _intComparer = null;
        [SerializeField, ShowIf("@Type==AllVariableType.Float")] private FloatReference _floatComparer = null;
        [SerializeField, ShowIf("@Type==AllVariableType.String")] private StringReference _stringComparer = null;
        [SerializeField, ShowIf("@Type==AllVariableType.Vector2")] private Vector2Reference _vector2Reference = null;
        [SerializeField, ShowIf("@Type==AllVariableType.Vector3")] private Vector3Reference _vector3Reference = null;
        [SerializeField, ShowIf("@Type==AllVariableType.Transform")]
        private TransformReference _transformReference = null;
        [SerializeField] private SetTime SetOn;

        [SerializeField, ShowIf("@Type==AllVariableType.Int"), LabelText("Value"), Indent]
        private int IntValue;

        [SerializeField, ShowIf("@Type==AllVariableType.Float"), LabelText("Value"), Indent]
        private float FloatValue;

        [SerializeField, ShowIf("@Type==AllVariableType.Bool"), LabelText("Value"), Indent]
        private bool BoolValue;

        [SerializeField, ShowIf("@Type==AllVariableType.Vector3"), LabelText("Value"), Indent]
        private Vector3 Vector3Value;

        [SerializeField, ShowIf("@Type==AllVariableType.Vector2"), LabelText("Value"), Indent]
        private Vector2 Vector2Value;

        [SerializeField, ShowIf("@Type==AllVariableType.Transform"), LabelText("Value"), Indent]
        private Transform TransformValue;

        [SerializeField, ShowIf("@Type==AllVariableType.String"), LabelText("Value"), Indent]
        private String StringValue;
//------Private Variables-------//



#region UNITY_METHODS

        private void Awake()
        {
            if (SetOn != SetTime.Awake)
                return;
            SetValue();
        }

        private void OnEnable()
        {
            if (SetOn != SetTime.OnEnable)
                return;
            SetValue();
        }

        private void Start()
        {
            if (SetOn != SetTime.Start)
                return;
            SetValue();
        }

#endregion


#region PUBLIC_METHODS

#endregion


#region PRIVATE_METHODS

        private void SetValue()
        {
            switch (Type)
            {
                case AllVariableType.None:
                    break;
                case AllVariableType.Bool:
                    _boolComparer.Value = BoolValue;
                    break;
                case AllVariableType.Float:
                    _floatComparer.Value = FloatValue;
                    break;
                case AllVariableType.Int:
                    _intComparer.Value = IntValue;
                    break;
                case AllVariableType.String:
                    _stringComparer.Value = StringValue;
                    break;
                case AllVariableType.Vector2:
                    _vector2Reference.Value = Vector2Value;
                    break;
                case AllVariableType.Vector3:
                    _vector3Reference.Value = Vector3Value;
                    break;
                case AllVariableType.Transform:
                    _transformReference.Value = TransformValue;
                    break;
            }
        }

#endregion
    }
}