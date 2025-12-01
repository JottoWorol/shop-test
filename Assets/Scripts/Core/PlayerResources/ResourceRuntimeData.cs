using System;

namespace Core.PlayerResources
{
    /// <summary>
    /// Convenience class for holding runtime data of a resource
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResourceRuntimeData<T>
    {
        public T CurrentValue { get; private set; }
        public event Action<T> updated;
        
        public void SetValue(T newValue)
        {
            CurrentValue = newValue;
            updated?.Invoke(newValue);
        }
    }
}