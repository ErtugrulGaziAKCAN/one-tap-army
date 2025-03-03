
/*WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW*\     (   (     ) )
|/                                                      \|       )  )   _((_
||  (c) Wanzyee Studio  < wanzyeestudio.blogspot.com >  ||      ( (    |_ _ |=n
|\                                                      /|   _____))   | !  ] U
\.ZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZ All rights reserved./  (_(__(S)   |___*/

using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

using WanzyeeStudio.Editrix.Extension;

namespace WanzyeeStudio.Editrix.Drawer{
	
	/// <summary>
	/// <c>UnityEditor.CustomEditor</c> for <c>EventDelegatorGroup</c>.
	/// </summary>
	/// 
	/// <remarks>
	/// Draw the GUI with reorderable list and readable delegator names.
	/// </remarks>
	/// 
	[CustomEditor(typeof(EventDelegatorGroup))]
	internal class EventDelegatorGroupEditor : Editor{

		#region Static

		/// <summary>
		/// The dictionary to convert escaped parameter <c>string</c> to display.
		/// </summary>
		/*
		 * http://stackoverflow.com/q/323640
		 */
		private static Dictionary<string, string> _escapes = new Dictionary<string, string>{
			{"\\", @"\\"}, {"\'", @"\'"}, {"\"", "\\\""}, {"\0", @"\0"}, {"\a", @"\a"}, {"\b", @"\b"},
			{"\f", @"\f"}, {"\n", @"\n"}, {"\r", @"\r"}, {"\t", @"\t"}, {"\v", @"\v"}
		};

		/// <summary>
		/// Get the <c>string</c> represents the value.
		/// </summary>
		/// <returns>The string.</returns>
		/// <param name="value">Value.</param>
		private static string GetString(object value){

			if(null == value) return "null";

			if(!(value is string)) return value.ToString();

			var _result = value as string;

			if(_escapes.Any((pair) => _result.Contains(pair.Key))){
				foreach(var _pair in _escapes) _result = _result.Replace(_pair.Key, _pair.Value);
			}

			return "\"" + _result + "\"";

		}

		/// <summary>
		/// Get the label with method information by specified delegator.
		/// </summary>
		/// <returns>The label.</returns>
		/// <param name="delegator">Delegator.</param>
		private static GUIContent GetLabel(EventDelegator delegator){

			var _method = (null == delegator) ? null : delegator.methodInfo;

			if(null == _method) return new GUIContent("None");

			var _instance = ((null == delegator.instance) ? "null" : delegator.instance.name) + "\n";
			var _name = (_method.IsStatic ? "" : _instance) + _method.ReflectedType.Name + "." + _method.Name;

			if(!delegator.parameters.Any()) return new GUIContent(_method.Name, _name + "()");

			var _params = delegator.parameters.Select((value) => GetString(value)).ToArray();
			var _tooltip = string.Format("{0}(\n    {1}\n)", _name, string.Join(",\n    ", _params));

			return new GUIContent(_method.Name, _tooltip);

		}

		#endregion


		#region Instance

		/// <summary>
		/// The reorderable list to arrange <c>UnityEditor.SerializedProperty</c> of <c>delegators</c>.
		/// </summary>
		private ReorderableList _list;

		/// <summary>
		/// OnEnable, setup the list.
		/// </summary>
		private void OnEnable(){
			_list = ReorderableListExpander.Create(serializedObject.FindProperty("delegators"), DrawElement);
		}

		/// <summary>
		/// OnInspectorGUI, draw inspector of the editor.
		/// </summary>
		/// 
		/// <remarks>
		/// Include the reorderable list of <c>delegators</c>, and draw other properties normally.
		/// Finally check to append dragged delegator, as normal array behaviour on Inspector.
		/// </remarks>
		/// 
		public override void OnInspectorGUI(){

			serializedObject.Update();
			GUILayout.Space(4f);

			EditorGUILayout.PropertyField(serializedObject.FindProperty("invokeAbove"));
			EditorGUILayout.PropertyField(serializedObject.FindProperty("invokeBelow"));

			var _rect = GUILayoutUtility.GetLastRect();
			_rect.y = _rect.yMax + EditorGUIUtility.standardVerticalSpacing;

			_list.DoLayoutList();

			_rect.yMax = GUILayoutUtility.GetLastRect().y;
			if(_rect.Contains(Event.current.mousePosition)) CheckDrag();

			GUILayout.Space(2f);
			serializedObject.ApplyModifiedProperties();

		}

		/// <summary>
		/// Draw the element of delegators, callback for reorderable list.
		/// </summary>
		/// <param name="rect">Rect.</param>
		/// <param name="index">Index.</param>
		/// <param name="isActive">If set to <c>true</c> is active.</param>
		/// <param name="isFocused">If set to <c>true</c> is focused.</param>
		private void DrawElement(Rect rect, int index, bool isActive, bool isFocused){

			var _property = _list.serializedProperty.GetArrayElementAtIndex(index);
			var _position = new Rect(rect.x, rect.y + 2f, rect.width, EditorGUIUtility.singleLineHeight);

			var _width = EditorGUIUtility.labelWidth;
			EditorGUIUtility.labelWidth = _width - 20f;

			_position = EditorGUI.PrefixLabel(_position, GetLabel(_property.objectReferenceValue as EventDelegator));
			EditorGUI.PropertyField(_position, _property, GUIContent.none);

			EditorGUIUtility.labelWidth = _width;

		}

		/// <summary>
		/// Check if Drag'n'Drop valid, append new delegators.
		/// </summary>
		private void CheckDrag(){

			var _delegators = EditrixGUI.CheckDragObjects((objects) => {
				
				var _components = objects.OfType<GameObject>().SelectMany((obj) => obj.GetComponents<EventDelegator>());
				return objects.OfType<EventDelegator>().Union(_components).Cast<Object>();

			});

			if(null != _delegators) serializedObject.FindProperty("delegators").AddRange(_delegators);

		}

		#endregion

	}

}
