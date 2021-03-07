﻿using System;
using Audio;
using UnityEngine;
using Utils;
using Random = UnityEngine.Random;

namespace Ball
{
    public class Ball : MonoBehaviour
    {
        // Keep a static counter to know when there are no more balls on screen e.g. level finished
        public static int ballsOnScreen;
        
        public static Action NoMoreBallsEvent;
        public static Action<Vector2> HitBallEvent;
        private BallDataContainer dataContainer, dataContainerClone;
        private Rigidbody2D ballRb;
        private float jumpForce;
        private float torque;
        private float turn;
        private Vector2 torqueRange;
        private GameObject newBall;
        private Ball newBallComponent;

        private void Awake()
        {
            // Get a reference to the ball rigidbody
            ballRb = GetComponent<Rigidbody2D>();
        }

        private void OnEnable()
        {
            // Update counter
            ballsOnScreen++;
        }

        private void OnDisable()
        {
            // Reduce the ball counter
            ballsOnScreen--;
        }

        public void Init(BallDataContainer ballDataContainer)
        {
            // Cache data container
            dataContainer = ballDataContainer;
            // Set ball direction and horizontal velocity
            var dir = dataContainer.goingRight ? Vector2.right : Vector2.left;
            ballRb.velocity = dir * dataContainer.horizontalSpeed;
            // Set ball size by it's level
            transform.localScale = Vector2.one * (dataContainer.level * dataContainer.sizeMultiplier);
            // Calculate ball jump force (how high the ball will jump) by it's level
            jumpForce = dataContainer.baseJumpForce;
            for (int i = 0; i < dataContainer.level; i++)
            {
                jumpForce *= dataContainer.jumpForceMultiplier;
            }
        }

        // Handle ball collisions
        private void OnTriggerEnter2D(Collider2D other)
        {
            // Cache ball velocity
            Vector2 vel = ballRb.velocity;
            // Compare tag to know what we hit
            switch (other.tag)
            {
                case Constants.FLOOR_TAG:
                    // Jump
                    vel.y = jumpForce;
                    break;
                case Constants.CEILING_TAG:
                    break;
                case Constants.LEFT_WALL_TAG:
                    // Go left
                    vel.x = dataContainer.horizontalSpeed;
                    break;
                case Constants.RIGHT_WALL_TAG:
                    // Go right
                    vel.x = -dataContainer.horizontalSpeed;
                    break;
                case Constants.PROJECTILE_TAG:
                    // If we hit a projectile from the player we raise an event and handle splitting the ball
                    HitBallEvent?.Invoke(transform.position);
                    AudioController.Instance.PlayAudio(Constants.POP);
                    SplitBall();
                    return;
            }
            // Set ball velocity
            ballRb.velocity = vel;
            // Set ball rotation
            ballRb.angularVelocity = Random.Range(dataContainer.torqueRange.x, dataContainer.torqueRange.y) * -vel.x;
        }
        
        private void SplitBall()
        {
            
            // If the ball level is 1 (the smallest), just destroy it
            if (dataContainer.level > 1)
            {
                int level = dataContainer.level - 1;
                // Loop twice because we are splitting a single ball into two balls
                for (int i = 0; i < 2; i++)
                {
                    // Clone the ball data container
                    dataContainerClone = Utils.Utils.Clone(dataContainer);
                    // Set the new ball level in the cloned data
                    dataContainerClone.level = level;
                    // Set new ball direction in the cloned data to achieve the effect where each ball goes off in the opposite direction
                    dataContainerClone.goingRight = i % 2 == 0;
                    // Spawn new ball
                    newBall = ObjectPooler.Instance.SpawnFromPool(dataContainer.ballPrefab); 
                    // Position new ball
                    newBall.transform.position = transform.position;
                    // Get new ball component
                    newBallComponent = newBall.GetComponent<Ball>();
                    // Initialize new ball component with the cloned and modified data
                    newBallComponent.Init(dataContainerClone);
                }
            }
            // Remove the ball
            gameObject.SetActive(false);
            // If no more balls are left invoke an event
            if (ballsOnScreen <= 0)
            {
                NoMoreBallsEvent?.Invoke();
            }
        }
    }
}