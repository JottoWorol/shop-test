using Core;
using Core.Interfaces;
using Core.PlayerResources;
using UnityEngine;

namespace Gold.Data
{
    [CreateAssetMenu(fileName = "GoldAmountData", menuName = "Custom/GoldSystem/Gold Amount Data")]
    public class GoldAmountSO: ResourceAmountSOBase
    {
        [field: Min(1)][field: SerializeField] public int Amount { get; private set; } = 10;
        public override IResourceAmountData ResourceAmount => new GoldAmountData(Amount);
    }
}