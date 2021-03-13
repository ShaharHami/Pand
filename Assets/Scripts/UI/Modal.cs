using System;
using Game;
using TMPro;
using UnityEngine;
using Utils;

namespace UI
{
    // Basic popup window
    public class Modal : GlobalAccessMonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI conclusionText, mainText;
        private string win;

        private void Awake()
        {
            InitializeReferences();
        }

        private void Start()
        {
            gameObject.SetActive(false);
            conclusionText.gameObject.SetActive(true);
            mainText.text = globalState.gameData.textAndUi.gameOver;
            win = globalState.gameData.textAndUi.win;
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
