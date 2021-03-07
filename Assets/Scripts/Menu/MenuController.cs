using Audio;
using Game;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

namespace Menu
{
    public class MenuController : MonoBehaviour
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
            LocalState.Instance.level = 0;
            if (GlobalState.Instance.gameStarted)
            {
                LocalState.Instance.uiController.Transition(false, 0.5f);
            }
            GlobalState.Instance.gameStarted = true;
            AudioController.Instance.PlayAudio(Constants.BGM);
        }

        private void LoadGame()
        {
            LocalState.Instance.uiController.Transition(true, 0.5f, () => SceneManager.LoadScene(gameScene));
        }

        public void ChangePlayers(int players)
        {
            AudioController.Instance.PlayAudio(Constants.BUTTON_SOUND);
            GlobalState.Instance.players = players;
            LoadGame();
        }

        public void QuitGame()
        {
            AudioController.Instance.PlayAudio(Constants.BUTTON_SOUND);
            Application.Quit();
        }
        
    }
}
