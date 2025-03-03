
/*WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW*\     (   (     ) )
|/                                                      \|       )  )   _((_
||  (c) Wanzyee Studio  < wanzyeestudio.blogspot.com >  ||      ( (    |_ _ |=n
|\                                                      /|   _____))   | !  ] U
\.ZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZ./  (_(__(S)   |___*/

namespace WanzyeeStudio{
	
	/// <summary>
	/// Input key state to describe keyboard operation.
	/// </summary>
	public enum KeyState{
		
		/// <summary>
		/// Nothing.
		/// </summary>
		None = 0,
		
		/// <summary>
		/// When pressed down.
		/// </summary>
		Press,
		
		/// <summary>
		/// While holding down.
		/// </summary>
		Hold,
		
		/// <summary>
		/// When released.
		/// </summary>
		Release
		
	};
	
}
