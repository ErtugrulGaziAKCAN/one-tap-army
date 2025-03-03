using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UI;
using UnityEngine;
namespace QuickTools.Scripts.HealthSystem
{
    public class HealthBar : MonoBehaviour
    {
//-------Public Variables-------//


//------Serialized Fields-------//
        [SerializeField, Required] private HealthCore HealthController;

        [SerializeField, FoldoutGroup("Internal Refs")]
        private TextMeshPro HealthText;

        [SerializeField] private GameObject BarsParent;

        [SerializeField, FoldoutGroup("Internal Refs")]
        private ProgressBarController CurrentHealthBar;

        [SerializeField, FoldoutGroup("Internal Refs")]
        private ProgressBarController DamageDelayedBar;

        [SerializeField, FoldoutGroup("Internal Refs")]
        private ProgressBarController HealDelayedBar;

//------Private Variables-------//
        private int _currentHealth;
        private Tween _damageBarTween;
        private Tween _healBarTween;
        private Tween _delayedTextTween;
        private bool _isTweening;


#region UNITY_METHODS

        private void OnEnable()
        {
            HealthController.OnHealthChanged += UpdateHealth;
            HealthController.OnReset += OnReset;
            _currentHealth = Mathf.CeilToInt(HealthController.CurrentHealth);
        }

        private void OnDisable()
        {
            HealthController.OnHealthChanged -= UpdateHealth;
            HealthController.OnReset -= OnReset;
        }

        private void Start()
        {
            UpdateHealth(0f);
        }

#endregion


#region PUBLIC_METHODS

#endregion


#region PRIVATE_METHODS

        private void UpdateHealth(float _)
        {
            if (_isTweening)
                KillTween();
            var oldHealth = _currentHealth;
            if (oldHealth < HealthController.CurrentHealth)
                StartDelayedHeal();
            else
                StartDelayedDamage();
        }

        private void UpdateHealthText()
        {
            HealthText.text = $"{_currentHealth}";
        }

        private void StartDelayedDamage()
        {
            _isTweening = true;
            var oldProgress = CurrentHealthBar.Progress;
            DamageDelayedBar.Progress = oldProgress;
            var healthPercentage = HealthController.HealthPercentage;
            CurrentHealthBar.Progress = healthPercentage;
            HealDelayedBar.Progress = 0f;
            _damageBarTween = DOVirtual.Float(oldProgress, healthPercentage, 0.15f, UpdateDelayedDamageBar)
                .SetDelay(.15f);
            var oldHealth = _currentHealth;
            var newHealth = Mathf.CeilToInt(HealthController.CurrentHealth);
            _delayedTextTween = DOVirtual.Float(oldHealth, newHealth, 0.15f, UpdateDelayedText)
                .SetDelay(.15f)
                .OnComplete(OnTweenComplete);
        }

        private void StartDelayedHeal()
        {
            _isTweening = true;
            var oldProgress = CurrentHealthBar.Progress;
            HealDelayedBar.Progress = HealthController.CurrentHealth / HealthController.GetMaxHealth;
            DamageDelayedBar.Progress = 0f;
            var newProgress = HealthController.CurrentHealth / HealthController.GetMaxHealth;
            _healBarTween = DOVirtual.Float(oldProgress, newProgress, 0.15f, UpdateDelayedHealBar)
                .SetDelay(.15f);
            var oldHealth = _currentHealth;
            var newHealth = Mathf.CeilToInt(HealthController.CurrentHealth);
            _delayedTextTween = DOVirtual.Float(oldHealth, newHealth, 0.15f, UpdateDelayedText)
                .SetDelay(.15f)
                .OnComplete(OnTweenComplete);
        }

        private void UpdateDelayedHealBar(float value)
        {
            CurrentHealthBar.Progress = value;
        }

        private void UpdateDelayedText(float value)
        {
            _currentHealth = Mathf.CeilToInt(value);
            UpdateHealthText();
        }

        private void UpdateDelayedDamageBar(float value)
        {
            DamageDelayedBar.Progress = value;
        }

        private void KillTween()
        {
            _currentHealth = Mathf.CeilToInt(HealthController.CurrentHealth);
            UpdateHealthText();
            _healBarTween?.Kill();
            _damageBarTween?.Kill();
            _delayedTextTween?.Kill();
        }

        private void OnTweenComplete()
        {
            _isTweening = false;
            if (HealthController.IsDead)
                BarsParent.SetActive(false);
        }

        private void OnReset(HealthCore _)
        {
            _currentHealth = Mathf.CeilToInt(HealthController.CurrentHealth);
            UpdateHealthText();
            CurrentHealthBar.Progress = 1f;
            HealDelayedBar.Progress = 0f;
            DamageDelayedBar.Progress = 0f;
            BarsParent.SetActive(true);
        }

#endregion
    }
}