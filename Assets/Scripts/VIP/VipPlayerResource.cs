using System;
using Core.Interfaces;
using VIP.Data;

namespace VIP
{
    class VipPlayerResource: IPlayerResource
    {
        public const string RESOURCE_ID = "Vip";

        private VipRuntimeData runtimeData;
    
        public VipPlayerResource(VipRuntimeData runtimeData)
        {
            this.runtimeData = runtimeData;
            runtimeData.updated += OnRuntimeDataUpdated;
        }

        public event Action<IResourceAmountData, IPlayerResource> updated;

        public string Id => RESOURCE_ID;

        public bool CanAfford(IResourceAmountData amountData)
        {
            if (amountData is not VipAmountData vipAmountData)
            {
                return false;
            }
        
            return runtimeData.CurrentVipTime >= vipAmountData.VipTimeAmount;
        }

        public void Deduct(IResourceAmountData amountData)
        {
            if (amountData is not VipAmountData vipAmountData)
            {
                return;
            }
        
            if (!CanAfford(vipAmountData))
            {
                return;
            }
        
            runtimeData.SetVipTime(runtimeData.CurrentVipTime - vipAmountData.VipTimeAmount);
        }

        public void Add(IResourceAmountData amountData)
        {
            if (amountData is not VipAmountData vipAmountData)
            {
                return;
            }
        
            runtimeData.SetVipTime(runtimeData.CurrentVipTime + vipAmountData.VipTimeAmount);
        }

        private void OnRuntimeDataUpdated(TimeSpan value)
        {
            updated?.Invoke(new VipAmountData(value), this);
        }
    }
}