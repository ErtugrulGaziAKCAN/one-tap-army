
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
	/// Invoke event methods by specified <c>UnityEngine.MonoBehaviour</c> message.
	/// </summary>
	[HelpURL("https://git.io/viqRR")]
	public class EventMonoInvoker : MonoBehaviour{
		
		#region Instance
		
		/// <summary>
		/// The <c>UnityEngine.MonoBehaviour</c> message to invoke.
		/// </summary>
		[Tooltip("Invoke when what message.")]
		public MonoMessage on = MonoMessage.Awake;

		/// <summary>
		/// The event listener methods to invoke.
		/// </summary>
		[Tooltip("Listener functions to invoke.")]
		public UnityEvent onMessage = new UnityEvent();
		
		/// <summary>
		/// Invoke the event listener methods.
		/// </summary>
		public void Invoke(){
			
			if(null == onMessage) throw new NullReferenceException("Field 'onMessage' can't be null.");

			onMessage.Invoke();

		}
		
		#endregion
		
		
		#region Message Methods

		/// <summary>
		/// Awake, check if to invoke.
		/// </summary>
		private void Awake(){
			if(MonoMessage.Awake == on) Invoke();
		}
		
		/// <summary>
		/// FixedUpdate, check if to invoke.
		/// </summary>
		private void FixedUpdate(){
			if(MonoMessage.FixedUpdate == on) Invoke();
		}
		
		/// <summary>
		/// LateUpdate, check if to invoke.
		/// </summary>
		private void LateUpdate(){
			if(MonoMessage.LateUpdate == on) Invoke();
		}
		
		/// <summary>
		/// OnAnimatorIK, check if to invoke.
		/// </summary>
		/// <param name="layerIndex">Useless here.</param>
		private void OnAnimatorIK(int layerIndex){
			if(MonoMessage.OnAnimatorIK == on) Invoke();
		}
		
		/// <summary>
		/// OnAnimatorMove, check if to invoke.
		/// </summary>
		private void OnAnimatorMove(){
			if(MonoMessage.OnAnimatorMove == on) Invoke();
		}
		
		/// <summary>
		/// OnApplicationFocus, check if to invoke.
		/// </summary>
		/// <param name="focusStatus">Useless here.</param>
		private void OnApplicationFocus(bool focusStatus){
			if(MonoMessage.OnApplicationFocus == on) Invoke();
		}
		
		/// <summary>
		/// OnApplicationPause, check if to invoke.
		/// </summary>
		/// <param name="pauseStatus">Useless here.</param>
		private void OnApplicationPause(bool pauseStatus){
			if(MonoMessage.OnApplicationPause == on) Invoke();
		}
		
		/// <summary>
		/// OnApplicationQuit, check if to invoke.
		/// </summary>
		private void OnApplicationQuit(){
			if(MonoMessage.OnApplicationQuit == on) Invoke();
		}
		
		/// <summary>
		/// OnAudioFilterRead, check if to invoke.
		/// </summary>
		/// <param name="data">Useless here.</param>
		/// <param name="channels">Useless here.</param>
		private void OnAudioFilterRead(float[] data, int channels){
			if(MonoMessage.OnAudioFilterRead == on) Invoke();
		}
		
		/// <summary>
		/// OnBecameInvisible, check if to invoke.
		/// </summary>
		private void OnBecameInvisible(){
			if(MonoMessage.OnBecameInvisible == on) Invoke();
		}
		
		/// <summary>
		/// OnBecameVisible, check if to invoke.
		/// </summary>
		private void OnBecameVisible(){
			if(MonoMessage.OnBecameVisible == on) Invoke();
		}
		
		/// <summary>
		/// OnCollisionEnter, check if to invoke.
		/// </summary>
		/// <param name="collision">Useless here.</param>
		private void OnCollisionEnter(Collision collision){
			if(MonoMessage.OnCollisionEnter == on) Invoke();
		}
		
		/// <summary>
		/// OnCollisionEnter2D, check if to invoke.
		/// </summary>
		/// <param name="coll">Useless here.</param>
		private void OnCollisionEnter2D(Collision2D coll){
			if(MonoMessage.OnCollisionEnter2D == on) Invoke();
		}
		
		/// <summary>
		/// OnCollisionExit, check if to invoke.
		/// </summary>
		/// <param name="collisionInfo">Useless here.</param>
		private void OnCollisionExit(Collision collisionInfo){
			if(MonoMessage.OnCollisionExit == on) Invoke();
		}
		
		/// <summary>
		/// OnCollisionExit2D, check if to invoke.
		/// </summary>
		/// <param name="coll">Useless here.</param>
		private void OnCollisionExit2D(Collision2D coll){
			if(MonoMessage.OnCollisionExit2D == on) Invoke();
		}
		
		/// <summary>
		/// OnCollisionStay, check if to invoke.
		/// </summary>
		/// <param name="collisionInfo">Useless here.</param>
		private void OnCollisionStay(Collision collisionInfo){
			if(MonoMessage.OnCollisionStay == on) Invoke();
		}
		
		/// <summary>
		/// OnCollisionStay2D, check if to invoke.
		/// </summary>
		/// <param name="coll">Useless here.</param>
		private void OnCollisionStay2D(Collision2D coll){
			if(MonoMessage.OnCollisionStay2D == on) Invoke();
		}
		
		/// <summary>
		/// OnConnectedToServer, check if to invoke.
		/// </summary>
		private void OnConnectedToServer(){
			if(MonoMessage.OnConnectedToServer == on) Invoke();
		}
		
		/// <summary>
		/// OnControllerColliderHit, check if to invoke.
		/// </summary>
		/// <param name="hit">Useless here.</param>
		private void OnControllerColliderHit(ControllerColliderHit hit){
			if(MonoMessage.OnControllerColliderHit == on) Invoke();
		}
		
		/// <summary>
		/// OnDestroy, check if to invoke.
		/// </summary>
		private void OnDestroy(){
			if(MonoMessage.OnDestroy == on) Invoke();
		}
		
		/// <summary>
		/// OnDisable, check if to invoke.
		/// </summary>
		private void OnDisable(){
			if(MonoMessage.OnDisable == on) Invoke();
		}
		
		/// <summary>
		/// OnDrawGizmos, check if to invoke.
		/// </summary>
		private void OnDrawGizmos(){
			if(MonoMessage.OnDrawGizmos == on) Invoke();
		}
		
		/// <summary>
		/// OnDrawGizmosSelected, check if to invoke.
		/// </summary>
		private void OnDrawGizmosSelected(){
			if(MonoMessage.OnDrawGizmosSelected == on) Invoke();
		}
		
		/// <summary>
		/// OnEnable, check if to invoke.
		/// </summary>
		private void OnEnable(){
			if(MonoMessage.OnEnable == on) Invoke();
		}
		
		/// <summary>
		/// OnGUI, check if to invoke.
		/// </summary>
		private void OnGUI(){
			if(MonoMessage.OnGUI == on) Invoke();
		}
		
		/// <summary>
		/// OnJointBreak, check if to invoke.
		/// </summary>
		/// <param name="breakForce">Useless here.</param>
		private void OnJointBreak(float breakForce){
			if(MonoMessage.OnJointBreak == on) Invoke();
		}

		/// <summary>
		/// OnJointBreak2D, check if to invoke.
		/// </summary>
		/// <param name="brokenJoint">Useless here.</param>
		private void OnJointBreak2D(Joint2D brokenJoint){
			if(MonoMessage.OnJointBreak2D == on) Invoke();
		}
		
		/// <summary>
		/// OnMouseDown, check if to invoke.
		/// </summary>
		private void OnMouseDown(){
			if(MonoMessage.OnMouseDown == on) Invoke();
		}
		
		/// <summary>
		/// OnMouseDrag, check if to invoke.
		/// </summary>
		private void OnMouseDrag(){
			if(MonoMessage.OnMouseDrag == on) Invoke();
		}
		
		/// <summary>
		/// OnMouseEnter, check if to invoke.
		/// </summary>
		private void OnMouseEnter(){
			if(MonoMessage.OnMouseEnter == on) Invoke();
		}
		
		/// <summary>
		/// OnMouseExit, check if to invoke.
		/// </summary>
		private void OnMouseExit(){
			if(MonoMessage.OnMouseExit == on) Invoke();
		}
		
		/// <summary>
		/// OnMouseOver, check if to invoke.
		/// </summary>
		private void OnMouseOver(){
			if(MonoMessage.OnMouseOver == on) Invoke();
		}
		
		/// <summary>
		/// OnMouseUp, check if to invoke.
		/// </summary>
		private void OnMouseUp(){
			if(MonoMessage.OnMouseUp == on) Invoke();
		}
		
		/// <summary>
		/// OnMouseUpAsButton, check if to invoke.
		/// </summary>
		private void OnMouseUpAsButton(){
			if(MonoMessage.OnMouseUpAsButton == on) Invoke();
		}
		
		/// <summary>
		/// OnParticleCollision, check if to invoke.
		/// </summary>
		/// <param name="other">Useless here.</param>
		private void OnParticleCollision(GameObject other){
			if(MonoMessage.OnParticleCollision == on) Invoke();
		}

		/// <summary>
		/// OnParticleTrigger, check if to invoke.
		/// </summary>
		private void OnParticleTrigger(){
			if(MonoMessage.OnParticleTrigger == on) Invoke();
		}
		
		/// <summary>
		/// OnPostRender, check if to invoke.
		/// </summary>
		private void OnPostRender(){
			if(MonoMessage.OnPostRender == on) Invoke();
		}
		
		/// <summary>
		/// OnPreCull, check if to invoke.
		/// </summary>
		private void OnPreCull(){
			if(MonoMessage.OnPreCull == on) Invoke();
		}
		
		/// <summary>
		/// OnPreRender, check if to invoke.
		/// </summary>
		private void OnPreRender(){
			if(MonoMessage.OnPreRender == on) Invoke();
		}
		
		/// <summary>
		/// OnRenderImage, check if to invoke.
		/// </summary>
		/// <param name="src">Useless here.</param>
		/// <param name="dest">Useless here.</param>
		private void OnRenderImage(RenderTexture src, RenderTexture dest){
			if(MonoMessage.OnRenderImage == on) Invoke();
		}
		
		/// <summary>
		/// OnRenderObject, check if to invoke.
		/// </summary>
		private void OnRenderObject(){
			if(MonoMessage.OnRenderObject == on) Invoke();
		}
		
		/// <summary>
		/// OnTransformChildrenChanged, check if to invoke.
		/// </summary>
		private void OnTransformChildrenChanged(){
			if(MonoMessage.OnTransformChildrenChanged == on) Invoke();
		}
		
		/// <summary>
		/// OnTransformParentChanged, check if to invoke.
		/// </summary>
		private void OnTransformParentChanged(){
			if(MonoMessage.OnTransformParentChanged == on) Invoke();
		}
		
		/// <summary>
		/// OnTriggerEnter, check if to invoke.
		/// </summary>
		/// <param name="other">Useless here.</param>
		private void OnTriggerEnter(Collider other){
			if(MonoMessage.OnTriggerEnter == on) Invoke();
		}
		
		/// <summary>
		/// OnTriggerEnter2D, check if to invoke.
		/// </summary>
		/// <param name="other">Useless here.</param>
		private void OnTriggerEnter2D(Collider2D other){
			if(MonoMessage.OnTriggerEnter2D == on) Invoke();
		}
		
		/// <summary>
		/// OnTriggerExit, check if to invoke.
		/// </summary>
		/// <param name="other">Useless here.</param>
		private void OnTriggerExit(Collider other){
			if(MonoMessage.OnTriggerExit == on) Invoke();
		}
		
		/// <summary>
		/// OnTriggerExit2D, check if to invoke.
		/// </summary>
		/// <param name="other">Useless here.</param>
		private void OnTriggerExit2D(Collider2D other){
			if(MonoMessage.OnTriggerExit2D == on) Invoke();
		}
		
		/// <summary>
		/// OnTriggerStay, check if to invoke.
		/// </summary>
		/// <param name="other">Useless here.</param>
		private void OnTriggerStay(Collider other){
			if(MonoMessage.OnTriggerStay == on) Invoke();
		}
		
		/// <summary>
		/// OnTriggerStay2D, check if to invoke.
		/// </summary>
		/// <param name="other">Useless here.</param>
		private void OnTriggerStay2D(Collider2D other){
			if(MonoMessage.OnTriggerStay2D == on) Invoke();
		}
		
		/// <summary>
		/// OnValidate, check if to invoke.
		/// </summary>
		private void OnValidate(){
			if(MonoMessage.OnValidate == on) Invoke();
		}
		
		/// <summary>
		/// OnWillRenderObject, check if to invoke.
		/// </summary>
		private void OnWillRenderObject(){
			if(MonoMessage.OnWillRenderObject == on) Invoke();
		}
		
		/// <summary>
		/// Reset, check if to invoke.
		/// </summary>
		private void Reset(){
			if(MonoMessage.Reset == on) Invoke();
		}
		
		/// <summary>
		/// Start, check if to invoke.
		/// </summary>
		private void Start(){
			if(MonoMessage.Start == on) Invoke();
		}
		
		/// <summary>
		/// Update, check if to invoke.
		/// </summary>
		private void Update(){
			if(MonoMessage.Update == on) Invoke();
		}

		#endregion


		#if !UNITY_WEBGL && !UNITY_2018_2_OR_NEWER

		/// <summary>
		/// OnDisconnectedFromServer, check if to invoke.
		/// </summary>
		/// <param name="info">Useless here.</param>
		private void OnDisconnectedFromServer(NetworkDisconnection info){
			if(MonoMessage.OnDisconnectedFromServer == on) Invoke();
		}

		/// <summary>
		/// OnFailedToConnect, check if to invoke.
		/// </summary>
		/// <param name="error">Useless here.</param>
		private void OnFailedToConnect(NetworkConnectionError error){
			if(MonoMessage.OnFailedToConnect == on) Invoke();
		}

		/// <summary>
		/// OnFailedToConnectToMasterServer, check if to invoke.
		/// </summary>
		/// <param name="info">Useless here.</param>
		private void OnFailedToConnectToMasterServer(NetworkConnectionError info){
			if(MonoMessage.OnFailedToConnectToMasterServer == on) Invoke();
		}

		/// <summary>
		/// OnMasterServerEvent, check if to invoke.
		/// </summary>
		/// <param name="msEvent">Useless here.</param>
		private void OnMasterServerEvent(MasterServerEvent msEvent){
			if(MonoMessage.OnMasterServerEvent == on) Invoke();
		}

		/// <summary>
		/// OnNetworkInstantiate, check if to invoke.
		/// </summary>
		/// <param name="info">Useless here.</param>
		private void OnNetworkInstantiate(NetworkMessageInfo info){
			if(MonoMessage.OnNetworkInstantiate == on) Invoke();
		}

		/// <summary>
		/// OnPlayerConnected, check if to invoke.
		/// </summary>
		/// <param name="player">Useless here.</param>
		private void OnPlayerConnected(NetworkPlayer player){
			if(MonoMessage.OnPlayerConnected == on) Invoke();
		}

		/// <summary>
		/// OnPlayerDisconnected, check if to invoke.
		/// </summary>
		/// <param name="player">Useless here.</param>
		private void OnPlayerDisconnected(NetworkPlayer player){
			if(MonoMessage.OnPlayerDisconnected == on) Invoke();
		}

		/// <summary>
		/// OnServerInitialized, check if to invoke.
		/// </summary>
		private void OnServerInitialized(){
			if(MonoMessage.OnServerInitialized == on) Invoke();
		}

		/// <summary>
		/// OnSerializeNetworkView, check if to invoke.
		/// </summary>
		/// <param name="stream">Useless here.</param>
		/// <param name="info">Useless here.</param>
		private void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info){
			if(MonoMessage.OnSerializeNetworkView == on) Invoke();
		}

		#endif

	}

}
