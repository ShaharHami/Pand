using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "GameData", menuName = "Create Data/Create Game Data", order = 0)]
    public class GameData : ScriptableObject
    {
        public List<LevelData> levels;
        public int levelLoadDelay;
        public List<PlayerData> players;
        public List<PowerUpData> powerUps;
        [Space]
        public TextAndUIData textAndUi;
        [Space]
        public float screenPadding;
    }
}