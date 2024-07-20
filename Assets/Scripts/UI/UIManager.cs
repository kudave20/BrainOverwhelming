using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Brain.UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI successText;
        private int successNum = 0;
        void Start()
        {
            successText.text = "0";
            successNum = 0;
        
        }

        public void updateSuccess()
        {
            successNum++;
            successText.text = successNum.ToString();
        }
    }
}