using System;
using System.Collections.Generic;
using QuickTools.Scripts.Utilities;
using Sirenix.OdinInspector;
using UnityEngine;
using UpgradeCards.UI.CardUI.Base;
namespace UpgradeCards.Data.Base
{
    public class UpgradeCardSo : ScriptableObject
    {
        [BoxGroup("Design")] public int MaxLevel;
        [BoxGroup("Design")] public int DefaultCardLevel;
        [BoxGroup("References"), PreviewField] public List<Sprite> CardImages;
        [BoxGroup("References"), PreviewField] public Sprite BackgroundImage;
        [BoxGroup("References"), PreviewField] public Sprite CardNameSprite;
        [BoxGroup("References")] public UpgradableCardUIBase CardUI;
        [ReadOnly, BoxGroup("Config")] public int CurrentCardLevel;

        public Action OnUpgraded;

        [Button]
        public void ResetCard() => CurrentCardLevel = DefaultCardLevel;

        [PreviewField] public Sprite TargetCardImage() =>
            CardImages[DefaultCardLevel == 0 ? CurrentCardLevel : CurrentCardLevel - 1];

        [Button]
        public void UpgradeCard()
        {
            if (IsReachedMax())
            {
                EditorDebug.Log("Reached Max" + this.name);
                return;
            }
            CurrentCardLevel++;
            OnUpgraded?.Invoke();
        }

        public bool IsReachedMax() => CurrentCardLevel >= MaxLevel;
    }
}