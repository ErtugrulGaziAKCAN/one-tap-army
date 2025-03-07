
/*WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW*\     (   (     ) )
|/                                                      \|       )  )   _((_
||  (c) Wanzyee Studio  < wanzyeestudio.blogspot.com >  ||      ( (    |_ _ |=n
|\                                                      /|   _____))   | !  ] U
\.ZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZ./  (_(__(S)   |___*/

using UnityEngine;
using UnityEngine.Events;
using System;

namespace WanzyeeStudio{
	
	/// <summary>
	/// Invoke event methods by single keyboard input operation.
	/// </summary>
	[HelpURL("https://git.io/viqRB")]
	public class EventKeyInvoker : MonoBehaviour{

		#region Fields

		/// <summary>
		/// The key state when to invoke.
		/// </summary>
		[Tooltip("Invoke when what key state.")]
		public KeyState on = KeyState.Press;

		/// <summary>
		/// The input key to check state.
		/// </summary>
		[Tooltip("Invoke by which input key.")]
		public KeyCode key = KeyCode.None;

		/// <summary>
		/// The event listener methods to invoke.
		/// </summary>
		[Tooltip("Listener functions to invoke.")]
		public UnityEvent onKey = new UnityEvent();

		#endregion


		#region Methods

		/// <summary>
		/// Update, check key state to invoke.
		/// </summary>
		private void Update(){
			if(CheckKey()) Invoke();
		}

		/// <summary>
		/// Check if input key state occured.
		/// </summary>
		/// <returns><c>true</c>, if key was checked, <c>false</c> otherwise.</returns>
		private bool CheckKey(){

			switch(on){

				case KeyState.None: return false;

				case KeyState.Press: return Input.GetKeyDown(key);

				case KeyState.Hold: return Input.GetKey(key);

				case KeyState.Release: return Input.GetKeyUp(key);

				default: throw new InvalidOperationException("Undefined value of 'on' property.");

			}

		}

		/// <summary>
		/// Invoke the event listener methods.
		/// </summary>
		public void Invoke(){
			
			if(null == onKey) throw new NullReferenceException("Field 'onKey' can't be null.");

			onKey.Invoke();

		}

		#endregion

	}

}
