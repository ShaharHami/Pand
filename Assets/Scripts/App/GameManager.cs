using System;
using System.Collections;
using System.Linq;
using Audio;
using Data;
using Player;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utils;

namespace App
{
    public class GameManager : AppMonoBehaviour
    {
        [SerializeField] private Image backgroundImage;
        private UIController uiController;
        private LevelInitializer _levelInitializer;
        private GameData gameData;
        private int highestScore;
        private string selectedPlayer;

        private void Awake()
        {
            if (SystemInfo.deviceType == DeviceType.Handheld)
            {
                Application.targetFrameRate = 60;
            }
        }

        private void Start()
        {
            gameData = App.globalState.gameData;
            uiController = App.localState.UiController;
            uiController.Transition(false, 0.5f);
            StartGame();
        }

        private void StartGame()
        {
            _levelInitializer = new LevelInitializer(backgroundImage);
            _levelInitializer.InitializePlayers();
            LoadNextLevel();
        }

        private void OnEnable()
        {
            Ball.BallView.OnNoMoreBalls += HandleLevelClear;
            PlayerController.OnPlayerDead += HandlePlayerDead;
        }

        private void OnDisable()
        {
            Ball.BallView.OnNoMoreBalls -= HandleLevelClear;
            PlayerController.OnPlayerDead -= HandlePlayerDead;
        }

        private void HandleLevelClear()
        {
            if (App.localState.level >= gameData.levels.Count-1)
            {
                HandleGameOver(true, SelectPlayerWhoWon());
            }
            else
            {
                App.localState.level++;
                LoadNextLevel(true);
            }
        }

        private void LoadNextLevel(bool transition = false)
        {
            if (transition)
            {
                uiController.ToggleCentralMessage(true);
                uiController.SetCentralMessage(gameData.levels[App.localState.level].levelName);
                uiController.Transition(true, 0.5f, () =>
                {
                    NextLevel();
                    uiController.Transition(false, 0.5f);
                });   
            }
            else
            {
                uiController.ToggleCentralMessage(true);
                uiController.SetCentralMessage(gameData.levels[App.localState.level].levelName);
                NextLevel();
            }
        }

        private void NextLevel()
        {
            StartCoroutine(CountDown(gameData.levelLoadDelay, InitializeLevel));
            _levelInitializer.InitializeLevelGraphic();
        }

        private void InitializeLevel()
        {
            uiController.ToggleCentralMessage(false);
            _levelInitializer.InitializePowerUps();
            _levelInitializer.InitializeBalls();
        }
        
        private string SelectPlayerWhoWon()
        {
            // Determine which player has the higher score
            highestScore = 0;
            selectedPlayer = String.Empty;
            foreach (var player in App.localState.playerControllers)
            {
                if (player.Score > highestScore)
                {
                    highestScore = player.Score;
                    selectedPlayer = player.PlayerName;
                }
            }
            
            return selectedPlayer;
        }
        
        private void HandlePlayerDead(PlayerController player)
        {
            if (App.localState.playerControllers.Count(playerController => !playerController.PlayerDead) <= 0)
            {
                HandleGameOver(false);
            }
        }

        private void HandleGameOver(bool win, string playerWhoWon = "")
        {
            App.localState.gameOver = true;
            if (win)
            {
                uiController.SetConclusionText(playerWhoWon);
            }
            else
            {
                uiController.HideConclusionText();
            }
            uiController.ToggleModal(true);
        }
        
        private IEnumerator CountDown(int count, Action action)
        {
            for (int i = 0; i < count; i++)
            {
                yield return new WaitForSeconds(1);
                uiController.SetCentralMessage((count - i - 1).ToString());
            }
            action.Invoke();
        }

        public void ReturnToMenu()
        {
            App.audioController.PlayAudio(Constants.BUTTON_SOUND);
            App.objectPooler.ResetObjects();
            uiController.Transition(true, 0.5f, () => SceneManager.LoadScene(Constants.MENU_SCENE_NAME));
        }
    }
}
