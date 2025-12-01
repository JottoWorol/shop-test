using System;
using Core.Interfaces;
using Gold.Data;
using UnityEngine;

namespace Gold
{
    public class GoldPlayerResource: IPlayerResource
    {
        public const string RESOURCE_ID = "Gold";
        
        public string Id => RESOURCE_ID;
        public event Action<IResourceAmountData, IPlayerResource> updated;

        private readonly GoldRuntimeData runtimeData;

        public GoldPlayerResource(GoldRuntimeData runtimeData)
        {
            this.runtimeData = runtimeData;
            runtimeData.updated += OnRuntimeDataUpdated;
        }

        public bool CanAfford(IResourceAmountData amountData)
        {
            if (amountData is not GoldAmountData goldAmountData)
            {
                return false;
            }
            
            return CanAfford(goldAmountData);
        }

        public void Deduct(IResourceAmountData amountData)
        {
            if (amountData is not GoldAmountData goldAmountData)
            {
                return;
            }

            if (!CanAfford(goldAmountData))
            {
                return;
            }
            
            runtimeData.SetValue(runtimeData.CurrentValue - goldAmountData.Amount);
        }

        public void Add(IResourceAmountData amountData)
        {
            if (amountData is not GoldAmountData goldAmountData)
            {
                Debug.LogError("Invalid resource amount data type for GoldPlayerResource.");
                return;
            }
            
            runtimeData.SetValue(runtimeData.CurrentValue + goldAmountData.Amount);
        }
        
        private void OnRuntimeDataUpdated(int newAmount)
        {
            var amountData = new GoldAmountData(newAmount);
            updated?.Invoke(amountData, this);
        }
        
        private bool CanAfford(GoldAmountData amountData)
        {
            return runtimeData.CurrentValue >= amountData.Amount;
        }
    }
}