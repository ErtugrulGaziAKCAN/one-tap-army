
/*WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW*\     (   (     ) )
|/                                                      \|       )  )   _((_
||  (c) Wanzyee Studio  < wanzyeestudio.blogspot.com >  ||      ( (    |_ _ |=n
|\                                                      /|   _____))   | !  ] U
\.ZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZ All rights reserved./  (_(__(S)   |___*/

using UnityEngine;
using System;
using System.Linq;
using System.Reflection;

using Object = UnityEngine.Object;

namespace WanzyeeStudio{
	
	/// <summary>
	/// Wrap a method to be invoked by <c>UnityEngine.Events.UnityEvent</c>.
	/// </summary>
	/// 
	/// <remarks>
	/// Support static method and multiple parameters which can be edited in the Inspector GUI.
	/// Set the instance to invoke, or <c>null</c> for a static method, then pick the method by the popup menu.
	/// Trick to select an editor static method, press "Ctrl" key when click the menu.
	/// </remarks>
	/// 
	/// <remarks>
	/// The parameter value, array, or list, of types below are supported:
	/// 	- <c>enum</c>, <c>bool</c>, <c>string</c>, <c>int</c>, <c>long</c>, <c>float</c>, <c>double</c>.
	/// 	- <c>UnityEngine.Object</c>, include others derive from it.
	/// 	- <c>UnityEngine.Vector2</c>, <c>UnityEngine.Vector3</c>, <c>UnityEngine.Vector4</c>.
	/// 	- <c>UnityEngine.Quaternion</c>, <c>UnityEngine.Matrix4x4</c>.
	/// 	- <c>UnityEngine.Rect</c>, <c>UnityEngine.RectOffset</c>, <c>UnityEngine.Bounds</c>.
	/// 	- <c>UnityEngine.Color</c>, <c>UnityEngine.Color32</c>, <c>UnityEngine.Gradient</c>.
	/// 	- <c>UnityEngine.AnimationCurve</c>, <c>UnityEngine.LayerMask</c>, <c>UnityEngine.GUIContent</c>.
	/// </remarks>
	/// 
	[HelpURL("https://git.io/viqRc")]
	public partial class EventDelegator : MonoBehaviour, ISerializationCallbackReceiver{

		#region Static

		/// <summary>
		/// Determine if the specified method is supported, i.e., non-generic and all parameter types are supported.
		/// </summary>
		/// <returns><c>true</c> if is supported; otherwise, <c>false</c>.</returns>
		/// <param name="method">Method.</param>
		public static bool IsSupported(MethodInfo method){

			if(null == method || method.IsGenericMethod) return false;

			return method.GetParameters().All((param) => Param.CheckSupported(param.ParameterType));

		}

		/// <summary>
		/// Check if the method, instance and parameters are valid and matched, otherwise throw exception.
		/// </summary>
		/// <param name="methodInfo">Method info.</param>
		/// <param name="instance">Instance.</param>
		/// <param name="parameters">Parameters.</param>
		private static void CheckValid(MethodInfo methodInfo, Object instance, object[] parameters){

			if(null == methodInfo) throw new ArgumentNullException("methodInfo");
			if(methodInfo.IsGenericMethod) throw new NotSupportedException("Not support generic method.");

			var _static = methodInfo.IsStatic;
			if(!_static && null == instance) throw new TargetException("Non-static method requires an instance.");

			if(!_static && !methodInfo.DeclaringType.IsInstanceOfType(instance))
				throw new TargetException("Instance does not match method target type.");

			var _types = methodInfo.GetParameters().Select((param) => param.ParameterType).ToArray();
			if(_types.Any((type) => !Param.CheckSupported(type))) throw new NotSupportedException("Not support param.");

			if(_types.Length != parameters.Length)
				throw new TargetParameterCountException("Params number doesn't match the method.");

			var _params = parameters.Where((value, index) => null != value && !_types[index].IsInstanceOfType(value));
			if(_params.Any()) throw new ArgumentException("Params includes value of wrong type.");

		}

		#endregion


		#region Fields

		/// <summary>
		/// The instance target to invoke the method, also as flag to select static method if <c>null</c>.
		/// </summary>
		[Obfuscation(Exclude = true)]
		[SerializeField]
		[Tooltip("Instance to invoke method, or null for static.")]
		[TypeConstraint]
		private Object _instance;

		/// <summary>
		/// The qualified name of method's reflected or declaring type.
		/// </summary>
		[Obfuscation(Exclude = true)]
		[SerializeField]
		[Tooltip("Qualified name of method's reflected or declaring type.")]
		private string _type = "";

		/// <summary>
		/// The method name.
		/// </summary>
		[Obfuscation(Exclude = true)]
		[SerializeField]
		[Tooltip("Method name.")]
		private string _method = "";

		/// <summary>
		/// The array to store the type and the value of parameters.
		/// </summary>
		[Obfuscation(Exclude = true)]
		[SerializeField]
		[Tooltip("Parameters with each type and value.")]
		private Param[] _params = {};

		/// <summary>
		/// The flag if currently invoking, used to check looping for safety.
		/// </summary>
		private bool _invoking;

		#endregion


		#region Properties

		/// <summary>
		/// Get the method instance target.
		/// </summary>
		/// <value>The instance.</value>
		public Object instance{
			get{ return (null == methodInfo || methodInfo.IsStatic) ? null : _instance; }
		}

		/// <summary>
		/// Get the <c>MethodInfo</c> found with serialized type, name, and parameter types.
		/// </summary>
		/// <value>The method info.</value>
		public MethodInfo methodInfo{ get; private set; }

		/// <summary>
		/// Get the parameter values passed to the method.
		/// </summary>
		/// <value>The parameters.</value>
		public object[] parameters{ get; private set; }

		/// <summary>
		/// Get the result returned by the last time method invoking.
		/// </summary>
		/// <value>The result.</value>
		public object result{ get; private set; }

		#endregion


		#region Methods

		/// <summary>
		/// Start, declare to show the enabled toggle.
		/// </summary>
		private void Start(){}

		/// <summary>
		/// OnAfterDeserialize, find the method and build the parameters by serialized settings.
		/// </summary>
		public void OnAfterDeserialize(){

			methodInfo = null;
			parameters = new object[0];

			if(string.IsNullOrEmpty(_type) || null == Type.GetType(_type)) return;

			var _types = _params.Select((param) => Type.GetType(param.type)).ToArray();
			var _flag = BindingFlags.Static | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

			var _info = Type.GetType(_type).GetMethod(_method, _flag, null, _types, null);
			if(null == _info) return;

			methodInfo = _info;
			parameters = _params.Select((param) => param.GetValue()).ToArray();

		}

		/// <summary>
		/// OnBeforeSerialize, serialize setting fields by existing method and parameters.
		/// </summary>
		public void OnBeforeSerialize(){
			
			var _info = methodInfo;
			if(null == _info && string.IsNullOrEmpty(_type)) return;

			_type = (null == _info) ? "" : _info.ReflectedType.AssemblyQualifiedName;
			_method = (null == _info) ? "" : _info.Name;

			var _types = (null == _info) ? new Type[0] : _info.GetParameters().Select((param) => param.ParameterType);
			_params = _types.Select((type, index) => new Param(type, parameters[index])).ToArray();

		}

		/// <summary>
		/// Set the method, instance target, and the parameter values manually.
		/// </summary>
		/// <param name="methodInfo">Method info.</param>
		/// <param name="instance">Instance.</param>
		/// <param name="parameters">Parameters.</param>
		public void SetMethod(MethodInfo methodInfo, Object instance, params object[] parameters){

			if(null == methodInfo || null == parameters) parameters = new object[0];
			if(null != methodInfo) CheckValid(methodInfo, instance, parameters);

			this.methodInfo = methodInfo;
			this.parameters = parameters;

			_instance = instance;
			result = null;

		}

		/// <summary>
		/// Invoke the method set by this if enabled.
		/// </summary>
		public void Invoke(){
			Raise<object>();
		}

		/// <summary>
		/// Invoke the method set by this with returned value if enabled, otherwise return the default value.
		/// </summary>
		/// <returns>The value returned by the method.</returns>
		/// <typeparam name="T">The method return value type.</typeparam>
		public T Raise<T>(){
			
			if(!enabled || null == methodInfo) return default(T);
			if(_invoking) throw new OverflowException("Looping method call.");

			if(!typeof(T).IsAssignableFrom(methodInfo.ReturnType)){
				var _format = "Can't cast from method return type {0} to destination type {1}.";
				throw new InvalidCastException(string.Format(_format, methodInfo.ReturnType, typeof(T)));
			}

			_invoking = true;

			try{ result = methodInfo.Invoke(methodInfo.IsStatic ? null : _instance, parameters); }
			finally{ _invoking = false; }

			return (null == result) ? default(T) : (T)result;

		}

		#endregion

	}

}
