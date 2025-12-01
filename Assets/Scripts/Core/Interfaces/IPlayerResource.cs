using System;

namespace Core.Interfaces
{
    /// <summary>
    /// Represents a player resource that can be managed
    /// </summary>
    public interface IPlayerResource
    {
        /// <summary>
        /// Unique identifier for the resource
        /// </summary>
        string Id { get; }
        event Action<IResourceAmountData, IPlayerResource> updated;
        
        /// <summary>
        /// Checks if the player can afford the specified amount of the resource
        /// </summary>
        /// <param name="amountData"></param>
        bool CanAfford(IResourceAmountData amountData);
        
        /// <summary>
        /// Deducts the specified amount of the resource from the player
        /// </summary>
        /// <param name="amountData"></param>
        void Deduct(IResourceAmountData amountData);
        
        /// <summary>
        /// Adds the specified amount of the resource to the player
        /// </summary>
        /// <param name="amountData"></param>
        void Add(IResourceAmountData amountData);
    }
}