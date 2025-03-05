using System;
using AI_Controllers.System_Attack.Controller.Core;
using AnimationControllers;
using QuickTools.Scripts.Collectibles.Core;
using scriptable_states.Runtime;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;
namespace AI_Controllers.DataHolder.Core
{
    public abstract class AIDataHolderCore : MonoBehaviour
    {
//-------Public Variables-------//
        [BoxGroup("Design")] public int AI_ID;
        [BoxGroup("Design")] public float CollectibleSensorRange;
        [HideInInspector] public CollectibleCore ClosestCollectible;
        [BoxGroup("Design")] public float RivalSensorRange;
        [BoxGroup("Design")] public float AttackDistance;
        [BoxGroup("Design")] public float AttackDamage;
        [ReadOnly] public bool IsAttacking;
        [BoxGroup("Design")] public LayerMask RivalLayer;
        [HideInInspector] public AIHealthController ClosestRivalHealth;
        [BoxGroup("References")] public Transform AITransform;
        [BoxGroup("References")] public NavMeshAgent Agent;
        [BoxGroup("References")] public AIHealthController AIHealth;
        [BoxGroup("References")] public StateComponent StateComponentAccess;
        [BoxGroup("References")] public AIAttackControllerBase AIAttackController;
        [BoxGroup("AnimationData")] public FastAnimationController AnimationController;
        [BoxGroup("Config"), ReadOnly] public Vector3 TargetPosition;
        [HideInInspector] public bool IsAllyAI;
        [HideInInspector] public float AgentStoppingDistance;

//------Serialized Fields-------//


//------Private Variables-------//

#region UNITY_METHODS

        private void OnEnable()
        {
            IsAttacking = false;
        }
        
        private void Start()
        {
            AgentStoppingDistance = Agent.stoppingDistance;
        }

#endregion


#region PUBLIC_METHODS

#endregion


#region PRIVATE_METHODS

#endregion
    }
}