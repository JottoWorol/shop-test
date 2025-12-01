using UnityEngine;

namespace Health.Data
{
    [CreateAssetMenu(fileName = "HealthSystemResources", menuName = "Custom/HealthSystem/Resources", order = 0)]
    public class HealthSystemResources: ScriptableObject
    {
        [field: SerializeField] public int StartingHealthAmount { get; private set; } = 100;
        [field: SerializeField] public int CheatAddAmount { get; private set; } = 10;

        /// <summary>
        /// Player cannot take percentage damage that would reduce health below this value.
        /// </summary>
        [field: SerializeField] public int MinHealthAfterPercentageDamage { get; private set; } = 10;
    }
}