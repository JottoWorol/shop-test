using System;
using Core.Interfaces;
using UnityEngine;

namespace Gold.Data
{
    public class GoldAmountData: IResourceAmountData
    {
        public string ResourceId => GoldPlayerResource.RESOURCE_ID;

        public int Amount { get; private set; }
        
        public GoldAmountData(int amount)
        {
            if (amount < 0)
            {
                Debug.LogError("amount cannot be negative");
                amount = 0;
            }
            
            Amount = amount;
        }
        
        public IResourceAmountData SumWith(IResourceAmountData other)
        {
            if (other is not GoldAmountData otherGoldAmountData)
            {
                throw new ArgumentException($"Cannot sum {nameof(GoldAmountData)} with {other.GetType().Name}");
            }

            return new GoldAmountData(Amount + otherGoldAmountData.Amount);
        }
    }
}