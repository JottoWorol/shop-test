using UnityEngine;

namespace Gold.Data
{
    [CreateAssetMenu(fileName = "GoldSystemResources", menuName = "Custom/GoldSystem/Resources", order = 0)]
    public class GoldSystemResources : ScriptableObject
    {
        [field: SerializeField] public int StartingGoldAmount { get; private set; } = 100;
        [field: SerializeField] public int CheatAddAmount { get; private set; } = 10;
    }
}