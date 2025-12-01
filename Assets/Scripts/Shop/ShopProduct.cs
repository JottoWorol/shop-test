using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using Core.Interfaces;
using Shop.Interfaces;

namespace Shop
{
    public class ShopProduct : IShopProduct
    {
        public event Action priceResourceUpdated;
        
        private readonly Dictionary<IPlayerResource, IResourceAmountData> resourceToPriceAmountMap;
        private readonly Dictionary<IPlayerResource, IResourceAmountData> resourceToRewardDataMap;
        
        public IProductDataProvider ProductDataProvider { get; }

        public ShopProduct(IProductDataProvider productDataProvider)
        {
            ProductDataProvider = productDataProvider ?? throw new ArgumentNullException(nameof(productDataProvider));
            
            resourceToPriceAmountMap = new Dictionary<IPlayerResource, IResourceAmountData>();
            resourceToRewardDataMap = new Dictionary<IPlayerResource, IResourceAmountData>();
            
            FillResourceDictionary(resourceToPriceAmountMap, productDataProvider.GetPriceEntries());
            FillResourceDictionary(resourceToRewardDataMap, productDataProvider.GetRewardEntries());
            
            foreach (var playerResource in resourceToPriceAmountMap.Keys)
            {
                playerResource.updated += OnPriceResourceUpdated;
            }
        }
        
        public bool CanPurchase()
        {
            return resourceToPriceAmountMap.All(priceElement =>
                priceElement.Key.CanAfford(priceElement.Value));
        }

        public bool TryPurchase()
        {
            if (!CanPurchase())
            {
                return false;
            }

            foreach (var priceElement in resourceToPriceAmountMap)
            {
                priceElement.Key.Deduct(priceElement.Value);
            }

            return true;
        }
        
        public void GrantRewards()
        {
            foreach (var rewardElement in resourceToRewardDataMap)
            {
                rewardElement.Key.Add(rewardElement.Value);
            }
        }

        private void FillResourceDictionary(Dictionary<IPlayerResource, IResourceAmountData> targetDictionary,
            IResourceAmountData[] source)
        {
            foreach (var resourceAmountData in source)
            {
                if (!PlayerData.Instance.TryGetResourceDataById(resourceAmountData.ResourceId, out var resource))
                {
                    throw new Exception($"Resource with id {resourceAmountData.ResourceId} not found in PlayerData");
                }

                if (!targetDictionary.TryAdd(resource, resourceAmountData))
                {
                    var existingAmountData = targetDictionary[resource];
                    var summedAmountData = existingAmountData.SumWith(resourceAmountData);
                    targetDictionary[resource] = summedAmountData;
                }

            }
        }

        private void OnPriceResourceUpdated(IResourceAmountData amountData, IPlayerResource resource)
        {
            priceResourceUpdated?.Invoke();
        }
    }
}