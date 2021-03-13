using System;
using Game;
using TMPro;
using UnityEngine;
using Utils;

namespace UI
{
    public class UIController : GlobalAccessMonoBehaviour
    {
        [SerializeField] private GameObject mobileControls;
        [SerializeField] private Transition transition;
        [SerializeField] private Modal modal;
        [SerializeField] private TextMeshProUGUI centralMessageText;

        private void Awake()
        {
            InitializeReferences();
        }

        private void Start()
        {
            if (mobileControls != null)
            {
                mobileControls.SetActive(SystemInfo.deviceType == DeviceType.Handheld);
            }
        }

        public void Transition(bool transitionIn, float duration, Action complete = null)
        {
            if (transitionIn)
            {
                transition.TransitionIn(duration, complete);
            }
            else
            {
                transition.TransitionOut(duration, complete);
            }
        }

        public void ToggleCentralMessage(bool on)
        {
            centralMessageText.gameObject.SetActive(on);
        }
        
        public void SetCentralMessage(string message)
        {
            centralMessageText.text = message;
        }
        
        public void HideConclusionText()
        {
            modal.HideConclusionText();
        }
        
        public void SetConclusionText(string playerName)
        {
            modal.SetConclusionText(playerName);
        }

        public void ToggleModal(bool on)
        {
            mobileControls.gameObject.SetActive(!on);
            modal.gameObject.SetActive(on);
        }
    }
}
