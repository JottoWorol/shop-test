using System;
using Core.Interfaces;
using UnityEngine;

namespace Location.Data
{
    public class LocationValueData: IResourceAmountData
    {
        private const string DEFAULT_LOCATION_NAME = "Unknown";
        public string ResourceId => LocationPlayerResource.RESOURCE_ID;
        public string LocationName { get; }

        public LocationValueData(string locationName)
        {
            if (string.IsNullOrEmpty(locationName))
            {
                Debug.LogError("Location name cannot be null or empty");
                locationName = DEFAULT_LOCATION_NAME;
            }
            
            LocationName = locationName;
        }
        
        /// <summary>
        /// For location entity there is no intuitive way to sum two locations, so we just return the other location.
        /// </summary>
        /// <param name="other"></param>
        public IResourceAmountData SumWith(IResourceAmountData other)
        {
            if (other is not LocationValueData otherLocationValueData)
            {
                throw new ArgumentException($"Cannot sum {nameof(LocationValueData)} with {other.GetType().Name}");
            }

            return otherLocationValueData;
        }
    }
}