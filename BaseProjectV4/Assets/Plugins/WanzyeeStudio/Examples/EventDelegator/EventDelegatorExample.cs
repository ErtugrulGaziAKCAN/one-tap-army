
/*WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW*\     (   (     ) )
|/                                                      \|       )  )   _((_
||  (c) Wanzyee Studio  < wanzyeestudio.blogspot.com >  ||      ( (    |_ _ |=n
|\                                                      /|   _____))   | !  ] U
\.ZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZ./  (_(__(S)   |___*/

using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Linq;

namespace WanzyeeStudio.Example{
	
	/// <summary>
	/// Example for <c>EventDelegator</c> and <c>TypeConstraintAttribute</c>.
	/// </summary>
	/// 
	/// <remarks>
	/// Include basic methods to be invoked by <c>EventDelegator</c>, <c>EventKeyInvoker</c>, etc.
	/// And fields with <c>TypeConstraintAttribute</c> to show appearance.
	/// This just implements the interface for the attribute example.
	/// </remarks>
	/// 
	public class EventDelegatorExample : MonoBehaviour, ISelectHandler{

		#region TypeConstraint Example

		/// <summary>
		/// The <c>TypeConstraintAttribute</c> example for type.
		/// </summary>
		[Tooltip(
			"TypeConstraint example for type.\n" +
			"Right click in field to select a Component."
		)]
		[TypeConstraint]
		public Component component;

		/// <summary>
		/// The <c>TypeConstraintAttribute</c> example for interface.
		/// </summary>
		[Tooltip(
			"TypeConstraint example for interface.\n" +
			"Right click in field to select a Component implements ISelectHandler"
		)]
		[TypeConstraint(typeof(ISelectHandler))]
		public Object selectable;

		#endregion


		#region Example Methods

		/// <summary>
		/// Instance method for event or delegator to log a <c>string</c>.
		/// </summary>
		/// <param name="message">Message.</param>
		public void TestInstance(string message){
			Debug.LogFormat(this, "Instance: {0}", message);
		}

		/// <summary>
		/// Instance method for event or delegator to log a <c>KeyCode</c> array.
		/// </summary>
		/// <param name="keys">Keys.</param>
		public void TestInstanceParams(params KeyCode[] keys){
			Debug.LogFormat(this, "Instance: {0}", string.Join(", ", keys.Select((key) => key.ToString()).ToArray()));
		}

		/// <summary>
		/// Static method for event or delegator to log a <c>string</c>.
		/// </summary>
		/// <param name="note">Note.</param>
		public static void TestStatic(string note){
			Debug.LogFormat("Static: {0}", note);
		}

		/// <summary>
		/// Static method for event or delegator to log a <c>UnityEngine.MonoBehaviour</c> list.
		/// </summary>
		/// <param name="components">Components.</param>
		public static void TestStaticList(List<MonoBehaviour> components){
			var _components = components.Select((obj) => (null == obj) ? "null" : obj.ToString());
			Debug.LogFormat("Static: {0}", string.Join(", ", _components.ToArray()));
		}

		#endregion


		#region Methods

		/// <summary>
		/// Awake, check game view size to warning, to make sure all the messages are visible.
		/// </summary>
		private void Awake(){
			if(Screen.width < 500 || Screen.height < 400) Debug.LogWarning("Please set Game view size over 500x400.");
		}

		/// <summary>
		/// OnSelect, implement for interface without doing anything.
		/// </summary>
		/// <param name="eventData">Event data.</param>
		public void OnSelect(BaseEventData eventData){
			Debug.Log("OnSelect", this);
		}

		#endregion

	}

}
