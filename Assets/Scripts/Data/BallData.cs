using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "Ball Data", menuName = "Create Data/Create Ball Data", order = 0)]
    public class BallData : ScriptableObject
    {
        public GameObject ballPrefab;
        public int level;
        public float horizontalSpeed;
        public float baseJumpForce;
        public float jumpForceMultiplier;
        public float sizeMultiplier;
        public Vector2 torqueRange;
        public bool goingRight;
    }
}