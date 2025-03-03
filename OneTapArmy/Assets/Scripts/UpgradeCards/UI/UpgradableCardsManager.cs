using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using MonKey.Extensions;
using Obvious.Soap;
using Plugins.CW.LeanPool.Required.Scripts;
using QuickTools.Scripts.TimeSystem;
using QuickTools.Scripts.Utilities;
using Sirenix.OdinInspector;
using UnityEngine;
using UpgradeCards.Data.Base;
using UpgradeCards.UI.CardUI.Base;
namespace UpgradeCards.UI
{
    public class UpgradableCardsManager : MonoBehaviour
    {
//-------Public Variables-------//


//------Serialized Fields-------//
        [SerializeField] private ScriptableEventNoParam OnUpgradableSelected;
        [SerializeField] private GameObject Visuals;
        [SerializeField] private List<UpgradeCardSo> AllCards;
        [SerializeField] private Transform CardSpawnParent;

//------Private Variables-------//
        private List<UpgradableCardUIBase> _spawnedCards = new List<UpgradableCardUIBase>();

#region UNITY_METHODS

        private void OnEnable()
        {
            OnUpgradableSelected.OnRaised += DeActivateUpgradePanel;
            AllCards.ForEach((c) => c.ResetCard());
        }

        private void OnDisable()
        {
            OnUpgradableSelected.OnRaised -= DeActivateUpgradePanel;
        }

#endregion


#region PUBLIC_METHODS

        [Button]
        public void SpawnNewCardsUI()
        {
            _spawnedCards = new List<UpgradableCardUIBase>();
            var cards = AllCards.Where((c) => !c.IsReachedMax()).ToList();
            cards.Shuffle();
            cards = cards.Take(3).ToList();
            foreach (var card in cards)
            {
                if (card == null)
                    continue;
                var spawned = LeanPool.Spawn(card.CardUI, CardSpawnParent);
                spawned.InitCard(card);
                _spawnedCards.Add(spawned);
            }
            ActivateUpgradePanel();
        }



        [Button]
        private void DeActivateUpgradePanel()
        {
            EditorDebug.Log("Worked");
            var delay = 1f;
            _spawnedCards.ForEach((s) =>
            {
                s.DeInteractable();
                DOVirtual.DelayedCall(delay, () =>
                {
                    s.transform.DOScale(Vector3.zero, .45f).SetEase(Ease.OutBack).OnComplete(() => LeanPool.Despawn(s))
                        .SetUpdate(true);
                }).SetUpdate(true);
            });
            DOVirtual.DelayedCall(delay, () =>
            {
                TimescaleManager.ChangeTimescale(1f, .7f);
                Visuals.transform.DOScale(Vector3.zero, .3f).SetEase(Ease.OutBack).SetUpdate(true)
                    .OnComplete(() => Visuals.SetActive(false));
            }).SetUpdate(true);
        }

#endregion


#region PRIVATE_METHODS

        private void ActivateUpgradePanel()
        {
            TimescaleManager.ChangeTimescale(0f, .7f);
            Visuals.SetActive(true);
        }

#endregion
    }
}