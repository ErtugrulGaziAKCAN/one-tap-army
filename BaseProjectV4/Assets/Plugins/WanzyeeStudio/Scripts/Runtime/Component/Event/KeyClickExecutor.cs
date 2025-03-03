
/*WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW*\     (   (     ) )
|/                                                      \|       )  )   _((_
||  (c) Wanzyee Studio  < wanzyeestudio.blogspot.com >  ||      ( (    |_ _ |=n
|\                                                      /|   _____))   | !  ] U
\.ZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZ All rights reserved./  (_(__(S)   |___*/

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

using WanzyeeStudio.Extension;

namespace WanzyeeStudio{

	/// <summary>
	/// Execute click event on this object when the center touchable and the specified key pressed.
	/// </summary>
	/// 
	/// <remarks>
	/// Usage example, redirect the mobile back button, i.e., "Esc" key, to a UI button.
	/// This executes all <c>UnityEngine.EventSystems.IPointerClickHandler</c> attached together.
	/// </remarks>
	/// 
	[HelpURL("https://git.io/fA1SQ")]
	[RequireComponent(typeof(RectTransform))]
	public class KeyClickExecutor : MonoBehaviour{

		#region Fields

		/// <summary>
		/// The input key to listen.
		/// </summary>
		[Tooltip("Execute by which input key.")]
		public KeyCode key = KeyCode.Escape;

		/// <summary>
		/// The raycast result list to reuse.
		/// </summary>
		private readonly List<RaycastResult> _raycasts = new List<RaycastResult>();

		#endregion


		#region Methods

		/// <summary>
		/// Update, check if key pressed and touchable on the top layer to execute click event.
		/// </summary>
		private void Update(){

			if(KeyCode.None == key || !Input.GetKeyDown(key)) return;

			var _data = TryGetEventData();
			if(null == _data) return;

			EventSystem.current.RaycastAll(_data, _raycasts);
			if(!_raycasts.Any()) return;

			var _target = ExecuteEvents.GetEventHandler<IPointerClickHandler>(_raycasts[0].gameObject);
			if(_target == _data.pointerPress) ExecuteEvents.Execute(_target, _data, ExecuteEvents.pointerClickHandler);
			
		}

		/// <summary>
		/// Check if necessary components enabled and the center inside the screen to return the click event data.
		/// </summary>
		/// <returns>The event data.</returns>
		/*
		 * Combine basic checking and creating data, since the parameters are almost the same.
		 */
		private PointerEventData TryGetEventData(){
			
			var _graphic = GetComponent<Graphic>();
			if(null == _graphic || !_graphic.enabled || !_graphic.raycastTarget) return null;

			var _raycaster = GetComponentInParent<GraphicRaycaster>();
			if(null == _raycaster || !_raycaster.enabled) return null;

			var _position = GetComponent<RectTransform>().CenterToScreenPoint();
			if(!new Rect(0f, 0f, Screen.width, Screen.height).Contains(_position)) return null;

			var _target = ExecuteEvents.GetEventHandler<IPointerClickHandler>(gameObject);
			return (null != _target) ? CreateEventData(_raycaster, _position, _target) : null;

		}

		/// <summary>
		/// Create the event data to execute click.
		/// </summary>
		/// <returns>The event data.</returns>
		/// <param name="raycaster">Raycaster.</param>
		/// <param name="position">Position.</param>
		/// <param name="target">Target.</param>
		private PointerEventData CreateEventData(BaseRaycaster raycaster, Vector2 position, GameObject target){

			var _raycast = new RaycastResult(){ module = raycaster };
			var _result = new PointerEventData(EventSystem.current){ clickCount = 1 };

			_result.position = _result.pressPosition = _raycast.screenPosition = position;
			_result.pointerEnter = _result.pointerPress = _result.rawPointerPress = _raycast.gameObject = target;

			_result.pointerCurrentRaycast = _result.pointerPressRaycast = _raycast;
			return _result;

		}

		#endregion

	}

}
