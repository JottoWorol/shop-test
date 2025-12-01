using System;
using Gold.Views;

namespace Gold
{
    public class GoldCounterController
    {
        public event Action cheatGoldRequested;
        
        private readonly GoldCounterView counterView;

        public GoldCounterController(GoldCounterView counterView)
        {
            this.counterView = counterView;
            
            counterView.addButton.onClick.AddListener(OnCheatGoldRequested);
        }
        
        public void SetCounter(int amount)
        {
            counterView.SetCounter(amount);
        }

        private void OnCheatGoldRequested()
        {
            cheatGoldRequested?.Invoke();
        }
    }
}