using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Kauda
{
    public class AudioChangeText : MonoBehaviour
    {
        [SerializeField] private string onText = "On";
        [SerializeField] private string offText = "Off";
        [SerializeField] private TextMeshProUGUI displayText; 

        public void SetOnText()
        {
            displayText.text = onText;
        }

        public void SetOffText()
        {
            displayText.text = offText;
        }
    }
}
