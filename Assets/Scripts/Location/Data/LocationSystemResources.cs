using UnityEngine;

namespace Location.Data
{
    [CreateAssetMenu(fileName = "LocationSystemResources", menuName = "Custom/LocationSystem/Resources", order = 0)]
    public class LocationSystemResources: ScriptableObject
    {
        [field: SerializeField] public LocationNameSO StartingLocation { get; private set; }
        [field: SerializeField] public LocationNameSO CheatChangeLocation { get; private set; }
    }
}