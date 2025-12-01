using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Shop.Data;
using Shop.Interfaces;
using Shop.Views;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Shop
{
    public class ShopViewController
    {
        private readonly ShopResources shopResources;
        private readonly Dictionary<ShopProductViewBase, IShopProduct> productViewToDataCache = new();
        
        public event Action<IShopProduct> productPurchaseRequested;
        public event Action<IShopProduct> productInfoRequested;
        private ShopView shopView;

        public ShopViewController(ShopResources shopResources)
        {
            this.shopResources = shopResources;
        }
        
        public async UniTask InitializeView(Transform parentTransform)
        {
            await CreateShopView(parentTransform);
        }
        
        public async UniTask AddProducts(IShopProduct[] products)
        {
            try
            {
                foreach (var product in products)
                {
                    var productData = product.ProductDataProvider;
                    var productView = await CreateProductCompactView(productData);
                    productView.purchaseButtonClicked += OnProductViewPurchaseButtonClicked;
                    productView.infoButtonClicked += OnProductInfoButtonClicked;

                    productViewToDataCache[productView] = product;
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to add products to shop view: {e.Message}");
            }
        }

        public void UpdateProductsViewState()
        {
            foreach (var (view, product) in productViewToDataCache)
            {
                view.SetState(product.CanPurchase()
                    ? ProductViewStateEnum.Available
                    : ProductViewStateEnum.Unavailable);
            }
        }

        private async UniTask CreateShopView(Transform parentTransform)
        {
            var shopViewPrefab = shopResources.ShopViewPrefab;
            var shopViewResult = await Object.InstantiateAsync(shopViewPrefab, parentTransform).ToUniTask();

            if (shopViewResult == null || shopViewResult.Length == 0 || shopViewResult[0] == null)
            {
                throw new InvalidOperationException("Shop View Prefab instantiation failed: result is null or empty");
            }

            shopView = shopViewResult[0];
            shopView.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
        }

        private async UniTask<ShopProductCompactView> CreateProductCompactView(IProductDataProvider productData)
        {
            if (shopView == null)
            {
                throw new InvalidOperationException("Cannot initialize products: Shop View is not initialized");
            }

            var productViewResult =
                await Object.InstantiateAsync(
                    shopResources.ShopProductViewPrefab, 
                    shopView.ShopProductsContainer, 
                    Vector3.zero, Quaternion.identity).ToUniTask();
                        
            if (productViewResult == null || productViewResult.Length == 0 || productViewResult[0] == null)
            {
                throw new InvalidOperationException("Product View Prefab instantiation failed: result is null or empty");
            }

            var productView = productViewResult[0];

            productView.Setup(productData);
            return productView;
        }

        private void OnProductViewPurchaseButtonClicked(ShopProductViewBase view)
        {
            view.SetState(ProductViewStateEnum.Processing);
            if (productViewToDataCache.TryGetValue(view, out var productData))
            {
                productPurchaseRequested?.Invoke(productData);
            }
            else
            {
                Debug.LogError("Product view not found in cache on purchase button click");
            }
        }

        private void OnProductInfoButtonClicked(ShopProductCompactView view)
        {
            if (productViewToDataCache.TryGetValue(view, out var productData))
            {
                productInfoRequested?.Invoke(productData);
            }
            else
            {
                Debug.LogError("Product view not found in cache on purchase button click");
            }
        }
    }
}