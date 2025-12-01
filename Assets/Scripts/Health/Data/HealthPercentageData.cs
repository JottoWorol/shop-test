using System;
using Core.Interfaces;
using UnityEngine;

namespace Health.Data
{
    public class HealthPercentageData: IResourceAmountData
    {
        public string ResourceId => HealthPlayerResource.RESOURCE_ID;
        public int Percentage { get; }
        
        public HealthPercentageData(int percentage)
        {
            Percentage = percentage;
        }
        
        public IResourceAmountData SumWith(IResourceAmountData other)
        {
            if (other is not HealthPercentageData otherHealthPercentageData)
            {
                throw new ArgumentException($"Cannot sum {nameof(HealthPercentageData)} with {other.GetType().Name}");
            }

            var clampedSum = Mathf.Min(Percentage + otherHealthPercentageData.Percentage, 100);
            return new HealthPercentageData(clampedSum);
        }
    }
}