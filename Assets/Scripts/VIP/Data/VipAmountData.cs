using System;
using Core.Interfaces;
using UnityEngine;

namespace VIP.Data
{
    public class VipAmountData : IResourceAmountData
    {
        public string ResourceId => VipPlayerResource.RESOURCE_ID;
        public TimeSpan VipTimeAmount { get; private set; }

        public VipAmountData(TimeSpan vipTimeAmount)
        {
            if (vipTimeAmount.TotalMilliseconds < 0)
            {
                Debug.LogError("vipTimeAmount cannot be negative");
                vipTimeAmount = TimeSpan.Zero;
            }
            
            VipTimeAmount = vipTimeAmount;
        }

        public IResourceAmountData SumWith(IResourceAmountData other)
        {
            if (other is not VipAmountData otherVip)
                throw new ArgumentException($"Cannot sum {nameof(VipAmountData)} with {other.GetType().Name}");

            return new VipAmountData(VipTimeAmount + otherVip.VipTimeAmount);
        }
    }
}

