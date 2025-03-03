using System;
using System.Collections.Generic;
using QuickTools.Scripts.Utilities;
using Sirenix.OdinInspector;
using UnityEngine;
namespace UpgradeCards.Base
{
    public class UpgradeCardSo : ScriptableObject
    {
        [BoxGroup("Design")] public int MaxLevel;
        [BoxGroup("References")] public string CardName;
        [BoxGroup("References"), PreviewField] public List<Sprite> CardImages;
        [BoxGroup("References"), PreviewField] public Sprite BackgroundImage;
        [BoxGroup("References"), PreviewField] public Sprite CardNameSprite;
        [ReadOnly, BoxGroup("Config")] public int CurrentCardLevel = 1;

        public Action OnUpgraded;

        public void ResetCard() => CurrentCardLevel = 1;

        [PreviewField] public Sprite TargetCardImage() => CardImages[CurrentCardLevel - 1];

        [Button]
        public void UpgradeCard()
        {
            if (CurrentCardLevel >= MaxLevel)
            {
                EditorDebug.Log("Reached Max" + this.name);
                return;
            }
            CurrentCardLevel++;
        }
    }
}