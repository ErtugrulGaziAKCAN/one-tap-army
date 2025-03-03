
/*WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW*\     (   (     ) )
|/                                                      \|       )  )   _((_
||  (c) Wanzyee Studio  < wanzyeestudio.blogspot.com >  ||      ( (    |_ _ |=n
|\                                                      /|   _____))   | !  ] U
\.ZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZ./  (_(__(S)   |___*/

using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections;

namespace WanzyeeStudio{
	
	/// <summary>
	/// Invoke event methods by delay and repeat time.
	/// </summary>
	[HelpURL("https://git.io/viqR0")]
	public class EventTimeInvoker : MonoBehaviour{

		#region Fields

		/// <summary>
		/// The delay time to invoke after enabled.
		/// </summary>
		[Tooltip("Delay to invoke after enabled.")]
		public float delay;

		/// <summary>
		/// The repeat count to invoke, 0 means unlimited.
		/// </summary>
		[Tooltip("Repeat count to invoke, 0 means unlimit.")]
		public int repeat = 1;

		/// <summary>
		/// The interval between repeat invoke.
		/// </summary>
		[Tooltip("Interval between repeat invoke.")]
		public float interval = 1f;

		/// <summary>
		/// The event listener methods to invoke.
		/// </summary>
		[Tooltip("Listener functions to invoke.")]
		public UnityEvent onTime = new UnityEvent();

		#endregion


		#region Methods

		/// <summary>
		/// OnEnable, start timer coroutine to invoke.
		/// </summary>
		private void OnEnable(){
			StartCoroutine(WaitTime());
		}

		/// <summary>
		/// OnDisable, stop coroutine.
		/// </summary>
		private void OnDisable(){
			StopAllCoroutines();
		}

		/// <summary>
		/// Timer coroutine to invoke.
		/// </summary>
		/// <returns>The time.</returns>
		/*
		 * Calculating duration instead of WaitForSeconds to use dynamic params.
		 */
		private IEnumerator WaitTime(){

			var _time = Time.time;
			while(0f < delay && Time.time - _time < delay) yield return null;

			var _repeat = 0;
			while(0 == _repeat || 0 >= repeat || _repeat < repeat){

				Invoke();
				yield return _repeat++;

				_time = Time.time;
				while(0f < interval && Time.time - _time < interval) yield return null;

			}

		}

		/// <summary>
		/// Invoke the event listener methods.
		/// </summary>
		public void Invoke(){

			if(null == onTime) throw new NullReferenceException("Field 'onTime' can't be null.");

			onTime.Invoke();

		}

		#endregion

	}

}
