using System;
using Core;
using Cysharp.Threading.Tasks;
using Health.Data;
using Health.Views;
using UnityEngine;

namespace Health
{
    public class HealthSystem: GameSystemBase
    {
        [SerializeField] private HealthCounterView counterView;
        [SerializeField] private HealthSystemResources resources;
        
        private HealthRuntimeData runtimeData;
        private HealthPlayerResource playerResource;
        private HealthCounterController counterController;

        public override UniTask InitializeSystem()
        {
            try
            {
                if (counterView == null)
                {
                    throw new InvalidOperationException("HealthCounterView is not assigned in HealthSystem");
                }
                
                if (resources == null)
                {
                    throw new InvalidOperationException("HealthSystemResources is not assigned in HealthSystem");
                }
                
                runtimeData = new HealthRuntimeData();
                playerResource = new HealthPlayerResource(runtimeData, resources.MinHealthAfterPercentageDamage);

                PlayerData.Instance.TryAddResourceData(playerResource);

                counterController = new HealthCounterController(counterView);
                counterController.cheatHealthRequested += OnCheatHealthRequested;

                runtimeData.updated += OnHealthUpdated;
                runtimeData.SetValue(resources.StartingHealthAmount);

                return UniTask.CompletedTask;
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to initialize HealthSystem: {e.Message}");
                return UniTask.CompletedTask;
            }
        }

        private void OnCheatHealthRequested()
        {
            runtimeData.SetValue(runtimeData.CurrentValue + resources.CheatAddAmount);
        }

        private void OnHealthUpdated(int count)
        {
            counterController.SetCounter(count);
        }
    }
}