using System;
using Core;
using Cysharp.Threading.Tasks;
using Shop.Data;
using Shop.Interfaces;
using Shop.Views;
using UnityEngine;

namespace Shop
{
    public class ShopSystem: GameSystemBase
    {
        [SerializeField] private Transform shopViewParent;
        [SerializeField] private ShopResources shopResources;
        
        private IPurchaseProcessor processor;
        private IShopProduct[] shopProducts;
        private ShopViewController shopViewController;
        private ProductCardSceneController productCardSceneController;
        private bool isInitialized;

        public override async UniTask InitializeSystem()
        {
            try
            {
                if (shopResources == null)
                {
                    throw new InvalidOperationException("ShopResources is not assigned in ShopSystem");
                }
                
                if (shopViewParent == null)
                {
                    throw new InvalidOperationException("ShopViewParent is not assigned in ShopSystem");
                }
                
                shopViewController = new ShopViewController(shopResources);
                await shopViewController.InitializeView(shopViewParent);
                
                shopProducts = CreateProducts();
                await shopViewController.AddProducts(shopProducts);
                shopViewController.UpdateProductsViewState();
                shopViewController.productPurchaseRequested += OnProductPurchaseRequested;
                shopViewController.productInfoRequested += OnProductInfoRequested;
                
                processor = new DummyPurchaseProcessor();
                processor.purchaseFinished += PurchaseFinished;
                
                productCardSceneController = new ProductCardSceneController(shopResources.ShopCardSceneBuildIndex);
                productCardSceneController.productPurchaseRequested += OnProductPurchaseRequested;
                
                isInitialized = true;
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to initialize shop system: {ex.Message}");
            }
        }

        private IShopProduct[] CreateProducts()
        {
            try
            {
                var result = new IShopProduct[shopResources.BuiltInProducts.Length];
                for (var i = 0; i < shopResources.BuiltInProducts.Length; i++)
                {
                    var shopProduct = new ShopProduct(shopResources.BuiltInProducts[i]);
                    result[i] = shopProduct;
                }

                return result;
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to create shop products: {e.Message}");
                throw;
            }
        }

        private void OnProductPurchaseRequested(IShopProduct product)
        {
            if (!isInitialized)
            {
                Debug.LogWarning("ShopSystem is not initialized yet. Cannot process purchase requests.");
                return;
            }
            
            if (!processor.CanPurchaseProduct(product))
            {
                shopViewController.UpdateProductsViewState();
                return;
            }
            
            processor.PurchaseProduct(product);
        }

        private void PurchaseFinished(PurchaseResult result)
        {
            if (result.Success)
            {
                result.Product.GrantRewards();
                Debug.Log($"Purchase successful: {result.Product.ProductDataProvider.GetName()}");
            }
            else
            {
                Debug.Log($"Purchase failed: {result.Product.ProductDataProvider.GetName()}");
            }
            
            shopViewController.UpdateProductsViewState();
            productCardSceneController.UpdateCurrentProductViewState();
        }

        private void OnProductInfoRequested(IShopProduct shopProduct)
        {
            if (!isInitialized)
            {
                Debug.LogWarning("ShopSystem is not initialized yet. Cannot open product info.");
                return;
            }
            
            productCardSceneController.OpenForProduct(shopProduct);
        }
    }
}