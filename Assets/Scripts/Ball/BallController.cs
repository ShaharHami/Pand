using System;
using App;
using UnityEngine;

namespace Ball
{
    [Serializable]
    public class BallController : AppBase
    {
        public void OnBallColiision(Rigidbody2D ballRb, Collider2D other)
        {
            Debug.Log("Ball Controller");
            // // Cache ball velocity
            // Vector2 vel = ballRb.velocity;
            // // Compare tag to know what we hit
            // switch (other.tag)
            // {
            //     case Constants.FLOOR_TAG:
            //         // Jump
            //         vel.y = jumpForce;
            //         break;
            //     case Constants.CEILING_TAG:
            //         break;
            //     case Constants.LEFT_WALL_TAG:
            //         // Go left
            //         vel.x = dataContainer.horizontalSpeed;
            //         break;
            //     case Constants.RIGHT_WALL_TAG:
            //         // Go right
            //         vel.x = -dataContainer.horizontalSpeed;
            //         break;
            //     case Constants.PROJECTILE_TAG:
            //         // If we hit a projectile from the player we raise an event and handle splitting the ball
            //         HitBallEvent?.Invoke(transform.position);
            //         audioController.PlayAudio(Constants.POP);
            //         SplitBall();
            //         return;
            // }
            // // Set ball velocity
            // ballRb.velocity = vel;
            // // Set ball rotation
            // ballRb.angularVelocity = Random.Range(dataContainer.torqueRange.x, dataContainer.torqueRange.y) * -vel.x;
        }
    }
}