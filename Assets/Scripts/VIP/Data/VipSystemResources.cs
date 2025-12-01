using UnityEngine;

namespace VIP.Data
{
    [CreateAssetMenu(fileName = "VipSystemResources", menuName = "Custom/VipSystem/Resources", order = 0)]
    public class VipSystemResources: ScriptableObject
    {
        [field: SerializeField] public int StartingVipTimeSeconds { get; private set; } = 30;
        [field: SerializeField] public int CheatAddTimeSeconds { get; private set; } = 10;
    }
}