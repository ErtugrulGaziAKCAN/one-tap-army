using System.Collections;
using AI_Controllers.DataHolder;
using AI_Controllers.DataHolder.Core;
using AnimationControllers;
using DG.Tweening;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
namespace AI_Controllers.System_Attack.Controller.Core
{
    public abstract class AIAttackControllerBase : MonoBehaviour
    {
//-------Public Variables-------//


//------Serialized Fields-------//
        [SerializeField] private bool SetActiveAnimation = true;
        [SerializeField, ShowIf(nameof(SetActiveAnimation))] protected FastAnimationController AnimatorAccess;
        [SerializeField] protected AIDataHolderCore DataHolder;
        [SerializeField] private UnityEvent OnStartAttacking;
        [SerializeField] private bool StopAttackingWithDelay;
        [SerializeField, ShowIf(nameof(StopAttackingWithDelay))] private float StopAttackDelay;
        [SerializeField] private bool AttackWithDelay;
        [SerializeField, ShowIf(nameof(AttackWithDelay))] private float AttackDelay;

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
            if (SetActiveAnimation)
                AnimatorAccess.GetAnimator().SetTrigger(Attack);
            OnStartAttacking?.Invoke();
            if (StopAttackingWithDelay)
                DOVirtual.DelayedCall(StopAttackDelay, OnAttackAnimationEnd);
            if (AttackWithDelay)
                DOVirtual.DelayedCall(AttackDelay, OnAttacked);
        }

        protected virtual void OnAttacked()
        {
            if (DataHolder.ClosestRivalHealth == null)
                return;
            if (!DataHolder.ClosestRivalHealth.IsDead)
                return;
            var rivalData = DataHolder.ClosestRivalHealth.GetComponent<SoldierAIDataHolderCore>();
            if (rivalData == null)
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