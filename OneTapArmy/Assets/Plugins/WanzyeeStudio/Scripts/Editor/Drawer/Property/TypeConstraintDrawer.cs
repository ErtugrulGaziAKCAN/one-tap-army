
/*WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW*\     (   (     ) )
|/                                                      \|       )  )   _((_
||  (c) Wanzyee Studio  < wanzyeestudio.blogspot.com >  ||      ( (    |_ _ |=n
|\                                                      /|   _____))   | !  ] U
\.ZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZ All rights reserved./  (_(__(S)   |___*/

using UnityEditor;
using UnityEngine;
using System;
using System.Linq;

using WanzyeeStudio.Extension;

using Object = UnityEngine.Object;

namespace WanzyeeStudio.Editrix.Drawer{

	/// <summary>
	/// <c>UnityEditor.CustomPropertyDrawer</c> for <c>TypeConstraintAttribute</c>.
	/// </summary>
	/// 
	/// <remarks>
	/// Draw a common object field, but filter the assigned object type by constraints.
	/// And a context menu to select one of multiple components.
	/// </remarks>
	/// 
	[CustomPropertyDrawer(typeof(TypeConstraintAttribute))]
	internal class TypeConstraintDrawer : PropertyDrawer{
		
		#region Fields

		/// <summary>
		/// The constraint types to filter assigned object, stored when initializing.
		/// </summary>
		private Type[] _constraints;

		/// <summary>
		/// Error message.
		/// </summary>
		private static readonly GUIContent _error = new GUIContent("TypeConstraint Error!", "Only for object field.");

		#endregion


		#region Methods

		/// <summary>
		/// Get the constraint types from the field info and the attribute.
		/// </summary>
		/// <returns>The constraints.</returns>
		private Type[] GetConstraints(){

			var _field = new []{fieldInfo.FieldType.GetItemType() ?? fieldInfo.FieldType};

			var _types = (attribute as TypeConstraintAttribute).constraints;

			return (null == _types) ? _field : _field.Union(_types).Where((type) => null != type).ToArray();

		}

		/// <summary>
		/// Filter the valid objects from specified sources by constraint types.
		/// Look into <c>UnityEngine.Component</c> on assigned <c>UnityEngine.GameObject</c>.
		/// </summary>
		/// <returns>The objects.</returns>
		/// <param name="sources">Sources.</param>
		private Object[] FilterObjects(params Object[] sources){

			if(null == _constraints) _constraints = GetConstraints();
			var _objects = sources.OfType<GameObject>().ToArray();

			return _objects.Cast<Object>(

				).Union(_objects.SelectMany((obj) => obj.GetComponents<Component>().Cast<Object>())
				).Union(sources

				).Where((obj) => null != obj
				).Where((obj) => _constraints.All((type) => type.IsInstanceOfType(obj))

			).ToArray();

		}

		/// <summary>
		/// Check if to show the context menu to select one of components, right click in the specified area.
		/// </summary>
		/// <param name="position">Position.</param>
		/// <param name="property">Property.</param>
		private void CheckContext(Rect position, SerializedProperty property){

			position.xMin += EditorGUIUtility.labelWidth;
			if(EventType.ContextClick != Event.current.type || !position.Contains(Event.current.mousePosition)) return;
			Event.current.Use();

			var _old = property.objectReferenceValue;
			var _objects = FilterObjects((_old is Component) ? (_old as Component).gameObject : _old);
			if(2 > _objects.Length) return;

			var _menu = new GenericMenu();

			for(int i = 0; i < _objects.Length; i++){
				var _new = _objects[i];
				var _label = string.Format("[{0}] {1}", i, _new.GetType().Name);
				_menu.AddItem(new GUIContent(_label), _new == _old, () => ApplyValue(property, _new));
			}

			_menu.ShowAsContext();

		}

		/// <summary>
		/// Set the value and apply the serialized object.
		/// </summary>
		/// <param name="property">Property.</param>
		/// <param name="value">Value.</param>
		private void ApplyValue(SerializedProperty property, Object value){
			property.objectReferenceValue = value;
			property.serializedObject.ApplyModifiedProperties();
		}

		/// <summary>
		/// OnGUI, draw inspector of the property.
		/// </summary>
		/// <param name="position">Position rect.</param>
		/// <param name="property">Property.</param>
		/// <param name="label">Label.</param>
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label){

			if(SerializedPropertyType.ObjectReference != property.propertyType){
				EditrixGUI.ErrorField(position, label, _error);
				return;
			}

			CheckContext(position, property);
			label = EditorGUI.BeginProperty(position, label, property);
			
			var _old = FilterObjects(property.objectReferenceValue).FirstOrDefault();
			EditorGUI.PropertyField(position, property, label);
			
			var _new = property.objectReferenceValue;
			if(null != _new) property.objectReferenceValue = FilterObjects(_new).FirstOrDefault() ?? _old;
			
			EditorGUI.EndProperty();

		}

		#endregion

	}

}
