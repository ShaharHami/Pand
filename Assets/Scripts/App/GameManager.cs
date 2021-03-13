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
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Image backgroundImage;
        private UIController uiController;
        private LevelInitializer _levelInitializer;
        private GameData gameData;
        private GlobalState globalState;
        private LocalState localState;
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
            globalState = GlobalState.Instance;
            localState = LocalState.Instance;
            gameData = globalState.gameData;
            uiController = localState.UiController;
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
            Ball.Ball.OnNoMoreBalls += HandleLevelClear;
            PlayerController.OnPlayerDead += HandlePlayerDead;
        }

        private void OnDisable()
        {
            Ball.Ball.OnNoMoreBalls -= HandleLevelClear;
            PlayerController.OnPlayerDead -= HandlePlayerDead;
        }

        private void HandleLevelClear()
        {
            if (localState.level >= gameData.levels.Count-1)
            {
                HandleGameOver(true, SelectPlayerWhoWon());
            }
            else
            {
                localState.level++;
                LoadNextLevel(true);
            }
        }

        private void LoadNextLevel(bool transition = false)
        {
            if (transition)
            {
                uiController.ToggleCentralMessage(true);
                uiController.SetCentralMessage(gameData.levels[localState.level].levelName);
                uiController.Transition(true, 0.5f, () =>
                {
                    NextLevel();
                    uiController.Transition(false, 0.5f);
                });   
            }
            else
            {
                uiController.ToggleCentralMessage(true);
                uiController.SetCentralMessage(gameData.levels[localState.level].levelName);
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
            foreach (var player in localState.playerControllers)
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
            if (localState.playerControllers.Count(playerController => !playerController.PlayerDead) <= 0)
            {
                HandleGameOver(false);
            }
        }

        private void HandleGameOver(bool win, string playerWhoWon = "")
        {
            localState.gameOver = true;
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
            AudioController.Instance.PlayAudio(Constants.BUTTON_SOUND);
            ObjectPooler.Instance.ResetObjects();
            uiController.Transition(true, 0.5f, () => SceneManager.LoadScene(Constants.MENU_SCENE_NAME));
        }
    }
}
