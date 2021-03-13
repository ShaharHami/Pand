using System;
using Audio;
using Game;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

namespace Menu
{
    public class MenuController : GlobalAccessMonoBehaviour
    {
        [SerializeField] private int gameScene;
        [SerializeField] private GameObject desktopUI, mobileUI;

        private void Awake()
        {
            InitializeReferences();
        }

        private void Start()
        {
            if (SystemInfo.deviceType == DeviceType.Handheld)
            {
                mobileUI.SetActive(true);
                desktopUI.SetActive(false);
            }
            else
            {
                mobileUI.SetActive(false);
                desktopUI.SetActive(true);
            }
            localState.level = 0;
            if (globalState.gameStarted)
            {
                localState.UiController.Transition(false, 0.5f);
            }
            globalState.gameStarted = true;
            audioController.PlayAudio(Constants.BGM);
        }

        private void LoadGame()
        {
            localState.UiController.Transition(true, 0.5f, () => SceneManager.LoadScene(gameScene));
        }

        public void ChangePlayers(int players)
        {
            audioController.PlayAudio(Constants.BUTTON_SOUND);
            globalState.players = players;
            LoadGame();
        }

        public void QuitGame()
        {
            audioController.PlayAudio(Constants.BUTTON_SOUND);
            Application.Quit();
        }
        
    }
}
