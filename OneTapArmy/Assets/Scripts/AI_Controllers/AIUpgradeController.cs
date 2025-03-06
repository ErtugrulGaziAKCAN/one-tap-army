using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AI_Controllers.DataHolder.Core;
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


//------Serialized Fields-------//
        [SerializeField, BoxGroup("Design")] private SoldierUpgradeCardSo TargetSoldierSo;
        [SerializeField, BoxGroup("References")] private HealthCore AIHealth;
        [SerializeField, BoxGroup("References")] private AIDataHolderCore AIDataHolder;
        [SerializeField, BoxGroup("Events")] private UnityEvent OnUpgradedEvent;
        [SerializeField, BoxGroup("Events")] private List<AIUpgradeSkins> UpgradeSkins;

//------Private Variables-------//
        private bool _isInit;

#region UNITY_METHODS

        private void OnEnable()
        {
            TargetSoldierSo.OnUpgraded += ApplyUpgrades;
            StartCoroutine(WaitForOneFrame(() =>
            {
                ApplyUpgrades();
                StartCoroutine(WaitForOneFrame(() => _isInit = true));
            }));
        }

        private void OnDisable()
        {
            _isInit = false;
            TargetSoldierSo.OnUpgraded -= ApplyUpgrades;
        }

#endregion


#region PUBLIC_METHODS

#endregion


#region PRIVATE_METHODS

        private void ApplyUpgrades()
        {
            var cardLevel = TargetSoldierSo.CurrentCardLevel;
            AIHealth.SetMaxHealth(TargetSoldierSo.HealthValue.GetValueOnLevel(cardLevel));
            AIDataHolder.SetAgentSpeed(TargetSoldierSo.SpeedValue.GetValueOnLevel(cardLevel));
            AIDataHolder.SetAttackDamage(TargetSoldierSo.AttackValue.GetValueOnLevel(cardLevel));
            UpdateSkin(cardLevel);
            if (_isInit)
                OnUpgradedEvent?.Invoke();
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