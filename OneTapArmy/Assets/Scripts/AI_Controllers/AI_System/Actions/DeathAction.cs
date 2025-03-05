using AI_Controllers.DataHolder.Core;
using DG.Tweening;
using Plugins.CW.LeanPool.Required.Scripts;
using scriptable_states.Runtime;
using UnityEngine;
namespace AI_Controllers.AI_System.Actions
{
    [CreateAssetMenu(menuName = "Scriptable State Machine/Actions/DeathAction",
        fileName = "new DeathAction")]
    public class DeathAction : ScriptableAction
    {
//-------Public Variables-------//


//------Serialized Fields-------//


//------Private Variables-------//
        private static readonly int Death = Animator.StringToHash("Death");

#region UNITY_METHODS

#endregion


#region PUBLIC_METHODS

        public override void Act(StateComponent statesComponent)
        {
            statesComponent.TryGetComponent(out AIDataHolderCore dataHolder);
            dataHolder.Agent.isStopped = true;
            dataHolder.AnimationController.GetAnimator().SetTrigger(Death);
            DOVirtual.DelayedCall(3f, () => dataHolder.AITransform.DOMoveY(-.3f, 1.5f).SetEase(Ease.Linear).OnComplete(
                () =>
                {
                    dataHolder.AIHealth.ResetHealth();
                    dataHolder.AIHealthProgressBar.OnReset(dataHolder.AIHealth);
                    LeanPool.Despawn(dataHolder);
                }));
        }

#endregion


#region PRIVATE_METHODS

#endregion
    }
}