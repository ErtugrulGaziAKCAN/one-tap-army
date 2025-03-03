
/*WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW*\     (   (     ) )
|/                                                      \|       )  )   _((_
||  (c) Wanzyee Studio  < wanzyeestudio.blogspot.com >  ||      ( (    |_ _ |=n
|\                                                      /|   _____))   | !  ] U
\.ZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZ./  (_(__(S)   |___*/

using UnityEngine;

namespace WanzyeeStudio{
	
	/// <summary>
	/// Filter raycasts by a mask <c>UnityEngine.Texture2D</c> for non-rectangle area of effect.
	/// </summary>
	[HelpURL("https://git.io/viqRu")]
	[RequireComponent(typeof(RectTransform))]
	public class RaycastMaskFilter : MonoBehaviour, ICanvasRaycastFilter{

		#region Fields

		/// <summary>
		/// The readable <c>UnityEngine.Texture2D</c> as the filter mask.
		/// </summary>
		[Tooltip("Readable texture as the filter mask.")]
		public Texture2D mask;

		/// <summary>
		/// The channel to read the mask pixels.
		/// </summary>
		[Tooltip("Channel to read the mask pixels.")]
		public ColorChannel channel = ColorChannel.A;

		/// <summary>
		/// The minimum pixel value to pass raycasts.
		/// </summary>
		[Range(0f, 1f)]
		[Tooltip("Minimum pixel value to pass raycasts.")]
		public float threshold;

		/// <summary>
		/// The flag to reverse the <c>threshold</c> as maximum.
		/// </summary>
		[Tooltip("Flag to reverse the threshold as maximum.")]
		public bool reverse;

		/// <summary>
		/// The stored <c>UnityEngine.RectTransform</c> to reuse.
		/// </summary>
		private RectTransform _transform;

		#endregion


		#region Methods

		/// <summary>
		/// Check if the raycast valid, i.e., the hit <c>mask</c> pixel is over the <c>threshold</c>.
		/// </summary>
		/// <returns><c>true</c> if raycast valid; otherwise, <c>false</c>.</returns>
		/// <param name="sp">Screen position.</param>
		/// <param name="eventCamera">Raycast camera.</param>
		public bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera){

			if(null == mask) return true;
			if(null == _transform) _transform = GetComponent<RectTransform>();

			Vector2 _local;
			var _in = RectTransformUtility.ScreenPointToLocalPointInRectangle(_transform, sp, eventCamera, out _local);
			if(!_in) return false;

			var _point = Rect.PointToNormalized(_transform.rect, _local);

			var _x = Mathf.Clamp((int)(_point.x * mask.width), 0, mask.width - 1);
			var _y = Mathf.Clamp((int)(_point.y * mask.height), 0, mask.height - 1);

			var _color = mask.GetPixel(_x, _y)[(int)channel];

			if(reverse) return (1f > threshold) ? (_color <= threshold) : (_color < threshold);
			else return (0f < threshold) ? (_color >= threshold) : (_color > threshold);

		}

		#endregion

	}

}
