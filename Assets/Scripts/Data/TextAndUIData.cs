using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "TextUIData", menuName = "Create Data/Create Text And UI Data", order = 0)]
    public class TextAndUIData : ScriptableObject
    {
        public string win;
        public string dead;
        public string gameOver;
    }
}