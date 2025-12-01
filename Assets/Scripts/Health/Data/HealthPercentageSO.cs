using Core;
using Core.Interfaces;
using Core.PlayerResources;
using UnityEngine;

namespace Health.Data
{
    [CreateAssetMenu(fileName = "HealthPercentageData", menuName = "Custom/HealthSystem/Health Percentage Data")]
    public class HealthPercentageSO: ResourceAmountSOBase
    {
        [field: Min(10)][field: SerializeField] public int HealthPercentage { get; private set; } = 10;
        public override IResourceAmountData ResourceAmount => new HealthPercentageData(HealthPercentage);
    }
}