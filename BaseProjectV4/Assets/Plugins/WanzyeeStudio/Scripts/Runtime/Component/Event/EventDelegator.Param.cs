
/*WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW*\     (   (     ) )
|/                                                      \|       )  )   _((_
||  (c) Wanzyee Studio  < wanzyeestudio.blogspot.com >  ||      ( (    |_ _ |=n
|\                                                      /|   _____))   | !  ] U
\.ZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZ All rights reserved./  (_(__(S)   |___*/

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Object = UnityEngine.Object;

namespace WanzyeeStudio{
	
	public partial class EventDelegator{
		
		/// <summary>
		/// Serializable data struct to store the type and the value of a parameter.
		/// </summary>
		/*
		 * To make a custom serializable type supported is easy.
		 * Just add a field below in the format as others: "public someClass[] someName = null;".
		 * But not to rename the originals.
		 */
		[Serializable]
		private class Param{

			#region Value Fields

			/// <summary>
			/// The values of <c>enum</c>.
			/// </summary>
			public int[] valueEnums = {};

			/// <summary>
			/// The values of <c>bool</c>.
			/// </summary>
			public bool[] valueBools = {};

			/// <summary>
			/// The values of <c>string</c>.
			/// </summary>
			public string[] valueStrings = {};

			/// <summary>
			/// The values of <c>int</c>.
			/// </summary>
			public int[] valueInts = {};

			/// <summary>
			/// The values of <c>long</c>.
			/// </summary>
			public long[] valueLongs = {};

			/// <summary>
			/// The values of <c>float</c>.
			/// </summary>
			public float[] valueFloats = {};

			/// <summary>
			/// The values of <c>double</c>.
			/// </summary>
			public double[] valueDoubles = {};

			/// <summary>
			/// The values of <c>UnityEngine.Object</c>.
			/// </summary>
			public Object[] valueObjects = {};

			/// <summary>
			/// The values of <c>UnityEngine.Vector2</c>.
			/// </summary>
			public Vector2[] valueVector2s = {};

			/// <summary>
			/// The values of <c>UnityEngine.Vector3</c>.
			/// </summary>
			public Vector3[] valueVector3s = {};

			/// <summary>
			/// The values of <c>UnityEngine.Vector4</c>.
			/// </summary>
			public Vector4[] valueVector4s = {};

			/// <summary>
			/// The values of <c>UnityEngine.Quaternion</c>.
			/// </summary>
			public Quaternion[] valueQuaternions = {};

			/// <summary>
			/// The values of <c>UnityEngine.Matrix4x4</c>.
			/// </summary>
			public Matrix4x4[] valueMatrixs = {};

			/// <summary>
			/// The values of <c>UnityEngine.Rect</c>.
			/// </summary>
			public Rect[] valueRects = {};

			/// <summary>
			/// The values of <c>UnityEngine.RectOffset</c>.
			/// </summary>
			public RectOffset[] valueOffsets = {};

			/// <summary>
			/// The values of <c>UnityEngine.Bounds</c>.
			/// </summary>
			public Bounds[] valueBounds = {};

			/// <summary>
			/// The values of <c>UnityEngine.Color</c>.
			/// </summary>
			public Color[] valueColors = {};

			/// <summary>
			/// The values of <c>UnityEngine.Color32</c>.
			/// </summary>
			public Color32[] valueColor32s = {};

			/// <summary>
			/// The values of <c>UnityEngine.Gradient</c>.
			/// </summary>
			public Gradient[] valueGradients = {};

			/// <summary>
			/// The values of <c>UnityEngine.AnimationCurve</c>.
			/// </summary>
			public AnimationCurve[] valueCurves = {};

			/// <summary>
			/// The values of <c>UnityEngine.LayerMask</c>.
			/// </summary>
			public LayerMask[] valueLayers = {};

			/// <summary>
			/// The values of <c>UnityEngine.GUIContent</c>.
			/// </summary>
			public GUIContent[] valueContent = {};

			#endregion


			#region Static

			/// <summary>
			/// The types paired with <c>FieldInfo</c> stores corresponding parameter value.
			/// </summary>
			private static Dictionary<Type, FieldInfo> _fields = typeof(Param).GetFields(
				
				).Where((field) => field.FieldType.IsArray
				
				).ToDictionary((field) => (field.Name == "valueEnums") ? typeof(Enum) : field.FieldType.GetElementType()
				
			);

			/// <summary>
			/// Determine if the parameter type is supported, i.e., included to serialize and draw GUI.
			/// </summary>
			/// <returns><c>true</c> if is supported; otherwise, <c>false</c>.</returns>
			/// <param name="type">Type.</param>
			public static bool CheckSupported(Type type){

				if(null == type) return false;

				type = GetElementType(type) ?? type;

				return _fields.Any((pair) => pair.Key.IsAssignableFrom(type));

			}

			/// <summary>
			/// Get the element type of an array or list type.
			/// </summary>
			/// <returns>The element type.</returns>
			/// <param name="type">Type.</param>
			private static Type GetElementType(Type type){

				if(type.IsArray && 1 == type.GetArrayRank()) return type.GetElementType();

				if(!type.IsGenericType || typeof(List<>) != type.GetGenericTypeDefinition()) return null;

				return type.GetGenericArguments()[0];

			}

			#endregion


			#region Instance

			/// <summary>
			/// The qualified name of parameter type.
			/// </summary>
			[Tooltip("Qualified name of parameter type")]
			public string type;

			/// <summary>
			/// The field name to get value, also as the path for editor getting the serialized property.
			/// </summary>
			[Tooltip("Property path to the values corresponding to current type.")]
			public string path;

			/// <summary>
			/// Initialize this by specified type and value if valid.
			/// </summary>
			/// <returns>The instance.</returns>
			/// <param name="type">Type.</param>
			/// <param name="value">Value.</param>
			public Param(Type type, object value){

				if(null == type) throw new ArgumentNullException("type");

				if(null != value && !type.IsInstanceOfType(value))
					throw new ArgumentException("The value doesn't match the type.", "value");

				var _type = GetElementType(type) ?? type;
				var _field = _fields.FirstOrDefault((pair) => pair.Key.IsAssignableFrom(_type)).Value;
				if(null == _field) throw new NotSupportedException("Not support given param type.");

				if(type.IsValueType && null == value) value = Activator.CreateInstance(type);
				if(!type.IsArray && !type.IsGenericType) value = new []{value};

				var _array = (null == value) ? new ArrayList() : new ArrayList(value as ICollection);
				_field.SetValue(this, _array.ToArray(_field.FieldType.GetElementType()));

				path = _field.Name;
				this.type = type.AssemblyQualifiedName;

			}

			/// <summary>
			/// Get the current parameter value.
			/// </summary>
			/// <returns>The value.</returns>
			public object GetValue(){

				var _type = Type.GetType(type);

				if(_type.IsEnum) return Enum.ToObject(_type, valueEnums[0]);

				var _array = typeof(Param).GetField(path).GetValue(this) as Array;
				if(_array == valueObjects) CheckObjects(GetElementType(_type) ?? _type);

				if(!_type.IsArray && !_type.IsGenericType) return _array.GetValue(0);

				_array = new ArrayList(_array).ToArray(GetElementType(_type));
				return _type.IsArray ? _array : Activator.CreateInstance(_type, _array);

			}

			/// <summary>
			/// Check and clean up the <c>UnityEngine.Object</c> array by specified type.
			/// In case assigning invalid object in debug Inspector.
			/// </summary>
			/// <param name="type">Type.</param>
			private void CheckObjects(Type type){

				for(int i = 0; i < valueObjects.Length; i++){

					var _object = valueObjects[i];

					if(null != _object && !type.IsInstanceOfType(_object)) valueObjects[i] = null;

				}

			}

			#endregion

		}

	}

}
