using System;
using Data;
using UnityEngine;

namespace Ball
{
    [Serializable]
    public class BallDataContainer
    {
        public GameObject ballPrefab;
        public int level;
        public float horizontalSpeed;
        public float baseJumpForce;
        public float jumpForceMultiplier;
        public float sizeMultiplier;
        public Vector2 torqueRange;
        public bool goingRight;
        public BallDataContainer(BallData data)
        {
            ballPrefab = data.ballPrefab;
            level = data.level;
            horizontalSpeed = data.horizontalSpeed;
            baseJumpForce = data.baseJumpForce;
            jumpForceMultiplier = data.jumpForceMultiplier;
            sizeMultiplier = data.sizeMultiplier;
            torqueRange = data.torqueRange;
            goingRight = data.goingRight;
        }
    }
}