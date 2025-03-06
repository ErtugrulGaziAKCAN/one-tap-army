using AI_Controllers.System_Attack.Controller.Core;
using QuickTools.Scripts.HealthSystem;
using Sirenix.OdinInspector;
using UnityEngine;
namespace AI_Controllers.DataHolder.Core
{
    public abstract class AIDataHolderCore : MonoBehaviour
    {

//-------Public Variables-------//
        [BoxGroup("Design")] public float AttackDistance;
        [BoxGroup("Design")] public float AttackDamage;
        [BoxGroup("Design")] public LayerMask RivalLayer;
        [BoxGroup("Design")] public float RivalSensorRange;
        [BoxGroup("References")] public Transform AITransform;
        [BoxGroup("References")] public HealthCore AIHealth;
        [BoxGroup("References")] public AIAttackControllerBase AIAttackController;
        [ReadOnly] public bool IsAttacking;
        [ReadOnly] public HealthCore ClosestRivalHealth;
        
//------Serialized Fields-------//


//------Private Variables-------//

#region UNITY_METHODS

#endregion


#region PUBLIC_METHODS

#endregion


#region PRIVATE_METHODS

#endregion
        
    }
}