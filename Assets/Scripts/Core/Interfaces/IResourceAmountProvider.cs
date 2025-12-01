namespace Core.Interfaces
{
    /// <summary>
    /// Interface for unifying different data formats that provide resource amount information.
    /// E.g. ScriptableObjects, JSON data, etc.
    /// </summary>
    public interface IResourceAmountProvider
    {
        IResourceAmountData ResourceAmount { get; }
    }
}