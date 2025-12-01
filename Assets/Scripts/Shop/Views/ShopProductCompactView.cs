using System;
using UnityEngine;
using UnityEngine.UI;

namespace Shop.Views
{
    public class ShopProductCompactView: ShopProductViewBase
    {
        [SerializeField] private Button infoButton;

        public event Action<ShopProductCompactView> infoButtonClicked;

        public override void SetState(ProductViewStateEnum state)
        {
            base.SetState(state);
            infoButton.interactable = state == ProductViewStateEnum.Available;
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            infoButton.onClick.AddListener(OnInfoButtonClicked);
        }
        
        protected override void OnDisable()
        {
            base.OnDisable();
            infoButton.onClick.RemoveListener(OnInfoButtonClicked);
        }
        
        private void OnInfoButtonClicked()
        {
            infoButtonClicked?.Invoke(this);
        }
    }
}