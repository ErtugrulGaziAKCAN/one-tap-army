using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AI_Controllers.DataHolder;
using Enemy_Controllers.Upgrades;
using QuickTools.Scripts.HealthSystem;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UpgradeCards.Data;
namespace AI_Controllers
{
    public class AIUpgradeController : MonoBehaviour
    {
//-------Public Variables-------//
        [SerializeField, BoxGroup("Design")] public SoldierUpgradeCardSo TargetSoldierSo;

//------Serialized Fields-------//
        [SerializeField, BoxGroup("References")] private HealthCore AIHealth;
        [SerializeField, BoxGroup("References")] private SoldierAIDataHolderCore SoldierAIDataHolder;
        [SerializeField, BoxGroup("Events")] private UnityEvent OnUpgradedEvent;
        [SerializeField, BoxGroup("Events")] private List<AIUpgradeSkins> UpgradeSkins;

//------Private Variables-------//
        private bool _isInit;

#region UNITY_METHODS

        private void OnEnable()
        {
            if (SoldierAIDataHolder.IsAllyAI)
                TargetSoldierSo.OnUpgraded += CardUpgraded;
            StartCoroutine(WaitForOneFrame(() =>
            {
                ApplyUpgrades(SoldierAIDataHolder.IsAllyAI
                    ? TargetSoldierSo.CurrentCardLevel
                    : SoldierAIDataHolder.SpawnedCastle.GetComponentInChildren<EnemyAIUpgrade>().GetSoldierLevel(TargetSoldierSo));
                StartCoroutine(WaitForOneFrame(() => _isInit = true));
            }));
        }

        private void OnDisable()
        {
            _isInit = false;
            if (SoldierAIDataHolder.IsAllyAI)
                TargetSoldierSo.OnUpgraded -= CardUpgraded;
        }

#endregion


#region PUBLIC_METHODS

        public void ApplyUpgrades(int level)
        {
            AIHealth.SetMaxHealth(TargetSoldierSo.HealthValue.GetValueOnLevel(level));
            SoldierAIDataHolder.SetAgentSpeed(TargetSoldierSo.SpeedValue.GetValueOnLevel(level));
            SoldierAIDataHolder.SetAttackDamage(TargetSoldierSo.AttackValue.GetValueOnLevel(level));
            UpdateSkin(level);
            if (_isInit)
                OnUpgradedEvent?.Invoke();
        }

#endregion


#region PRIVATE_METHODS

        private void CardUpgraded()
        {
            ApplyUpgrades(TargetSoldierSo.CurrentCardLevel);
        }

        private IEnumerator WaitForOneFrame(Action action)
        {
            yield return new WaitForEndOfFrame();
            action?.Invoke();
        }

        private void UpdateSkin(int cardLevel)
        {
            if (UpgradeSkins.Count == 0)
                return;
            UpgradeSkins.ForEach((u) => u.Skin.SetActive(false));
            var targetSkin = UpgradeSkins.FirstOrDefault((u) => u.TargetLevel == cardLevel);
            if (targetSkin.Skin == null)
                return;
            targetSkin.Skin.SetActive(true);
            targetSkin.OnChange?.Invoke();
        }

#endregion
    }
    [Serializable]
    public struct AIUpgradeSkins
    {
        public GameObject Skin;
        public int TargetLevel;
        public UnityEvent OnChange;
    }
}