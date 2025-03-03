
/*WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW*\     (   (     ) )
|/                                                      \|       )  )   _((_
||  (c) Wanzyee Studio  < wanzyeestudio.blogspot.com >  ||      ( (    |_ _ |=n
|\                                                      /|   _____))   | !  ] U
\.ZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZ./  (_(__(S)   |___*/

using UnityEditor;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using WanzyeeStudio.Extension;

using Object = UnityEngine.Object;

namespace WanzyeeStudio.Editrix{
	
	/// <summary>
	/// Include some convenient methods for editor GUI.
	/// </summary>
	public static class EditrixGUI{

		#region Methods

		/// <summary>
		/// Get the label width within the specified view width.
		/// </summary>
		/// 
		/// <remarks>
		/// Commonly used for a prefix label in a resizable window.
		/// Use <c>EditorGUIUtility.currentViewWidth</c> instead if not assign.
		/// </remarks>
		/// 
		/// <returns>The label width.</returns>
		/// <param name="viewWidth">View width.</param>
		/// 
		public static float GetLabelWidth(float viewWidth = 0f){

			if(0f >= viewWidth) viewWidth = EditorGUIUtility.currentViewWidth;

			return Mathf.Max(viewWidth * 0.45f - 40f, 120f);

		}

		/// <summary>
		/// Set the icon for the specified <c>UnityEngine.Object</c> to show in the Inspector or Project window.
		/// </summary>
		/// 
		/// <remarks>
		/// This wraps the internal <c>EditorGUIUtility.SetIconForObject()</c>:
		/// 	1. It applies to all the same type objects, also the <c>UnityEditor.MonoScript</c> declares the type.
		/// 	2. You may pass the script asset to set without any object instance.
		/// 	3. To restore to default, pass a <c>null</c> icon.
		/// </remarks>
		/// 
		/// <remarks>
		/// The opposite methods below to get the icon:
		/// 	1. <c>AssetPreview.GetMiniThumbnail()</c> to get by <c>UnityEngine.Object</c>.
		/// 	2. <c>AssetPreview.GetMiniTypeThumbnail()</c> to get by <c>System.Type</c>.
		/// 	3. <c>EditorGUIUtility.ObjectContent()</c> to get by both.
		/// </remarks>
		/// 
		/// <param name="obj">Object.</param>
		/// <param name="icon">Icon.</param>
		/// 
		public static void SetIconForObject(Object obj, Texture2D icon){

			if(null == obj) throw new ArgumentNullException("obj");

			var _type = typeof(EditorGUIUtility);
			var _name = "SetIconForObject";

			var _method = _type.GetMethod(_name, true, typeof(void), typeof(Object), typeof(Texture2D));
			if(null == _method) throw new MissingMethodException(_type.FullName, _name);

			_method.Invoke(null, new object[]{obj, icon});

		}

		#endregion


		#region Draw Methods

		/// <summary>
		/// Make a multi-control with text fields for entering multiple floats in the same line.
		/// </summary>
		/// 
		/// <remarks>
		/// Wrap <c>EditorGUI.MultiFloatField()</c> as the GUI layout automatically.
		/// </remarks>
		/// 
		/// <param name="label">Main label.</param>
		/// <param name="subLabels">Sub labels.</param>
		/// <param name="values">Values.</param>
		/// <param name="options">Layout options.</param>
		/// 
		public static void MultiFloatField(
			GUIContent label,
			GUIContent[] subLabels,
			float[] values,
			params GUILayoutOption[] options
		){

			if(null == values) throw new ArgumentNullException("values");

			var _height = EditorGUIUtility.singleLineHeight;
			if(!EditorGUIUtility.wideMode) _height += _height;

			var _none = GUIContent.none;
			var _length = (null == subLabels) ? 0 : subLabels.Length;

			var _labels = values.Select((value, index) => (_length > index) ? (subLabels[index] ?? _none) : _none);
			var _position = EditorGUILayout.GetControlRect(true, _height, options);

			EditorGUI.MultiFloatField(_position, label ?? _none, _labels.ToArray(), values);

		}

		/// <summary>
		/// Make an X, Y, Z and W field for entering a <c>UnityEngine.Vector4</c>.
		/// </summary>
		/// 
		/// <remarks>
		/// Copycat of <c>EditorGUILayout.Vector4Field()</c> but use <c>MultiFloatField()</c>.
		/// Contrast to original, this correct line wrap and indent level.
		/// </remarks>
		/// 
		/// <returns>The new value.</returns>
		/// <param name="label">Label.</param>
		/// <param name="value">Value.</param>
		/// <param name="options">Layout options.</param>
		/// 
		public static Vector4 Vector4Field(GUIContent label, Vector4 value, params GUILayoutOption[] options){

			var _labels = new []{"X", "Y", "Z", "W"}.Select((text) => new GUIContent(text)).ToArray();
			var _values = new []{value.x, value.y, value.z, value.w};

			MultiFloatField(label, _labels, _values, options);
			return new Vector4(_values[0], _values[1], _values[2], _values[3]);

		}

		/// <summary>
		/// Make an X, Y, Z and W field for entering a <c>UnityEngine.Quaternion</c>.
		/// </summary>
		/// 
		/// <remarks>
		/// Just looks like <c>Vector4Field()</c>.
		/// </remarks>
		/// 
		/// <returns>The new value.</returns>
		/// <param name="label">Label.</param>
		/// <param name="value">Value.</param>
		/// <param name="options">Layout options.</param>
		/// 
		public static Quaternion QuaternionField(GUIContent label, Quaternion value, params GUILayoutOption[] options){
			
			var _labels = new []{"X", "Y", "Z", "W"}.Select((text) => new GUIContent(text)).ToArray();
			var _values = new []{value.x, value.y, value.z, value.w};

			MultiFloatField(label, _labels, _values, options);
			return new Quaternion(_values[0], _values[1], _values[2], _values[3]);

		}

		/// <summary>
		/// Make a toolbar style search field, optional to set a callback invoked when click the magnifier popup icon.
		/// </summary>
		/// <returns>The new text.</returns>
		/// <param name="text">The text to edit.</param>
		/// <param name="onPopup">The popup callback.</param>
		/// <param name="options">Layout options.</param>
		public static string SearchField(string text, Action onPopup = null, params GUILayoutOption[] options){

			var _position = new Rect(0f, 0f, 21f, EditorGUIUtility.singleLineHeight);
			EditorGUIUtility.AddCursorRect(_position, MouseCursor.Arrow);

			var _popup = (null != onPopup) && GUI.Button(_position, new GUIContent("", "Search..."), GUIStyle.none);
			var _style = (GUIStyle)((null == onPopup) ? "ToolbarSeachTextField" : "ToolbarSeachTextFieldPopup");

			var _options = new []{GUILayout.MaxWidth(285)};
			if(null != options) _options = _options.Concat(options).ToArray();

			GUILayout.BeginHorizontal();
			text = EditorGUILayout.TextField(text ?? "", _style, _options);

			var _cancel = GUILayout.Button("", "ToolbarSeachCancelButton");
			EditorGUILayout.Space();

			if(_popup || _cancel) GUI.FocusControl(null);
			if(_popup) onPopup.Invoke();

			GUILayout.EndHorizontal();
			return _cancel ? "" : text;

		}

		/// <summary>
		/// Display an error message after the prefix label.
		/// </summary>
		/// <param name="position">Position.</param>
		/// <param name="label">Label.</param>
		/// <param name="error">Error message.</param>
		public static void ErrorField(Rect position, GUIContent label, GUIContent error){

			EditorGUI.LabelField(new Rect(position){ width = EditorGUIUtility.labelWidth }, label);

			position.xMin += EditorGUIUtility.labelWidth;

			EditorGUI.HelpBox(position, error.text, MessageType.Error);

			EditorGUI.LabelField(position, new GUIContent("", error.tooltip));

		}

		#endregion


		#region Drag Methods

		/// <summary>
		/// Check if Drag'n'Drop performed with the filtered dragged <c>UnityEngine.Object</c> references.
		/// </summary>
		/// <returns>The filtered objects, only valid when drag performed, otherwise <c>null</c>.</returns>
		/// <param name="filter">The callback invoked to filter each object only when drag updated.</param>
		public static Object[] CheckDragObjects(Func<Object, bool> filter){
			
			if(null == filter) throw new ArgumentNullException("filter");
			return CheckDragObjects((objects) => objects.Where((obj) => filter.Invoke(obj)));

		}

		/// <summary>
		/// Check if Drag'n'Drop performed with the filtered dragged <c>UnityEngine.Object</c> references.
		/// </summary>
		/// <returns>The filtered objects, only valid when drag performed, otherwise <c>null</c>.</returns>
		/// <param name="filter">The callback invoked to filter all objects only when drag updated.</param>
		public static Object[] CheckDragObjects(Func<IEnumerable<Object>, IEnumerable<Object>> filter = null){
			return CheckDrag(DragAndDrop.objectReferences, filter);
		}

		/// <summary>
		/// Check if Drag'n'Drop performed with the filtered dragged <c>string</c> paths.
		/// </summary>
		/// <returns>The filtered paths, only valid when drag performed, otherwise <c>null</c>.</returns>
		/// <param name="filter">The callback invoked to filter each path only when drag updated.</param>
		public static string[] CheckDragPaths(Func<string, bool> filter){
			
			if(null == filter) throw new ArgumentNullException("filter");
			return CheckDragPaths((paths) => paths.Where((path) => filter.Invoke(path)));

		}

		/// <summary>
		/// Check if Drag'n'Drop performed with the filtered dragged <c>string</c> paths.
		/// </summary>
		/// <returns>The filtered paths, only valid when drag performed, otherwise <c>null</c>.</returns>
		/// <param name="filter">The callback invoked to filter all paths only when drag updated.</param>
		public static string[] CheckDragPaths(Func<IEnumerable<string>, IEnumerable<string>> filter = null){
			return CheckDrag(DragAndDrop.paths, filter);
		}

		/// <summary>
		/// Check if Drag'n'Drop performed with the filtered dragged sources.
		/// </summary>
		/// <returns>The filtered sources, only valid when drag performed, otherwise <c>null</c>.</returns>
		/// <param name="sources">The original dragged sources.</param>
		/// <param name="filter">The callback invoked to filter only when drag updated.</param>
		/// <typeparam name="T">The value type.</typeparam>
		/*
		 * Provide a common template to handle Event and DragAndDrop in ordered.
		 * Use callback filter to reduce filtering, e.g., when EventType.Repaint or EventType.Layout, etc.
		 * The attribute is 'coz of ConfuserEx breaking the trace to invoke this with unknown reason.
		 */
		[Obfuscation(Exclude = true)]
		private static T[] CheckDrag<T>(T[] sources, Func<IEnumerable<T>, IEnumerable<T>> filter){

			var _type = Event.current.type;
			if(EventType.DragUpdated != _type && EventType.DragPerform != _type) return null;

			var _result = (null == filter) ? sources : filter.Invoke(sources).ToArray();
			DragAndDrop.visualMode = _result.Any() ? DragAndDropVisualMode.Copy : DragAndDropVisualMode.Rejected;

			Event.current.Use();
			if(!_result.Any() || EventType.DragPerform != _type) return null;

			DragAndDrop.AcceptDrag();
			return _result;

		}

		#endregion

	}

}
