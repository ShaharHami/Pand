using Data;
using UnityEngine;

namespace Game
{
    // Meant for storing session persistent data 
    public class GlobalState : MonoBehaviour
    {
        public static GlobalState Instance;
        public GameData gameData;
        public int players { get; set; }
        public bool gameStarted;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(this);
            }

            players = 1;
            gameStarted = false;
        }
    }
}