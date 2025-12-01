using Core.Interfaces;
using UnityEngine;

namespace  Health.Data
{
    public class HealthAmountData: IResourceAmountData
    {
        public string ResourceId => HealthPlayerResource.RESOURCE_ID;

        public int Amount { get; private set; }
        
        public HealthAmountData(int amount)
        {
            if (amount < 0)
            {
                Debug.LogError("Amount cannot be negative");
                amount = 0;
            }
            
            Amount = amount;
        }
        
        public IResourceAmountData SumWith(IResourceAmountData other)
        {
            if (other is not HealthAmountData otherHealthAmountData)
            {
                throw new System.ArgumentException($"Cannot sum {nameof(HealthAmountData)} with {other.GetType().Name}");
            }

            return new HealthAmountData(Amount + otherHealthAmountData.Amount);
        }
    }
}