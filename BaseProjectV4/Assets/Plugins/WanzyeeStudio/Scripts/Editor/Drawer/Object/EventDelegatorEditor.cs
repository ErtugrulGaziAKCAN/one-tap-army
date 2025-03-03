
/*WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW*\     (   (     ) )
|/                                                      \|       )  )   _((_
||  (c) Wanzyee Studio  < wanzyeestudio.blogspot.com >  ||      ( (    |_ _ |=n
|\                                                      /|   _____))   | !  ] U
\.ZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZ All rights reserved./  (_(__(S)   |___*/

using UnityEditor;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using WanzyeeStudio.Editrix.Extension;
using WanzyeeStudio.Extension;

using Object = UnityEngine.Object;

namespace WanzyeeStudio.Editrix.Drawer{
	
	/// <summary>
	/// <c>UnityEditor.CustomEditor</c> for <c>EventDelegator</c>.
	/// </summary>
	/// 
	/// <remarks>
	/// Draw the GUI to setup a reflection method with parameters.
	/// </remarks>
	/// 
	[CustomEditor(typeof(EventDelegator))]
	internal class EventDelegatorEditor : Editor{

		#region Static

		/// <summary>
		/// Get the options, label method pairs, to select static method of specified types.
		/// </summary>
		/// <returns>The options.</returns>
		/// <param name="types">Types.</param>
		private static Dictionary<string, MethodInfo> GetStaticOptions(IEnumerable<Type> types){
			
			var _flag = BindingFlags.Static | BindingFlags.DeclaredOnly | BindingFlags.Public;

			var _methods = types.OrderBy((type) => type.FullName).SelectMany((type) => GetMethods(type, _flag));

			return _methods.ToDictionary((method) => GetLabel(method, true));

		}

		/// <summary>
		/// Get all supported methods of specified type, including property setting methods.
		/// I.e., public, non-generic, and all the parameter types are supported.
		/// </summary>
		/// <returns>The methods.</returns>
		/// <param name="type">Type.</param>
		/// <param name="flags">Flags.</param>
		private static IEnumerable<MethodInfo> GetMethods(Type type, BindingFlags flags){

			var _properties = type.GetProperties(flags).Where((property) => !property.GetIndexParameters().Any());
			var _setters = _properties.Select((property) => property.GetSetMethod()).Where((method) => null != method);

			var _methods = type.GetMethods(flags).Where((method) => !method.IsSpecialName);
			var _all = _methods.OrderBy((method) => method.Name).Union(_setters.OrderBy((method) => method.Name));

			var _publics = _all.Where((method) => method.IsPublic && !method.IsFamilyOrAssembly);
			return _publics.Where((method) => EventDelegator.IsSupported(method));

		}

		/// <summary>
		/// Get the label of specified method.
		/// Method name and each parameter type name for instance method.
		/// Prefix with declaring type for static method.
		/// Optional to optimize for menu item usage, separate names with slash.
		/// </summary>
		/// <returns>The label.</returns>
		/// <param name="method">Method.</param>
		/// <param name="menu">If set to <c>true</c> separate names with slash.</param>
		private static string GetLabel(MethodInfo method, bool menu){

			if(null == method) return "No Function";

			var _types = method.GetParameters().Select((param) => param.ParameterType.GetPrettyName());
			var _params = string.Join(", ", _types.ToArray());

			var _method = string.Format("{0} {1}({2})", method.ReturnType.GetPrettyName(), method.Name, _params);
			if(!method.IsStatic) return _method;

			if(!menu) return _method.Insert(_method.IndexOf(' ') + 1, method.DeclaringType.Name + ".");
			else return string.Format("{0}/{1}", method.DeclaringType.FullName.Replace('.', '/'), _method);

		}

		#endregion


		#region Methods

		/// <summary>
		/// OnInspectorGUI, draw inspector of the editor.
		/// </summary>
		/// 
		/// <remarks>
		/// Include an object field to select instance, the instance is also used as flag to find static or not.
		/// A popup menu to select method, then draw the parameter fields if existing.
		/// </remarks>
		/// 
		public override void OnInspectorGUI(){

			serializedObject.Update();
			GUILayout.Space(4f);

			EditorGUILayout.PropertyField(serializedObject.FindProperty("_instance"));
			var _method = (target as EventDelegator).methodInfo;

			CheckMethod(_method);
			DrawMethod(_method);

			var _params = (null == _method) ? null : _method.GetParameters();
			if(null != _params && 0 < _params.Length) DrawParameters(_params);

			serializedObject.ApplyModifiedProperties();

		}

		/// <summary>
		/// Check if the existing method matches the instance, otherwise reset the properties.
		/// </summary>
		/// <param name="method">Method.</param>
		private void CheckMethod(MethodInfo method){

			if(null == method) return;
			var _instance = serializedObject.FindProperty("_instance").objectReferenceValue;

			if(null == _instance && method.IsStatic) return;
			if(null != _instance && !method.IsStatic && method.DeclaringType.IsInstanceOfType(_instance)) return;

			serializedObject.FindProperty("_type").stringValue = "";
			serializedObject.FindProperty("_method").stringValue = "";
			serializedObject.FindProperty("_params").arraySize = 0;

		}

		/// <summary>
		/// Draw the method popup menu field to select.
		/// </summary>
		/// <param name="current">Current method.</param>
		private void DrawMethod(MethodInfo current){

			GUILayout.BeginHorizontal();
			EditorGUILayout.PrefixLabel(new GUIContent("Method", "Method to invoke."));

			var _position = GUILayoutUtility.GetRect(GUIContent.none, EditorStyles.popup);
			_position.xMin = EditorGUIUtility.labelWidth + 14f;

			if(GUI.Button(_position, GetLabel(current, false), EditorStyles.popup)) ShowMethodMenu(current, _position);
			GUILayout.EndHorizontal();

		}

		/// <summary>
		/// Show the menu to select a method.
		/// </summary>
		/// <param name="current">Current method.</param>
		/// <param name="rect">Rect.</param>
		private void ShowMethodMenu(MethodInfo current, Rect rect){

			var _menu = new GenericMenu();

			_menu.AddItem(GetLabel(null, true), SetMethod, null as MethodInfo, null == current);
			_menu.AddSeparator("");

			var _options = GetMethodOptions();
			foreach(var _pair in _options) _menu.AddItem(_pair.Key, SetMethod, _pair.Value, _pair.Value == current);

			_menu.DropDown(rect);

		}

		/// <summary>
		/// Get the options to select one of the instance's methods.
		/// Or static methods if the instance doesn't exist, show options of editor class if "Ctrl" key pressed.
		/// </summary>
		/// <returns>The options.</returns>
		private Dictionary<string, MethodInfo> GetMethodOptions(){

			var _object = serializedObject.FindProperty("_instance").objectReferenceValue;
			if(null == _object) return GetStaticOptions(EditrixUtility.GetExposingTypes(Event.current.control));

			var _methods = GetMethods(_object.GetType(), BindingFlags.Instance | BindingFlags.Public);
			return _methods.ToDictionary((method) => GetLabel(method, true));

		}

		/// <summary>
		/// Set the specified <c>MethodInfo</c> with default parameter values.
		/// </summary>
		/// <param name="method">Method.</param>
		private void SetMethod(MethodInfo method){
			
			var _delegator = target as EventDelegator;
			if(_delegator.methodInfo == method) return;

			Undo.RecordObject(_delegator, "Set Method");
			var _instance = serializedObject.FindProperty("_instance").objectReferenceValue;

			if(null == method){
				_delegator.SetMethod(method, _instance);
			}else{
				var _params = method.GetParameters().Select((param) => param.ParameterType.GetDefault());
				_delegator.SetMethod(method, _instance, _params.ToArray());
			}

			_delegator.OnBeforeSerialize();

		}

		#endregion


		#region Parameter Methods

		/// <summary>
		/// Draw the selected method parameters if the sizes match, in case the properties reset before method applied.
		/// </summary>
		/// <param name="parameters">Parameters.</param>
		private void DrawParameters(ParameterInfo[] parameters){

			var _property = serializedObject.FindProperty("_params");
			if(parameters.Length != _property.arraySize) return;

			EditorGUILayout.LabelField(new GUIContent("Parameters", "Setup parameter values."), GUIContent.none);
			EditorGUI.indentLevel++;

			for(int i = 0; i < parameters.Length; i++){

				var _label = parameters[i].Name;
				var _type = parameters[i].ParameterType;

				var _element = _property.GetArrayElementAtIndex(i);
				_element = _element.FindPropertyRelative(_element.FindPropertyRelative("path").stringValue);

				if(_type.IsArray || _type.IsGenericType) DrawArray(_label, _type.GetItemType(), _element);
				else DrawElement(_label, _type, _element.GetArrayElementAtIndex(0));

			}

			EditorGUI.indentLevel--;

		}

		/// <summary>
		/// Draw the parameter element field.
		/// </summary>
		/// <param name="label">Label.</param>
		/// <param name="type">Element type.</param>
		/// <param name="property">Element property.</param>
		private void DrawElement(string label, Type type, SerializedProperty property){

			if(type.IsEnum){

				var _enum = (Enum)Enum.ToObject(type, property.intValue);
				property.intValue = (int)(EditorGUILayout.EnumPopup(label, _enum) as object);

			}else if(typeof(Object).IsAssignableFrom(type)){

				var _object = property.objectReferenceValue;
				var _height = GUILayout.Height(EditorGUIUtility.singleLineHeight);
				property.objectReferenceValue = EditorGUILayout.ObjectField(label, _object, type, true, _height);

			}else{

				EditorGUILayout.PropertyField(property, new GUIContent(label), true);

			}

		}

		/// <summary>
		/// Draw the parameter array with element context menu as built-in GUI.
		/// Check if Drag'n'Drop to append when drawing <c>UnityEngine.Object</c> array.
		/// </summary>
		/// <param name="label">Label.</param>
		/// <param name="type">Element type.</param>
		/// <param name="property">Array property.</param>
		private void DrawArray(string label, Type type, SerializedProperty property){

			var _rect = GUILayoutUtility.GetRect(0f, EditorGUIUtility.singleLineHeight);
			var _point = Event.current.mousePosition;

			if(typeof(Object).IsAssignableFrom(type) && _rect.Contains(_point)) CheckDrag(type, property);
			EditorGUI.PropertyField(_rect, property, new GUIContent(label), false);

			if(!property.isExpanded) return;
			EditorGUI.indentLevel++;

			EditorGUILayout.PropertyField(property.FindPropertyRelative("Array.size"));
			for(int i = 0; i < property.arraySize; i++){

				var _element = property.GetArrayElementAtIndex(i);
				DrawElement("Element " + i, type, _element);

				_rect = GUILayoutUtility.GetLastRect();
				if(_rect.Contains(_point) && EventType.ContextClick == Event.current.type) ShowElementMenu(_element);

			}

			EditorGUI.indentLevel--;

		}

		/// <summary>
		/// Check if Drag'n'Drop valid, append new <c>UnityEngine.Object</c>.
		/// </summary>
		/// <param name="type">Element type.</param>
		/// <param name="property">Array property.</param>
		private void CheckDrag(Type type, SerializedProperty property){

			var _objects = EditrixGUI.CheckDragObjects((objects) => {

				if(typeof(Component).IsAssignableFrom(type)){
					var _components = objects.OfType<GameObject>().Select((obj) => obj.GetComponent(type));
					objects = objects.Union(_components.OfType<Object>());
				}

				return objects.Where((obj) => null != obj && type.IsInstanceOfType(obj));

			});

			if(null != _objects) property.AddRange(_objects);

		}

		/// <summary>
		/// Show the array element context menu to duplicate or delete specified element.
		/// </summary>
		/// <param name="property">Element property.</param>
		private void ShowElementMenu(SerializedProperty property){

			var _menu = new GenericMenu();

			_menu.AddItem("Duplicate Array Element", () => {
				property.DuplicateCommand();
				property.serializedObject.ApplyModifiedProperties();
			});

			_menu.AddItem("Delete Array Element", () => {
				property.DeleteCommand();
				property.serializedObject.ApplyModifiedProperties();
			});

			_menu.ShowAsContext();

		}

		#endregion

	}

}
