using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "Create Data/Create Level Data", order = 0)]
    public class LevelData : ScriptableObject
    {
        public string levelName;
        public List<BallData> balls;
        public Sprite background;
    }
}