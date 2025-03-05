using System.Collections.Generic;
using System.Linq;
using AI_Controllers.System_Attack.Controller.Core;
using AnimationControllers;
using Player.Points;
using QuickTools.Scripts.Collectibles.Core;
using QuickTools.Scripts.HealthSystem;
using QuickTools.Scripts.UI;
using QuickTools.Scripts.Utilities;
using scriptable_states.Runtime;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;
namespace AI_Controllers.DataHolder.Core
{
    public abstract class AIDataHolderCore : MonoBehaviour
    {
//-------Public Variables-------//
        [BoxGroup("Design")] public Color TargetAIColor;
        [BoxGroup("Design")] public float CollectibleSensorRange;
        [HideInInspector] public CollectibleCore ClosestCollectible;
        [BoxGroup("Design")] public float RivalSensorRange;
        [BoxGroup("Design")] public float AttackDistance;
        [BoxGroup("Design")] public float AttackDamage;
        [ReadOnly] public bool IsAttacking;
        [BoxGroup("Design")] public LayerMask RivalLayer;
        [HideInInspector] public HealthCore ClosestRivalHealth;
        [BoxGroup("References")] public Transform AITransform;
        [BoxGroup("References")] public NavMeshAgent Agent;
        [BoxGroup("References")] public HealthCore AIHealth;
        [BoxGroup("References")] public HealthBar AIHealthProgressBar;
        [BoxGroup("References")] public StateComponent StateComponentAccess;
        [BoxGroup("References")] public AIAttackControllerBase AIAttackController;
        [BoxGroup("References")] public UiColorizeGroup CurrentHealthColorize;
        [BoxGroup("References")] public ScriptableListAIDataHolderCore SpawnedAllies;
        [BoxGroup("Config"), ReadOnly] public Vector3 TargetPosition;
        [ReadOnly] public bool IsAllyAI;
        [BoxGroup("AnimationData")] public FastAnimationController AnimationController;
        [HideInInspector] public float AgentStoppingDistance;

//------Serialized Fields-------//


//------Private Variables-------//
        private List<SkinnedMeshRenderer> _skinnedMeshes;

#region UNITY_METHODS

        private void Start()
        {
            AgentStoppingDistance = Agent.stoppingDistance;
        }

        private void OnEnable()
        {
            IsAttacking = false;
            SetAIColor();
            AIHealth.OnDeath += OnDeath;
        }

        private void OnDisable()
        {
            AIHealth.OnDeath -= OnDeath;
        }

#endregion


#region PUBLIC_METHODS

#endregion


#region PRIVATE_METHODS

        private void SetAIColor()
        {
            _skinnedMeshes = AnimationController.GetComponentsInChildren<SkinnedMeshRenderer>().ToList();
            _skinnedMeshes.ForEach((s) => s.materials[0].color = TargetAIColor);
            CurrentHealthColorize.UiColor = TargetAIColor;
            CurrentHealthColorize.ApplyColors();
        }

        private void OnDeath(HealthCore health)
        {
            _skinnedMeshes.ForEach((s) => s.materials[0].color = QuickColors.LightGrey);
            if (IsAllyAI)
                SpawnedAllies.Remove(this);
            else
                PlayerXpController.Instance.AddXp(1);
        }

#endregion
    }
}