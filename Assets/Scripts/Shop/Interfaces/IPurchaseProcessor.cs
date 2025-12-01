using System;
using Shop.Data;

namespace Shop.Interfaces
{
    public interface IPurchaseProcessor
    {
        event Action<PurchaseResult> purchaseFinished;
        bool CanPurchaseProduct(IShopProduct shopProduct);
        void PurchaseProduct(IShopProduct shopProduct);
    }
}