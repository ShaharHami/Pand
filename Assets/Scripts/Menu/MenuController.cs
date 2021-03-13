using System;
using App;
using Audio;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

namespace Menu
{
    public class MenuController : AppMonoBehaviour
    {
        [SerializeField] private int gameScene;
        [SerializeField] private GameObject desktopUI, mobileUI;

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
            App.localState.level = 0;
            if (App.globalState.gameStarted)
            {
                App.localState.UiController.Transition(false, 0.5f);
            }
            App.globalState.gameStarted = true;
            App.audioController.PlayAudio(Constants.BGM);
        }

        private void LoadGame()
        {
            App.localState.UiController.Transition(true, 0.5f, () => SceneManager.LoadScene(gameScene));
        }

        public void ChangePlayers(int players)
        {
            App.audioController.PlayAudio(Constants.BUTTON_SOUND);
            App.globalState.players = players;
            LoadGame();
        }

        public void QuitGame()
        {
            App.audioController.PlayAudio(Constants.BUTTON_SOUND);
            Application.Quit();
        }
        
    }
}
