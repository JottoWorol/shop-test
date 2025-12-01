using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Location.Views
{
    public class LocationLabelView: MonoBehaviour
    {
        [field: SerializeField] public TMP_Text LocationLabelText { get; private set; }
        [field: SerializeField] public Button ResetButton { get; private set; }
        
        public void SetLabel(string text)
        {
            LocationLabelText.text = text;
        }
    }
}