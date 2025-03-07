
/*WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW*\     (   (     ) )
|/                                                      \|       )  )   _((_
||  (c) Wanzyee Studio  < wanzyeestudio.blogspot.com >  ||      ( (    |_ _ |=n
|\                                                      /|   _____))   | !  ] U
\.ZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZ All rights reserved./  (_(__(S)   |___*/

using UnityEngine;
using System;
using System.Linq;

namespace WanzyeeStudio{
	
	/// <summary>
	/// Group and reorder multiple <c>EventDelegator</c> to invoke at once.
	/// </summary>
	[HelpURL("https://git.io/viqR8")]
	public class EventDelegatorGroup : MonoBehaviour{
		
		#region Fields

		/// <summary>
		/// Flag if to invoke each <c>EventDelegator</c> attached above before this's <c>delegators</c>.
		/// </summary>
		[Tooltip("If to invoke each EventDelegator above.")]
		public bool invokeAbove;

		/// <summary>
		/// Flag if to invoke each <c>EventDelegator</c> attached below after this's <c>delegators</c>.
		/// </summary>
		[Tooltip("If to invoke each EventDelegator below.")]
		public bool invokeBelow;

		/// <summary>
		/// All the <c>EventDelegator</c> to invoke in turn between others above and below.
		/// </summary>
		[Tooltip("Delegators to invoke between above and below.")]
		[TypeConstraint]
		public EventDelegator[] delegators = {};

		/// <summary>
		/// The flag if currently invoking, used to check looping for safety.
		/// </summary>
		private bool _invoking;

		#endregion


		#region Methods

		/// <summary>
		/// Start, declare to show the enabled toggle.
		/// </summary>
		private void Start(){}

		/// <summary>
		/// Get all the <c>EventDelegator</c>, include which attached above and below if checked.
		/// </summary>
		/// <returns>The delegators.</returns>
		public EventDelegator[] GetDelegators(){

			var _result = delegators ?? Enumerable.Empty<EventDelegator>();
			if(!invokeAbove && !invokeBelow) return _result.ToArray();

			var _components = GetComponents<Component>();
			var _index = Array.IndexOf(_components, this);

			if(invokeAbove) _result = _components.Take(_index).OfType<EventDelegator>().Concat(_result);
			if(invokeBelow) _result = _result.Concat(_components.Skip(_index).OfType<EventDelegator>());

			return _result.ToArray();

		}

		/// <summary>
		/// Invoke each <c>EventDelegator</c> of this in turn if enabled.
		/// </summary>
		/*
		 * Return void for UnityEvent calling.
		 * To get the returned values, iterate delegators of this instead.
		 */
		public void Invoke(){

			if(!enabled) return;

			if(_invoking) throw new OverflowException("Looping method call.");
			_invoking = true;

			try{ foreach(var _delegator in GetDelegators()) if(null != _delegator) _delegator.Invoke(); }
			finally{ _invoking = false; }

		}

		#endregion

	}

}
