using System;
using UnityEngine;
using UnityEngine.UI;

namespace Shop.Views
{
    public class ShopProductFullScreenView: ShopProductViewBase
    {
        [SerializeField] private Button closeButton;
        
        public event Action closeButtonPressed;
        
        protected override void OnEnable()
        {
            base.OnEnable();
            closeButton.onClick.AddListener(OnCloseButtonPressed);
        }
        
        protected override void OnDisable()
        {
            base.OnDisable();
            closeButton.onClick.RemoveListener(OnCloseButtonPressed);
        }
        
        private void OnCloseButtonPressed()
        {
            closeButtonPressed?.Invoke();
        }
    }
}