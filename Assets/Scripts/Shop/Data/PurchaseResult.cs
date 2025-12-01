using Shop.Interfaces;

namespace Shop.Data
{
    public class PurchaseResult
    {
        public bool Success { get; private set; }
        public IShopProduct Product { get; private set; }
        
        public PurchaseResult(bool success, IShopProduct product)
        {
            Success = success;
            Product = product;
        }
    }
}