using Player.Interfaces;
using UnityEngine;

namespace Player
{
    public class PlayerKeyboardInput : IPlayerInput
    {
        private readonly string _axisName, _buttonName;
        public PlayerKeyboardInput(string axisName, string buttonName)
        {
            _axisName = axisName;
            _buttonName = buttonName;
        }

        public float GetAxis()
        {
            return Input.GetAxisRaw(_axisName);
        }

        public bool GetButton()
        {
            return Input.GetButtonDown(_buttonName);
        }
    }
}