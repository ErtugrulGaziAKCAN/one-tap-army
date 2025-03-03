using Plugins.DamageNumbersPro.Scripts.Internal;
using Sirenix.OdinInspector;
using UnityEngine;
namespace QuickTools.Scripts.UI
{
    [AddComponentMenu("QuickTools/Fx/Floating Text Spawner")]
    public class FloatingTextSpawner : MonoBehaviour
    {
//-------Public Variables-------//


//------Serialized Fields-------//
        [SerializeField] private DamageNumber Prefab;
        [SerializeField] private string Message;
        [SerializeField] private bool CustomColor;
        [SerializeField] private bool SetRefValue = true;
        [SerializeField, ShowIf(nameof(SetRefValue))] private FloatingTextSpawnerVariable Variable;
        [SerializeField, Indent, ShowIf(nameof(CustomColor))]
        private Color Color = Color.white;

//------Private Variables-------//
        private Transform _dnp;

#region UNITY_METHODS

        private void Start()
        {
            Variable.Value = this;
        }

#endregion


#region PUBLIC_METHODS

        [Button("Test")]
        public void Spawn()
        {
            if (CustomColor)
                _dnp = Prefab.Spawn(transform.position, Message, Color).transform;
            else
                _dnp = Prefab.Spawn(transform.position, Message).transform;
            _dnp.SetParent(transform);
            _dnp.localScale = Vector3.one;
        }

        public void Spawn(float number)
        {
            if (CustomColor)
                _dnp = Prefab.Spawn(transform.position, number, Color).transform;
            else
                _dnp = Prefab.Spawn(transform.position, number).transform;
            _dnp.SetParent(transform);
            _dnp.localScale = Vector3.one;
        }

        public void Spawn(string message)
        {
            if (CustomColor)
                _dnp = Prefab.Spawn(transform.position, message, Color).transform;
            else
                _dnp = Prefab.Spawn(transform.position, message).transform;
            _dnp.SetParent(transform);
            _dnp.localScale = Vector3.one;
        }

#endregion


#region PRIVATE_METHODS

#endregion
    }
}