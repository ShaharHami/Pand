using UnityEngine;

namespace Player
{
    public class PlayerMotion
    {
        private readonly float _speed;
        private readonly float _moveThreshold;
        private readonly float _maxVelocity;
        private readonly Vector3 _playerScale;
        private readonly Rigidbody2D _rb;
        private float force, velX;
        private Vector2 calcualtedForce;

        public PlayerMotion(float playerSpeed, float playerMoveThreshold, float playerMaxVelocity, Rigidbody2D playerRb)
        {
            _rb = playerRb;
            _speed = playerSpeed;
            _moveThreshold = playerMoveThreshold;
            _maxVelocity = playerMaxVelocity;
        }

        public void MovePlayerHorizontally(float input)
        {
            force = 0f;
            velX = Mathf.Abs(_rb.velocity.x);

            if (input > _moveThreshold)
            {
                if (velX < _maxVelocity)
                {
                    force = _speed;
                }
            }
            else if (input < -_moveThreshold)
            {
                if (velX < _maxVelocity)
                {
                    force = -_speed;
                }
            }

            calcualtedForce.x = force;
            calcualtedForce.y = Physics.gravity.y;
            _rb.velocity = calcualtedForce;
        }
    }
}
