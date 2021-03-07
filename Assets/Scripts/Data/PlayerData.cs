using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "Create Data/Create Player Data", order = 0)]
    public class PlayerData : ScriptableObject
    {
        public string playerName;
        [Space]
        [Header("Player Visuals")] 
        public GameObject playerPrefab;
        public Color playerInvincibilityColor;
        
        [Header("Player Initial Stats")] 
        public int playerLives;
        public Vector2 playerSpawnPosition;
        [Tooltip("In milliseconds")] 
        public int playerInvincibilityDuration;
        
        [Header("Player Controls")] 
        public string horizontalAxisName;
        public string fireButtonName;
        
        [Header("Player Motion")]
        public float playerSpeed;
        public float moveThreshold;
        public float maxVelocity;
        
        [Header("Player Weapon")]
        public GameObject projectilePrefab;
        public int maxProjectilesAllowed;
        public Color weaponColor;
        public float projectileSpeed;
        public float delay;
    }
}