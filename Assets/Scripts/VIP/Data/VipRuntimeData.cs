using System;

namespace VIP.Data
{
    public class VipRuntimeData
    {
        public TimeSpan CurrentVipTime { get; private set; }
        public event Action<TimeSpan> updated;
        
        public void SetVipTime(TimeSpan newVipTime)
        {
            CurrentVipTime = newVipTime;
            updated?.Invoke(newVipTime);
        }
    }
}