using System.Collections.Generic;
using Player;
using UI;
using UnityEngine;

namespace App
{
    // Easy access to non persistent, current scene data
    public class LocalState : MonoBehaviour
    {
        public static LocalState Instance;
        [HideInInspector] public List<PlayerController> playerControllers = new List<PlayerController>();
        private UIController uiController;
        public UIController UiController
        {
            get
            {
                if (uiController == null)
                {
                    uiController = FindObjectOfType<UIController>();
                }

                return uiController;
            }
        }

        public bool gameOver { get; set; }
        public int level { get; set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(Instance);
            }

            level = 0;
        }
    }
}