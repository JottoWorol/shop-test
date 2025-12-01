using System;
using VIP.Views;

namespace VIP
{
    public class VipTimerController
    {
        private const string VIP_TIME_FORMAT = "{0}sec";
        
        public event Action onAddButtonClicked;
        private readonly VipTimerView vipTimerView;

        public VipTimerController(VipTimerView vipTimerView)
        {
            this.vipTimerView = vipTimerView;
            vipTimerView.AddButton.onClick.AddListener(OnAddButtonClicked);
        }
        
        public void UpdateVipTimerText(TimeSpan timeSpan)
        {
            vipTimerView.SetTimerText(string.Format(VIP_TIME_FORMAT, (int)timeSpan.TotalSeconds));
        }
        
        private void OnAddButtonClicked()
        {
            onAddButtonClicked?.Invoke();
        }
    }
}