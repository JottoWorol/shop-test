using Core;
using Core.Interfaces;
using Core.PlayerResources;
using Shop.Interfaces;
using UnityEngine;

namespace Shop.Data
{
    [CreateAssetMenu(fileName = "ShopProductData", menuName = "Custom/ShopSystem/ShopProductData", order = 0)]
    public class ShopProductSO : ScriptableObject, IProductDataProvider
    {
        [SerializeField] private string productName;
        [SerializeField] private ResourceAmountSOBase[] priceEntries;
        [SerializeField] private ResourceAmountSOBase[] rewardEntries;
        
        public string GetName() => productName;
        
        public IResourceAmountData[] GetPriceEntries() => 
            System.Array.ConvertAll(priceEntries, entry => entry.ResourceAmount);
        
        public IResourceAmountData[] GetRewardEntries() => 
            System.Array.ConvertAll(rewardEntries, entry => entry.ResourceAmount);
    }
}