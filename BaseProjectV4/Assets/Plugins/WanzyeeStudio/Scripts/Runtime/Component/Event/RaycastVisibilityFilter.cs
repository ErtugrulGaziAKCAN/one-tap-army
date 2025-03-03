
/*WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW*\     (   (     ) )
|/                                                      \|       )  )   _((_
||  (c) Wanzyee Studio  < wanzyeestudio.blogspot.com >  ||      ( (    |_ _ |=n
|\                                                      /|   _____))   | !  ] U
\.ZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZ./  (_(__(S)   |___*/

using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;

namespace WanzyeeStudio{
	
	/// <summary>
	/// Filter raycasts by the <c>alpha</c> and <c>proportion</c> inside the <c>viewport</c>.
	/// </summary>
	[HelpURL("https://git.io/viqRz")]
	[RequireComponent(typeof(RectTransform))]
	public class RaycastVisibilityFilter : MonoBehaviour, ICanvasRaycastFilter{

		#region Fields

		/// <summary>
		/// The viewport contains this to calculate the proportion.
		/// </summary>
		[Tooltip("Viewport contains this to calculate the proportion.")]
		public RectTransform viewport;

		/// <summary>
		/// The minimum visibility to pass raycasts.
		/// </summary>
		[Range(0f, 1f)]
		[Tooltip("Minimum visibility to pass raycasts.")]
		public float threshold;

		/// <summary>
		/// The sampling frequency to calculate the proportion when this rotates relative to the <c>viewport</c>.
		/// </summary>
		[Range(7, 32)]
		[Tooltip("Sample frequency used when rotates relative to the viewport.")]
		public int sample = 10;

		/// <summary>
		/// The stored <c>UnityEngine.RectTransform</c> to reuse.
		/// </summary>
		private RectTransform _transform;

		/// <summary>
		/// The array to fill this's corners, used by <c>RectTransform.GetWorldCorners()</c>.
		/// </summary>
		private Vector3[] _corners = new Vector3[4];

		#endregion


		#region Properties

		/// <summary>
		/// Get the <c>UnityEngine.UI.Graphic</c> with this, throw exception if none.
		/// </summary>
		/// <value>The graphic component.</value>
		public Graphic graphic{
			get{
				var _result = GetComponent<Graphic>();
				if(null != _result) return _result;
				throw new MissingComponentException("Require a component of UnityEngine.UI.Graphic.");
			}
		}

		/// <summary>
		/// Get the alpha of the <c>UnityEngine.UI.Graphic</c> with this.
		/// </summary>
		/// <value>The alpha.</value>
		public float alpha{
			get{
				var _graphic = graphic;
				var _alpha = _graphic.canvasRenderer.GetAlpha();
				return (0f < _alpha) ? (_alpha * _graphic.color.a) : 0f;
			}
		}

		/// <summary>
		/// Get the <c>UnityEngine.RectTransform</c> proportion inside the <c>viewport</c>.
		/// </summary>
		/// 
		/// <remarks>
		/// Use parent <c>UnityEngine.UI.Mask</c> or <c>UnityEngine.Canvas</c> instead of <c>viewport</c> if not set.
		/// Or return 0 if there's none of the above.
		/// </remarks>
		/// 
		/// <value>The proportion.</value>
		/// 
		public float proportion{
			
			get{
				
				if(null != viewport) return GetProportion(viewport);

				var _mask = GetComponentInParent<Mask>();
				if(null != _mask && _mask.enabled) return GetProportion(_mask.rectTransform);

				var _canvas = graphic.canvas;
				return (null == _canvas) ? 0f : GetProportion(_canvas.GetComponent<RectTransform>());

			}

		}

		/// <summary>
		/// Get the visibility, i.e., the <c>alpha</c> multiply the <c>proportion</c>.
		/// </summary>
		/// 
		/// <remarks>
		/// Only valid when the <c>graphic</c> is active and enabled in the hierarchy, otherwise return 0.
		/// </remarks>
		/// 
		/// <value>The visibility.</value>
		/// 
		public float visibility{
			
			get{
				
				var _graphic = graphic;
				if(!_graphic.isActiveAndEnabled || null == _graphic.canvas || !_graphic.canvas.enabled) return 0f;

				var _alpha = alpha;
				return (0f < _alpha) ? (_alpha * proportion) : 0f;

			}

		}

		#endregion


		#region Methods

		/// <summary>
		/// Check if the raycast valid, i.e., the <c>visibility</c> is over the <c>threshold</c>.
		/// </summary>
		/// <returns><c>true</c> if raycast valid; otherwise, <c>false</c>.</returns>
		/// <param name="sp">Screen position.</param>
		/// <param name="eventCamera">Raycast camera.</param>
		public bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera){
			return visibility >= threshold;
		}

		/// <summary>
		/// Get the visible area proportion inside the <c>view</c>.
		/// </summary>
		/// <returns>The proportion.</returns>
		/// <param name="view">View.</param>
		/*
		 * The world corners are filled by the order: bottom-left, top-left, top-right, bottom-right.
		 */
		private float GetProportion(RectTransform view){

			if(null == _transform) _transform = GetComponent<RectTransform>();

			_transform.GetWorldCorners(_corners);
			for(int i = 0; i < 4; i++) _corners[i] = view.InverseTransformPoint(_corners[i]);

			var _view = view.rect;
			if(_corners.All(_view.Contains)) return 1f;

			var _aligned = (_transform.rotation == view.rotation);
			return _aligned ? GetAligned(_corners[0], _corners[2], _view.min, _view.max) : GetRotated(view);

		}

		/// <summary>
		/// Get the proportion when the rect rotates align to the view.
		/// </summary>
		/// <returns>The aligned proportion.</returns>
		/// <param name="rMin">Rect minimum corner.</param>
		/// <param name="rMax">Rect maximum corner.</param>
		/// <param name="vMin">view minimum corner.</param>
		/// <param name="vMax">view maximum corner.</param>
		/*
		 * Directly calculate the rects overlap area.
		 * The params are designed to reduce array indexing and property getting in the caller.
		 */
		private float GetAligned(Vector2 rMin, Vector2 rMax, Vector2 vMin, Vector2 vMax){

			var _left = Mathf.InverseLerp(rMin.x, rMax.x, vMin.x);
			var _right = Mathf.InverseLerp(rMin.x, rMax.x, vMax.x);

			var _bottom = Mathf.InverseLerp(rMin.y, rMax.y, vMin.y);
			var _top = Mathf.InverseLerp(rMin.y, rMax.y, vMax.y);

			return (_right - _left) * (_top - _bottom);

		}

		/// <summary>
		/// Get the proportion when this rotates relative to the <c>view</c>.
		/// </summary>
		/// <returns>The rotated proportion.</returns>
		/// <param name="view">View.</param>
		/*
		 * Sampling to check if the view contains the positions in the rect.
		 * This expensive method is much easier than calculating the area of the unknown overlap shape.
		 */
		private float GetRotated(RectTransform view){

			var _count = 0f;

			var _view = view.rect;
			var _rect = _transform.rect;

			var _xOffset = _rect.width / sample;
			var _yOffset = _rect.height / sample;

			var _xMax = _rect.xMax;
			var _yMax = _rect.yMax;

			for(var x = _rect.xMin + (_xOffset * 0.5f); x < _xMax; x += _xOffset){
				for(var y = _rect.yMin + (_yOffset * 0.5f); y < _yMax; y += _yOffset){
					if(_view.Contains(view.InverseTransformPoint(_transform.TransformPoint(x, y, 0f)))) _count++;
				}
			}

			return _count / (sample * sample);

		}

		#endregion

	}

}
