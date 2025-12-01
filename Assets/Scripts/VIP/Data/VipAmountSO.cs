using System;
using Core;
using Core.Interfaces;
using Core.PlayerResources;
using UnityEngine;

namespace VIP.Data
{
    [CreateAssetMenu(fileName = "VipTimeAmountData", menuName = "Custom/VipSystem/VIP Time Amount Data")]
    public class VipAmountSO : ResourceAmountSOBase
    {
        [field: SerializeField] public int VipTimeSeconds { get; private set; } = 10;
        public override IResourceAmountData ResourceAmount => new VipAmountData(new TimeSpan(0, 0, VipTimeSeconds));
    }
}