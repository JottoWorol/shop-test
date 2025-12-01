using System;
using Health.Views;

namespace Health
{
    public class HealthCounterController
    {
        public event Action cheatHealthRequested;
        
        private readonly HealthCounterView counterView;

        public HealthCounterController(HealthCounterView counterView)
        {
            this.counterView = counterView;
            
            counterView.AddButton.onClick.AddListener(OnCheatHealthRequested);
        }
        
        public void SetCounter(int amount)
        {
            counterView.SetCounter(amount);
        }

        private void OnCheatHealthRequested()
        {
            cheatHealthRequested?.Invoke();
        }
    }
}

