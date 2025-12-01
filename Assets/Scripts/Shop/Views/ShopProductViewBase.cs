using System;
using Shop.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Shop.Views
{
    public abstract class ShopProductViewBase : MonoBehaviour
    {
        [SerializeField] private TMP_Text productNameText;
        [SerializeField] private TMP_Text purchaseButtonText;
        [SerializeField] private Button purchaseButton;
        private const string BUTTON_TEXT_AVAILABLE = "Purchase";
        private const string BUTTON_TEXT_UNAVAILABLE = "Unavailable";
        private const string BUTTON_TEXT_PROCESSING = "Processing...";
        public event Action<ShopProductViewBase> purchaseButtonClicked;

        public void Setup(IProductDataProvider productData)
        {
            productNameText.text = productData.GetName();
        }

        public virtual void SetState(ProductViewStateEnum state)
        {
            switch (state)
            {
                case ProductViewStateEnum.None:
                    purchaseButton.interactable = false;
                    purchaseButtonText.text = BUTTON_TEXT_UNAVAILABLE;
                    break;
                case ProductViewStateEnum.Available:
                    purchaseButton.interactable = true;
                    purchaseButtonText.text = BUTTON_TEXT_AVAILABLE;
                    break;
                case ProductViewStateEnum.Unavailable:
                    purchaseButton.interactable = false;
                    purchaseButtonText.text = BUTTON_TEXT_UNAVAILABLE;
                    break;
                case ProductViewStateEnum.Processing:
                    purchaseButton.interactable = false;
                    purchaseButtonText.text = BUTTON_TEXT_PROCESSING;
                    break;
            }
        }

        protected virtual void OnEnable()
        {
            purchaseButton.onClick.AddListener(OnPurchaseButtonClicked);
        }

        protected virtual void OnDisable()
        {
            purchaseButton.onClick.RemoveListener(OnPurchaseButtonClicked);
        }

        private void OnPurchaseButtonClicked()
        {
            purchaseButtonClicked?.Invoke(this);
        }
    }
}