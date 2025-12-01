using Core;
using Core.Interfaces;
using Core.PlayerResources;
using UnityEngine;

namespace Location.Data
{
    [CreateAssetMenu(fileName = "LocationNameData", menuName = "Custom/LocationSystem/Location Name Data")]
    public class LocationNameSO: ResourceAmountSOBase
    {
        [field: SerializeField] public string LocationName { get; private set; }
        public override IResourceAmountData ResourceAmount => new LocationValueData(LocationName);
    }
}