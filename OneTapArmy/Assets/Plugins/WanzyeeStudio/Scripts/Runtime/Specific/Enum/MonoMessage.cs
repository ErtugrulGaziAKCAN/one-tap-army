
/*WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW*\     (   (     ) )
|/                                                      \|       )  )   _((_
||  (c) Wanzyee Studio  < wanzyeestudio.blogspot.com >  ||      ( (    |_ _ |=n
|\                                                      /|   _____))   | !  ] U
\.ZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZ./  (_(__(S)   |___*/

namespace WanzyeeStudio{
	
	/// <summary>
	/// Messages of <c>UnityEngine.MonoBehaviour</c>.
	/// </summary>
	public enum MonoMessage{

		/// <summary>
		/// Awake is called when the script instance is being loaded.
		/// </summary>
		Awake = 0,

		/// <summary>
		/// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
		/// </summary>
		FixedUpdate,
		
		/// <summary>
		/// LateUpdate is called every frame, if the Behaviour is enabled.
		/// </summary>
		LateUpdate,
		
		/// <summary>
		/// Callback for setting up animation IK.
		/// </summary>
		OnAnimatorIK,
		
		/// <summary>
		/// Callback for processing animation movements for modifying root motion.
		/// </summary>
		OnAnimatorMove,
		
		/// <summary>
		/// Sent to all game objects when the player gets or loses focus.
		/// </summary>
		OnApplicationFocus,
		
		/// <summary>
		/// Sent to all game objects when the player pauses.
		/// </summary>
		OnApplicationPause,
		
		/// <summary>
		/// Sent to all game objects before the application is quit.
		/// </summary>
		OnApplicationQuit,
		
		/// <summary>
		/// If implemented, Unity will insert a custom filter into the audio DSP chain.
		/// </summary>
		OnAudioFilterRead,
		
		/// <summary>
		/// OnBecameInvisible is called when the renderer is no longer visible by any camera.
		/// </summary>
		OnBecameInvisible,
		
		/// <summary>
		/// OnBecameVisible is called when the renderer became visible by any camera.
		/// </summary>
		OnBecameVisible,
		
		/// <summary>
		/// Called when this collider/rigidbody has begun touching another rigidbody/collider.
		/// </summary>
		OnCollisionEnter,
		
		/// <summary>
		/// Sent when an incoming collider makes contact with this object's collider.
		/// </summary>
		OnCollisionEnter2D,
		
		/// <summary>
		/// Called when this collider/rigidbody has stopped touching another rigidbody/collider.
		/// </summary>
		OnCollisionExit,
		
		/// <summary>
		/// Sent when a collider on another object stops touching this object's collider.
		/// </summary>
		OnCollisionExit2D,
		
		/// <summary>
		/// Called once per frame for every collider/rigidbody that is touching rigidbody/collider.
		/// </summary>
		OnCollisionStay,
		
		/// <summary>
		/// Sent each frame where a collider on another object is touching this object's collider.
		/// </summary>
		OnCollisionStay2D,
		
		/// <summary>
		/// Called on the client when you have successfully connected to a server.
		/// </summary>
		OnConnectedToServer,
		
		/// <summary>
		/// Called when the controller hits a collider while performing a Move.
		/// </summary>
		OnControllerColliderHit,
		
		/// <summary>
		/// This function is called when the MonoBehaviour will be destroyed.
		/// </summary>
		OnDestroy,
		
		/// <summary>
		/// This function is called when the behaviour becomes disabled () or inactive.
		/// </summary>
		OnDisable,
		
		/// <summary>
		/// Called on the client when the connection was lost or you disconnected from the server.
		/// </summary>
		OnDisconnectedFromServer,
		
		/// <summary>
		/// Implement OnDrawGizmos if you want to draw gizmos that are also pickable and always drawn.
		/// </summary>
		OnDrawGizmos,
		
		/// <summary>
		/// Implement this OnDrawGizmosSelected if you want to draw gizmos only if the object is selected.
		/// </summary>
		OnDrawGizmosSelected,
		
		/// <summary>
		/// This function is called when the object becomes enabled and active.
		/// </summary>
		OnEnable,
		
		/// <summary>
		/// Called on the client when a connection attempt fails for some reason.
		/// </summary>
		OnFailedToConnect,
		
		/// <summary>
		/// Called on clients or servers when there is a problem connecting to the MasterServer.
		/// </summary>
		OnFailedToConnectToMasterServer,
		
		/// <summary>
		/// OnGUI is called for rendering and handling GUI events.
		/// </summary>
		OnGUI,
		
		/// <summary>
		/// Called when a joint attached to the same game object broke.
		/// </summary>
		OnJointBreak,

		/// <summary>
		/// Called when a Joint2D attached to the same game object breaks.
		/// </summary>
		OnJointBreak2D,
		
		/// <summary>
		/// Called on clients or servers when reporting events from the MasterServer.
		/// </summary>
		OnMasterServerEvent,
		
		/// <summary>
		/// Called when the user has pressed the mouse button while over the GUIElement or Collider.
		/// </summary>
		OnMouseDown,
		
		/// <summary>
		/// Called when the user has clicked on a GUIElement or Collider and is still holding down.
		/// </summary>
		OnMouseDrag,
		
		/// <summary>
		/// Called when the mouse enters the GUIElement or Collider.
		/// </summary>
		OnMouseEnter,
		
		/// <summary>
		/// Called when the mouse is not any longer over the GUIElement or Collider.
		/// </summary>
		OnMouseExit,
		
		/// <summary>
		/// Called every frame while the mouse is over the GUIElement or Collider.
		/// </summary>
		OnMouseOver,
		
		/// <summary>
		/// OnMouseUp is called when the user has released the mouse button.
		/// </summary>
		OnMouseUp,
		
		/// <summary>
		/// Called when the mouse is released over the same GUIElement or Collider as it was pressed.
		/// </summary>
		OnMouseUpAsButton,
		
		/// <summary>
		/// Called on objects which have been network instantiated with Network.Instantiate.
		/// </summary>
		OnNetworkInstantiate,
		
		/// <summary>
		/// OnParticleCollision is called when a particle hits a collider.
		/// </summary>
		OnParticleCollision,

		/// <summary>
		/// Called when any particles in a particle system meet the conditions in the trigger module.
		/// </summary>
		OnParticleTrigger,
		
		/// <summary>
		/// Called on the server whenever a new player has successfully connected.
		/// </summary>
		OnPlayerConnected,
		
		/// <summary>
		/// Called on the server whenever a player disconnected from the server.
		/// </summary>
		OnPlayerDisconnected,
		
		/// <summary>
		/// OnPostRender is called after a camera finished rendering the scene.
		/// </summary>
		OnPostRender,
		
		/// <summary>
		/// OnPreCull is called before a camera culls the scene.
		/// </summary>
		OnPreCull,
		
		/// <summary>
		/// OnPreRender is called before a camera starts rendering the scene.
		/// </summary>
		OnPreRender,
		
		/// <summary>
		/// OnRenderImage is called after all rendering is complete to render image.
		/// </summary>
		OnRenderImage,
		
		/// <summary>
		/// OnRenderObject is called after camera has rendered the scene.
		/// </summary>
		OnRenderObject,
		
		/// <summary>
		/// Used to customize synchronization of variables in a script watched by a network view.
		/// </summary>
		OnSerializeNetworkView,
		
		/// <summary>
		/// Called on the server whenever a Network.InitializeServer was invoked and has completed.
		/// </summary>
		OnServerInitialized,
		
		/// <summary>
		/// This function is called when the list of children of the transform of the GameObject has changed.
		/// </summary>
		OnTransformChildrenChanged,
		
		/// <summary>
		/// This function is called when the parent property of the transform of the GameObject has changed.
		/// </summary>
		OnTransformParentChanged,
		
		/// <summary>
		/// OnTriggerEnter is called when the Collider other enters the trigger.
		/// </summary>
		OnTriggerEnter,
		
		/// <summary>
		/// Sent when another object enters a trigger collider attached to this object.
		/// </summary>
		OnTriggerEnter2D,
		
		/// <summary>
		/// OnTriggerExit is called when the Collider other has stopped touching the trigger.
		/// </summary>
		OnTriggerExit,
		
		/// <summary>
		/// Sent when another object leaves a trigger collider attached to this object.
		/// </summary>
		OnTriggerExit2D,
		
		/// <summary>
		/// OnTriggerStay is called once per frame for every Collider other that is touching the trigger.
		/// </summary>
		OnTriggerStay,
		
		/// <summary>
		/// Sent each frame where another object is within a trigger collider attached to this object.
		/// </summary>
		OnTriggerStay2D,
		
		/// <summary>
		/// This function is called when the script is loaded or a value is changed in the inspector.
		/// </summary>
		OnValidate,
		
		/// <summary>
		/// OnWillRenderObject is called once for each camera if the object is visible.
		/// </summary>
		OnWillRenderObject,
		
		/// <summary>
		/// Reset to default values.
		/// </summary>
		Reset,
		
		/// <summary>
		/// Called on the frame when a script is enabled just before any Update is called the first time.
		/// </summary>
		Start,
		
		/// <summary>
		/// Update is called every frame, if the MonoBehaviour is enabled.
		/// </summary>
		Update

	};

}
