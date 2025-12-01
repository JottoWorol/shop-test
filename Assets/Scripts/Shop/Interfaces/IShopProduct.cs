using System;

namespace Shop.Interfaces
{
    public interface IShopProduct
    {
        event Action priceResourceUpdated;
        IProductDataProvider ProductDataProvider { get; }
        bool CanPurchase();
        bool TryPurchase();
        void GrantRewards();
    }
}