using System;
using Cysharp.Threading.Tasks;
using Shop.Interfaces;
using Shop.Views;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Shop
{
    /// <summary>
    /// Loads and reuses special scene for Product Card display.
    /// Does not unload the scene so it can be reused later.
    /// </summary>
    public class ProductCardSceneController
    {
        public event Action<IShopProduct> productPurchaseRequested;
        
        private readonly int sceneBuildIndex;
        private ShopProductFullScreenView productView;
        private IShopProduct currentProduct;
        private Scene scene;

        public ProductCardSceneController(int sceneBuildIndex)
        {
            this.sceneBuildIndex = sceneBuildIndex;
            this.scene = SceneManager.GetSceneByBuildIndex(sceneBuildIndex);
        }

        public void OpenForProduct(IShopProduct product)
        {
            LoadSceneAsync().ContinueWith(OnSceneReady).Forget(OnException);
                
            void OnSceneReady()
            {
                productView.Setup(product.ProductDataProvider);
                productView.SetState(product.CanPurchase()
                    ? ProductViewStateEnum.Available
                    : ProductViewStateEnum.Unavailable);
                currentProduct = product;
                SceneManager.SetActiveScene(scene);
            }
        }
        
        public void UpdateCurrentProductViewState()
        {
            if (currentProduct == null || productView == null)
            {
                return;
            }
            
            productView.SetState(currentProduct.CanPurchase()
                ? ProductViewStateEnum.Available
                : ProductViewStateEnum.Unavailable);
        }

        private async UniTask LoadSceneAsync()
        {
            if (!scene.isLoaded)
            {
                var loadOp = SceneManager.LoadSceneAsync(sceneBuildIndex, LoadSceneMode.Additive);
                await loadOp.ToUniTask();
                scene = SceneManager.GetSceneByBuildIndex(sceneBuildIndex); // Refresh scene reference
            }
            
            InitializeProductView();
        }

        private void InitializeProductView()
        {
            if (productView != null)
            {
                return;
            }
            
            foreach (var rootGameObject in scene.GetRootGameObjects())
            {
                productView = rootGameObject.GetComponentInChildren<ShopProductFullScreenView>();
                if (productView != null)
                {
                    productView.closeButtonPressed += CloseScene;
                    productView.purchaseButtonClicked += OnPurchaseButtonClicked;
                    break;
                }
            }
        }
        
        private void OnPurchaseButtonClicked(ShopProductViewBase productView)
        {
            productView.SetState(ProductViewStateEnum.Processing);
            productPurchaseRequested?.Invoke(currentProduct);
        }

        private void CloseScene()
        {
            SceneManager.UnloadSceneAsync(scene);
            productView.closeButtonPressed -= CloseScene;
            productView.purchaseButtonClicked -= OnPurchaseButtonClicked;
            productView = null;
        }

        private void OnException(Exception e)
        {
            Debug.LogError($"Failed to open product card scene: {e.Message}");
        }
    }
}