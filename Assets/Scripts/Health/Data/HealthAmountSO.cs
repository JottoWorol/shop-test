using Core;
using Core.Interfaces;
using Core.PlayerResources;
using UnityEngine;

namespace Health.Data
{
    [CreateAssetMenu(fileName = "HealthAmountData", menuName = "Custom/HealthSystem/Health Amount Data")]
    public class HealthAmountSO: ResourceAmountSOBase
    {
        [field: Min(1)][field: SerializeField] public int HealthAmount { get; private set; } = 10;
        public override IResourceAmountData ResourceAmount => new HealthAmountData(HealthAmount);
    }
}