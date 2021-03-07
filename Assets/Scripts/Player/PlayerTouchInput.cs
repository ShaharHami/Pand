using Player.Interfaces;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Player
{
    public class PlayerTouchInput : IPlayerInput
    {
        private Joystick joystick;
        private Button button;
        private bool clicked, isClicked;
        public PlayerTouchInput()
        {
            joystick = GameObject.FindWithTag(Constants.MOBILE_JOYSTICK_TAG).GetComponent<Joystick>();
            button = GameObject.FindWithTag(Constants.MOBILE_BUTTON_TAG).GetComponent<Button>();
            // This is a workaround so that I can poll the button on Update in the playerController
            // I store the click and give it back when polled
            button.onClick.AddListener(() =>
            {
                clicked = true;
            });
        }
        public float GetAxis()
        {
            return joystick.Horizontal;
        }

        public bool GetButton()
        {
            isClicked = clicked;
            clicked = false;
            return isClicked;
        }
    }
}