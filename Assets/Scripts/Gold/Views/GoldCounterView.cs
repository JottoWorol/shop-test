using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gold.Views
{
    public class GoldCounterView : MonoBehaviour
    {
        [field: SerializeField] public TMP_Text counterText { get; private set; }
        [field: SerializeField] public Button addButton { get; private set; }

        public void SetCounter(int amount)
        {
            counterText.text = amount.ToString();
        }
    }
}