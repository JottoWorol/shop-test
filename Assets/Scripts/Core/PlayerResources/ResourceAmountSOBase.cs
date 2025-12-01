using Core.Interfaces;
using UnityEngine;

namespace Core.PlayerResources
{
    /// <summary>
    /// Abstract base class for ScriptableObjects as IResourceAmountProvider
    /// </summary>
    public abstract class ResourceAmountSOBase: ScriptableObject, IResourceAmountProvider
    {
        public abstract IResourceAmountData ResourceAmount { get; }
    }
}