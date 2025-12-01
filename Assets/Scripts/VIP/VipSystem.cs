using System;
using Core;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VIP.Data;
using VIP.Views;

namespace VIP
{
    public class VipSystem: GameSystemBase
    {
        [SerializeField] private VipTimerView vipTimerView;
        [SerializeField] private VipSystemResources vipSystemResources;
        
        private VipTimerController vipTimerController;
        private VipRuntimeData runtimeData;

        public override UniTask InitializeSystem()
        {
            runtimeData = new VipRuntimeData();
            var playerResource = new VipPlayerResource(runtimeData);
            vipTimerController = new VipTimerController(vipTimerView);
            vipTimerController.onAddButtonClicked += OnCheatButtonClicked;
            
            PlayerData.Instance.TryAddResourceData(playerResource);
            
            runtimeData.updated += OnVipTimeUpdated;
            runtimeData.SetVipTime(new TimeSpan(0, 0, vipSystemResources.StartingVipTimeSeconds));
            
            return UniTask.CompletedTask;
        }

        private void OnCheatButtonClicked()
        {
            runtimeData.SetVipTime(runtimeData.CurrentVipTime + new TimeSpan(0, 0, vipSystemResources.CheatAddTimeSeconds));
        }

        private void OnVipTimeUpdated(TimeSpan value)
        {
            vipTimerController.UpdateVipTimerText(value);
        }
    }
}