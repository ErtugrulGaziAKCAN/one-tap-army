using System;
using Sirenix.OdinInspector;
using UnityEngine;
namespace QuickTools.Scripts.HealthSystem
{
    public abstract class HealthCore : MonoBehaviour
    {
//-------Public Variables-------//
        public Action<float> OnHealthChanged;
        public Action<HealthCore> OnDeath;
        public Action<HealthCore> OnReset;
        public bool IsDead => CurrentHealth <= 0;
        public float HealthPercentage => CurrentHealth / MaxHealth;
        public float GetMaxHealth => MaxHealth;

        public float CurrentHealth
        {
            get => _currentHealth;
            set
            {
                var delta = value - _currentHealth;
                _currentHealth = value;
                OnHealthChanged?.Invoke(delta);
            }
        }


//------Serialized Fields-------//
        [SerializeField] private float MaxHealth = 100f;


//------Private Variables-------//
        private float _currentHealth;


#region UNITY_METHODS

        protected virtual void Awake()
        {
            CurrentHealth = MaxHealth;
        }

#endregion


#region PUBLIC_METHODS

        [Button]
        public virtual void TakeDamage(float damage)
        {
            CurrentHealth -= damage;
            if (CurrentHealth > 0)
                return;

            CurrentHealth = 0;
            Die();
            OnDeath?.Invoke(this);
        }

        [Button]
        public virtual void Heal(float amount)
        {
            CurrentHealth += amount;
            if (CurrentHealth > MaxHealth)
                CurrentHealth = MaxHealth;
        }

        [Button]
        public virtual void ResetHealth()
        {
            CurrentHealth = MaxHealth;
            OnReset?.Invoke(this);
        }

#endregion


#region PRIVATE_METHODS

        protected abstract void Die();

#endregion
    }
}