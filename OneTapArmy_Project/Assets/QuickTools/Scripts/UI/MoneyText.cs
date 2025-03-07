using System;
using DG.Tweening;
using QuickTools.Scripts.Manager;
using QuickTools.Scripts.Utilities;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
namespace QuickTools.Scripts.UI
{
    public class MoneyText : MonoBehaviour
    {
        [SerializeField, Required] private TextMeshProUGUI Text;
        [SerializeField, Indent] private string Prefix = "";
        [SerializeField, Indent] private string Suffix = "";

        private int _visualValue;
        private Tweener _valueTween;
        private const float ANIMATION_TIME = 1.1f;

        private void Awake()
        {
            BalanceManager.OnBalanceChange += AnimateCounter;
        }

        private void OnEnable()
        {
            BalanceManager.ActiveBalanceTexts.Add(transform);
            _visualValue = BalanceManager.CurrentBalance;
            UpdateText();
        }

        private void OnDisable()
        {
            BalanceManager.ActiveBalanceTexts.Remove(transform);
        }

        private void OnDestroy()
        {
            BalanceManager.OnBalanceChange -= AnimateCounter;
        }

        private void Start()
        {
            AnimateCounter(false);
        }

        private void AnimateCounter(bool animate)
        {
            if (!(Math.Abs(_visualValue - BalanceManager.CurrentBalance) > .1f))
                return;

            if (animate)
            {
                const float delay = 1f;
                _valueTween?.Kill();
                BalanceManager.ActiveBalanceText.DOKill();
                _valueTween = DOTween
                    .To(() => _visualValue, x => _visualValue = x, BalanceManager.CurrentBalance,
                        ANIMATION_TIME)
                    .SetEase(Ease.Linear).SetDelay(delay).OnUpdate(UpdateText);
                BalanceManager.ActiveBalanceText.DOScale(1.2f, .1f).SetEase(Ease.InOutQuad).SetDelay(delay - .5f);
                BalanceManager.ActiveBalanceText.DOScale(1f, .1f).SetEase(Ease.InBack).SetDelay(delay + 1.1f);
            }
            else
            {
                _visualValue = BalanceManager.CurrentBalance;
                UpdateText();
            }
        }

        private void UpdateText()
        {
            Text.SetText(Prefix + MoneyFormat.DefaultWithoutIcon(_visualValue) + Suffix);
        }
    }
}