using System;
using Game;
using TMPro;
using UnityEngine;

namespace UI
{
    public class Modal : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI conclusionText, mainText;
        private string win;
        
        private void Start()
        {
            gameObject.SetActive(false);
            conclusionText.gameObject.SetActive(true);
            mainText.text = GlobalState.Instance.gameData.textAndUi.gameOver;
            win = GlobalState.Instance.gameData.textAndUi.win;
        }

        public void HideConclusionText()
        {
            conclusionText.gameObject.SetActive(false);
        }
        
        public void SetConclusionText(string playerName)
        {
            if (playerName != String.Empty)
            {
                conclusionText.text = playerName + " " + win;
            }
        }

        public void ToggleModal(bool on)
        {
            gameObject.SetActive(on);
        }
    }
}
