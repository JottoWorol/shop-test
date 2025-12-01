using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Health.Views
{
    public class HealthCounterView : MonoBehaviour
    {
        [field: SerializeField] public TMP_Text CounterText { get; private set; }
        [field: SerializeField] public Button AddButton { get; private set; }

        public void SetCounter(int amount)
        {
            CounterText.text = amount.ToString();
        }
    }
}

