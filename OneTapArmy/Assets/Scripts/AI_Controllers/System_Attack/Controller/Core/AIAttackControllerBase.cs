using System.Collections;
using AI_Controllers.DataHolder.Core;
using AnimationControllers;
using Unity.VisualScripting;
using UnityEngine;
namespace AI_Controllers.System_Attack.Controller.Core
{
    public abstract class AIAttackControllerBase : MonoBehaviour
    {
//-------Public Variables-------//


//------Serialized Fields-------//
        [SerializeField] protected FastAnimationController AnimatorAccess;
        [SerializeField] protected AIDataHolderCore DataHolder;

//------Private Variables-------//
        private static readonly int Attack = Animator.StringToHash("Attack");
        private bool _isInit;
        private AIAnimationEventHandler _attackEventHandler;

#region UNITY_METHODS

        private IEnumerator Start()
        {
            _attackEventHandler = transform.GetChild(0).AddComponent<AIAnimationEventHandler>();
            Subscribe();
            yield return new WaitForEndOfFrame();
            _isInit = true;
        }

        private void OnEnable()
        {
            if (!_isInit)
                return;
            Subscribe();
        }

        private void OnDisable()
        {
            _attackEventHandler.OnAttacked -= OnAttacked;
            _attackEventHandler.OnEndOfAnimation -= OnAttackAnimationEnd;
        }

#endregion


#region PUBLIC_METHODS

        public void StartAttacking()
        {
            AnimatorAccess.GetAnimator().SetTrigger(Attack);
        }

        protected virtual void OnAttacked()
        {
            if (DataHolder.ClosestRivalHealth == null)
                return;
            if (!DataHolder.ClosestRivalHealth.IsDead)
                return;
            var rivalData = DataHolder.ClosestRivalHealth.GetComponent<AIDataHolderCore>();
            if(rivalData==null)
                return;
            rivalData.SpawnedCastle.OnMemberKilledRival?.Invoke();
        }

#endregion


#region PRIVATE_METHODS

        private void OnAttackAnimationEnd()
        {
            DataHolder.IsAttacking = false;
        }

        private void Subscribe()
        {
            _attackEventHandler.OnAttacked += OnAttacked;
            _attackEventHandler.OnEndOfAnimation += OnAttackAnimationEnd;
        }

#endregion
    }
}