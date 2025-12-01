namespace Core.Interfaces
{
    public interface IResourceAmountData
    {
        /// <summary>
        /// The unique identifier of the resource type this amount data represents.
        /// </summary>
        string ResourceId { get; }
        
        /// <summary>
        /// Sums this resource amount data with another of the same type and returns a new instance representing the total.
        /// </summary>
        /// <param name="other"></param>
        IResourceAmountData SumWith(IResourceAmountData other);
    }
}