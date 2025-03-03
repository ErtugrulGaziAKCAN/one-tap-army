
/*WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW*\     (   (     ) )
|/                                                      \|       )  )   _((_
||  (c) Wanzyee Studio  < wanzyeestudio.blogspot.com >  ||      ( (    |_ _ |=n
|\                                                      /|   _____))   | !  ] U
\.ZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZ./  (_(__(S)   |___*/

using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;

using Object = UnityEngine.Object;

namespace WanzyeeStudio.Extension{
	
	/// <summary>
	/// Extension methods for <c>UnityEngine.Component</c>.
	/// </summary>
	public static class ComponentExtension{

		#region Transform

		/// <summary>
		/// Get the hierarchy path, used for log or <c>GameObject.Find()</c> with the default separator.
		/// </summary>
		/// <returns>The hierarchy path.</returns>
		/// <param name="transform">Transform.</param>
		/// <param name="separator">Separator.</param>
		public static string GetPath(this Transform transform, string separator = "/"){

			if(null == transform) throw new ArgumentNullException("transform");

			var _hierarchy = transform.GetComponentsInParent<Transform>(true);

			return string.Join(separator, _hierarchy.Reverse().Select((obj) => obj.name).ToArray());

		}

		/// <summary>
		/// Transforms rotation from local space to world space.
		/// </summary>
		/// <returns>The world rotation.</returns>
		/// <param name="transform">Transform.</param>
		/// <param name="rotation">Local rotation.</param>
		public static Quaternion TransformQuat(this Transform transform, Quaternion rotation){
			
			if(null == transform) throw new ArgumentNullException("transform");

			return rotation * transform.rotation;

		}

		/// <summary>
		/// Transforms position from world space to local space, opposite of <c>TransformQuat()</c>.
		/// </summary>
		/// <returns>The local rotation.</returns>
		/// <param name="transform">Transform.</param>
		/// <param name="rotation">World rotation.</param>
		public static Quaternion InverseTransformQuat(this Transform transform, Quaternion rotation){
			
			if(null == transform) throw new ArgumentNullException("transform");

			return rotation * Quaternion.Inverse(transform.rotation);

		}

		#endregion


		#region UI

		/// <summary>
		/// Convert the center of a specified <c>UnityEngine.RectTransform</c> to screen space point.
		/// </summary>
		/// <returns>The screen point.</returns>
		/// <param name="transform">Transform.</param>
		public static Vector2 CenterToScreenPoint(this RectTransform transform){

			if(null == transform) throw new ArgumentNullException("transform");

			var _canvas = transform.GetComponentsInParent<Canvas>().LastOrDefault();
			if(null == _canvas) throw new MissingComponentException("No root Canvas found.");

			var _corners = new Vector3[4];
			transform.GetWorldCorners(_corners);
			var _center = (_corners[0] + _corners[2]) * 0.5f;

			if(RenderMode.ScreenSpaceOverlay == _canvas.renderMode) return _center;
			else return (_canvas.worldCamera ?? Camera.main).WorldToScreenPoint(_center);

		}

		/// <summary>
		/// Set the sorting order of the UI element inside a <c>UnityEngine.Canvas</c>.
		/// </summary>
		/// 
		/// <remarks>
		/// This only apply the sub canvas in a root canvas.
		/// Check to add it with <c>UnityEngine.UI.GraphicRaycaster</c> if not existing.
		/// </remarks>
		/// 
		/// <param name="component">Component.</param>
		/// <param name="order">Sorting order.</param>
		/// 
		public static void SetSorting(this Component component, int order){

			if(null == component) throw new ArgumentNullException("component");

			var _root = component.GetComponentsInParent<Canvas>().LastOrDefault();
			var _canvas = component.GetComponent<Canvas>();

			if(null == _root || _canvas == _root) throw new InvalidOperationException("Component isn't in a Canvas.");
			if(null != _canvas && order == _canvas.sortingOrder) return;
			if(null == _canvas && order == _root.sortingOrder) return;

			if(order == _root.sortingOrder && HideFlags.DontSave == _canvas.hideFlags){
				Object.Destroy(_canvas.GetComponent<GraphicRaycaster>());
				Object.Destroy(_canvas);
				return;
			}

			if(null == _canvas){
				_canvas = component.gameObject.AddComponent<Canvas>();
				_canvas.hideFlags = HideFlags.DontSave;
				if(null != _root.GetComponent<GraphicRaycaster>()) _canvas.gameObject.AddComponent<GraphicRaycaster>();
			}

			if(order != _root.sortingOrder) _canvas.overrideSorting = true;
			_canvas.sortingOrder = order;

		}

		#endregion

	}

}
