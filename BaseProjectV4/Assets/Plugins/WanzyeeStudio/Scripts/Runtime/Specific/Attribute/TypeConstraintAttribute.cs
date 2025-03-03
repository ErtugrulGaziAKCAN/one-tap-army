
/*WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW*\     (   (     ) )
|/                                                      \|       )  )   _((_
||  (c) Wanzyee Studio  < wanzyeestudio.blogspot.com >  ||      ( (    |_ _ |=n
|\                                                      /|   _____))   | !  ] U
\.ZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZ All rights reserved./  (_(__(S)   |___*/

using UnityEngine;
using System;

namespace WanzyeeStudio{
	
	/// <summary>
	/// <c>UnityEngine.PropertyAttribute</c> to filter the assigned object type by constraints.
	/// </summary>
	/// 
	/// <remarks>
	/// Draw a common object field, but filter the assigned object type by constraint types.
	/// Generally for interface, a valid object has to derive from or implement all constraints.
	/// If there're multiple valid <c>UnityEngine.Component</c> attached to the same <c>UnityEngine.GameObject</c>.
	/// Also able to select one of them by right clicking on the object field.
	/// </remarks>
	/// 
	/// <example>
	/// Declaring examples below:
	/// </example>
	/// 
	/// <code>
	/// [TypeConstraint]
	/// public MonoBehaviour someScript;
	/// 
	/// [TypeConstraint(typeof(IInterface))]
	/// public Object someInterface;
	/// </code>
	/// 
	[AttributeUsage(AttributeTargets.Field)]
	public class TypeConstraintAttribute : PropertyAttribute{
		
		/// <summary>
		/// The constraint types to filter assigned object.
		/// </summary>
		public readonly Type[] constraints;

		/// <summary>
		/// Activate the <c>UnityEngine.Component</c> selecting menu.
		/// </summary>
		/// <returns>The instance.</returns>
		public TypeConstraintAttribute(){}

		/// <summary>
		/// Setup constraint types to filter, or only activate the selecting menu if nothing assigned.
		/// </summary>
		/// <returns>The instance.</returns>
		/// <param name="constraints">Constraints.</param>
		public TypeConstraintAttribute(params Type[] constraints){
			this.constraints = constraints;
		}

	}

}
