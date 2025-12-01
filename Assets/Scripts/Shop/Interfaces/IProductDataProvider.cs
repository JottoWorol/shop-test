using Core.Interfaces;

namespace Shop.Interfaces
{
    public interface IProductDataProvider
    {
        string GetName();
        IResourceAmountData[] GetPriceEntries();
        IResourceAmountData[] GetRewardEntries();
    }
}