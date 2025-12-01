using System;
using Location.Views;

namespace Location
{
    public class LocationLabelController
    {
        public event Action cheatResetRequested;
        
        private readonly LocationLabelView labelView;

        public LocationLabelController(LocationLabelView labelView)
        {
            this.labelView = labelView;

            labelView.ResetButton.onClick.AddListener(OnAddButtonClicked);
        }
        
        public void SetLocationLabel(string text)
        {
            labelView.SetLabel(text);
        }

        private void OnAddButtonClicked()
        {
            cheatResetRequested?.Invoke();
        }
    }
}