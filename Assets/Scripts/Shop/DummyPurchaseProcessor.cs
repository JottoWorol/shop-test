using System;
using Cysharp.Threading.Tasks;
using Shop.Data;
using Shop.Interfaces;
using UnityEngine;

namespace Shop
{
    public class DummyPurchaseProcessor: IPurchaseProcessor
    {
        private const int SIMULATED_PROCESSING_DELAY_MS = 3000;
        
        public event Action<PurchaseResult> purchaseFinished;

        private bool isBusy;

        public bool CanPurchaseProduct(IShopProduct shopProduct)
        {
            return !isBusy && shopProduct.CanPurchase();
        }
        
        public void PurchaseProduct(IShopProduct shopProduct)
        {
            ProcessPurchase(shopProduct).Forget(OnException);
        }

        private async UniTask ProcessPurchase(IShopProduct shopProduct)
        {
            if (isBusy || shopProduct == null)
            {
                purchaseFinished?.Invoke(new PurchaseResult(false, shopProduct));
                return;
            }
            isBusy = true;
            await UniTask.Delay(SIMULATED_PROCESSING_DELAY_MS);
            var success = shopProduct.TryPurchase();
            isBusy = false;
            purchaseFinished?.Invoke(new PurchaseResult(success, shopProduct));
        }

        private void OnException(Exception e)
        {
            Debug.LogError($"PurchaseProcessor failed to process purchase: {e.Message}");
        }
    }
}