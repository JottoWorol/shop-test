using System;
using Core;
using Cysharp.Threading.Tasks;
using Gold.Data;
using Gold.Views;
using UnityEngine;

namespace Gold
{
    public class GoldSystem: GameSystemBase
    {
        [SerializeField] private GoldCounterView counterView;
        [SerializeField] private GoldSystemResources config;
        
        private GoldRuntimeData runtimeGoldAmount;
        private GoldPlayerResource playerResource;
        private GoldCounterController counterController;

        public override UniTask InitializeSystem()
        {
            try
            {
                if (counterView == null)
                {
                    throw new InvalidOperationException("GoldCounterView is not assigned in GoldSystem");
                }
                
                if (config == null)
                {
                    throw new InvalidOperationException("GoldSystemResources is not assigned in GoldSystem");
                }
                
                runtimeGoldAmount = new GoldRuntimeData();
                playerResource = new GoldPlayerResource(runtimeGoldAmount);

                PlayerData.Instance.TryAddResourceData(playerResource);

                counterController = new GoldCounterController(counterView);
                counterController.cheatGoldRequested += OnCheatGoldRequested;

                runtimeGoldAmount.updated += OnGoldUpdated;
                runtimeGoldAmount.SetValue(config.StartingGoldAmount);

                return UniTask.CompletedTask;
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to initialize GoldSystem: {e.Message}");
                return UniTask.CompletedTask;
            }
        }

        private void OnCheatGoldRequested()
        {
            runtimeGoldAmount.SetValue(runtimeGoldAmount.CurrentValue + config.CheatAddAmount);
        }

        private void OnGoldUpdated(int count)
        {
            counterController.SetCounter(count);
        }
    }
}