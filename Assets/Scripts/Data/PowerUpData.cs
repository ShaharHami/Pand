using PowerUp;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "PowerUpData", menuName = "Create Data/Create Power Up Data", order = 0)]
    public class PowerUpData : ScriptableObject
    {
        public GameObject prefab;
        [Range(0f, 1f)] public float probability;
        public PowerUpType type;
        [Tooltip("in milliseconds")] public int duration;
    }
}