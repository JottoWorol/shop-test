using System;
using Core.Interfaces;
using Location.Data;

namespace Location
{
    public class LocationPlayerResource: IPlayerResource
    {
        public const string RESOURCE_ID = "Location";
        
        private readonly LocationRuntimeData runtimeData;
        
        public string Id => RESOURCE_ID;
        public event Action<IResourceAmountData, IPlayerResource> updated;
        
        public LocationPlayerResource(LocationRuntimeData runtimeData)
        {
            this.runtimeData = runtimeData;
            runtimeData.updated += OnRuntimeDataUpdated;
        }
        
        /// <summary>
        /// Location as a price is always affordable. Price - change to specific location.
        /// </summary>
        /// <param name="amountData"></param>
        public bool CanAfford(IResourceAmountData amountData)
        {
            if (amountData is not LocationValueData locationValueData)
            {
                throw new ArgumentException($"Cannot compare {nameof(LocationPlayerResource)} with {amountData.GetType().Name}");
            }

            return true;
        }

        /// <summary>
        /// Deduction will work as a location change.
        /// </summary>
        /// <param name="amountData"></param>
        public void Deduct(IResourceAmountData amountData)
        {
            if (amountData is not LocationValueData locationValueData)
            {
                throw new ArgumentException($"Cannot deduct {nameof(LocationPlayerResource)} with {amountData.GetType().Name}");
            }
            
            runtimeData.SetValue(locationValueData.LocationName);
        }

        /// <summary>
        /// Addition will work as a location change.
        /// </summary>
        /// <param name="amountData"></param>
        public void Add(IResourceAmountData amountData)
        {
            if (amountData is not LocationValueData locationValueData)
            {
                throw new ArgumentException($"Cannot add {nameof(LocationPlayerResource)} with {amountData.GetType().Name}");
            }
            
            runtimeData.SetValue(locationValueData.LocationName);
        }
        
        private void OnRuntimeDataUpdated(string newLocation)
        {
            var amountData = new LocationValueData(newLocation);
            updated?.Invoke(amountData, this);
        }
    }
}