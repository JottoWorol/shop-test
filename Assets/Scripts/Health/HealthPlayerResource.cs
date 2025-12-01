using System;
using Core.Interfaces;
using Health.Data;
using UnityEngine;

namespace  Health
{
    public class HealthPlayerResource : IPlayerResource
    {
        public const string RESOURCE_ID = "Health";
        private const int MAX_HEALTH = 100;
        
        private readonly HealthRuntimeData runtimeData;
        private readonly int minHealthAfterPercentageDamage;
        
        public string Id => RESOURCE_ID;
        public event Action<IResourceAmountData, IPlayerResource> updated;
        
        public HealthPlayerResource(HealthRuntimeData runtimeData, int minHealthAfterPercentageDamage)
        {
            this.minHealthAfterPercentageDamage = minHealthAfterPercentageDamage;
            this.runtimeData = runtimeData;
            runtimeData.updated += OnRuntimeDataUpdated;
        }
        
        public bool CanAfford(IResourceAmountData amountData)
        {
            if (amountData is HealthAmountData healthAmountData)
            {
                //if 1 health left, cannot afford any more damage
                return runtimeData.CurrentValue > healthAmountData.Amount;
            }

            if (amountData is HealthPercentageData healthPercentageData)
            {
                //at least 10% health must remain
                var healthAfterDamage = runtimeData.CurrentValue - Mathf.CeilToInt(healthPercentageData.Percentage / 100f * runtimeData.CurrentValue);
                return healthAfterDamage >= minHealthAfterPercentageDamage;
            }

            return false;
        }

        public void Deduct(IResourceAmountData amountData)
        {
            if (!CanAfford(amountData))
            {
                return;
            }
            
            if (amountData is HealthAmountData healthAmountData)
            {
                runtimeData.SetValue(runtimeData.CurrentValue - healthAmountData.Amount);
                return;
            }
            
            if (amountData is HealthPercentageData healthPercentageData)
            {
                var damageAmount = Mathf.CeilToInt(healthPercentageData.Percentage / 100f * runtimeData.CurrentValue);
                runtimeData.SetValue(runtimeData.CurrentValue - damageAmount);
                return;
            }
        }

        public void Add(IResourceAmountData amountData)
        {
            if (amountData is HealthAmountData healthAmountData)
            {
                var newAmount = Mathf.Min(runtimeData.CurrentValue + healthAmountData.Amount, MAX_HEALTH);
                runtimeData.SetValue(newAmount);
                return;
            }
            
            if (amountData is HealthPercentageData healthPercentageData)
            {
                var healAmount = Mathf.CeilToInt(healthPercentageData.Percentage / 100f * runtimeData.CurrentValue);
                var newAmount = Mathf.Min(runtimeData.CurrentValue + healAmount, MAX_HEALTH);
                runtimeData.SetValue(newAmount);
                return;
            }
        }

        private void OnRuntimeDataUpdated(int amount)
        {
            updated?.Invoke(new HealthAmountData(amount), this);
        }
    }
}