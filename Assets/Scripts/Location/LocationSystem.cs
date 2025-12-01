using System;
using Core;
using Cysharp.Threading.Tasks;
using Location.Data;
using Location.Views;
using UnityEngine;

namespace Location
{
    public class LocationSystem : GameSystemBase
    {
        [SerializeField] private LocationLabelView labelView;
        [SerializeField] private LocationSystemResources resources;
        
        private LocationRuntimeData runtimeData;
        private LocationPlayerResource playerResource;
        private LocationLabelController labelController;

        public override UniTask InitializeSystem()
        {
            try
            {
                if (labelView == null)
                {
                    throw new InvalidOperationException("LocationLabelView is not assigned in LocationSystem");
                }
                
                if (resources == null)
                {
                    throw new InvalidOperationException("LocationSystemResources is not assigned in LocationSystem");
                }
                
                runtimeData = new LocationRuntimeData();
                playerResource = new LocationPlayerResource(runtimeData);

                PlayerData.Instance.TryAddResourceData(playerResource);

                labelController = new LocationLabelController(labelView);
                labelController.cheatResetRequested += OnCheatResetRequested;

                runtimeData.updated += OnLocationUpdated;
                runtimeData.SetValue(resources.StartingLocation.LocationName);

                return UniTask.CompletedTask;
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to initialize LocationSystem: {e.Message}");
                return UniTask.CompletedTask;
            }
        }

        private void OnCheatResetRequested()
        {
            runtimeData.SetValue(resources.CheatChangeLocation.LocationName);
        }

        private void OnLocationUpdated(string locationName)
        {
            labelController.SetLocationLabel(locationName);
        }
    }
}