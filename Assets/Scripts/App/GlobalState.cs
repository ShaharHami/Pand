using Data;
using UnityEngine;

namespace App
{
    // Meant for storing session persistent data 
    public class GlobalState : MonoBehaviour
    {
        private static GlobalState Instance;
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
                Destroy(gameObject);
            }

            players = 1;
            gameStarted = false;
        }
    }
}