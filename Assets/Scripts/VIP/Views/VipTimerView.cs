using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace VIP.Views
{
    public class VipTimerView: MonoBehaviour
    {
        [field: SerializeField] public TMP_Text VipTimerText { get; private set; }
        [field: SerializeField] public Button AddButton { get; private set; }
        
        public void SetTimerText(string text)
        {
            VipTimerText.text = text;
        }
    }
}