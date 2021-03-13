using Ball;
using Data;
using Player;
using PowerUp;
using UnityEngine;
using UnityEngine.UI;
using Utils;
using Random = UnityEngine.Random;

namespace Game
{
    public class LevelInitializer : GlobalAccess
    {
        private PlayerController playerController;
        private Image _backgroundImage;
        private Vector2 screenPadding;
        private GameData gameData;
        private Camera cam;
        private Vector2 spawnArea, randomPos;
        private GameObject ballGo;
        private Ball.Ball ball;
        private PowerUpsSpawner powerUpsSpawner;

        public LevelInitializer(Image backgroundImage)
        {
            cam = Camera.main;
            gameData = globalState.gameData;
            _backgroundImage = backgroundImage;
            screenPadding = new Vector2(gameData.screenPadding, gameData.screenPadding);
        }

        public void InitializePlayers()
        {
            // Check if we are on mobile and if so only initialize the first player 
            if (SystemInfo.deviceType == DeviceType.Handheld)
            {
                InitializePlayer(gameData.players[0]);
            }
            else // else initialize as many players as are specified in the global state - if the exist in the data
            {
                for (int i = 0; i < globalState.players; i++)
                {
                    if (gameData.players[i] != null)
                    {
                        InitializePlayer(gameData.players[i]);
                    }
                }
            }
        }

        private void InitializePlayer(PlayerData player)
        {
            playerController = GameObject
                .Instantiate(player.playerPrefab, player.playerSpawnPosition, Quaternion.identity)
                .GetComponent<PlayerController>();
            // Initialize the player controller 
            playerController.Init(player, cam);
            localState.playerControllers.Add(playerController);
        }

        public void InitializeLevelGraphic()
        {
            _backgroundImage.sprite = gameData.levels[localState.level].background;
        }
        
        public void InitializeBalls()
        {
            Ball.Ball.ballsOnScreen = 0;
            // Get a random spawn point withing the screen bounds
            // Use padding to steer clear of the edges
            spawnArea = Utils.Utils.ScreenBounds(cam) - screenPadding;
            foreach (var ballData in gameData.levels[localState.level].balls)
            {
                randomPos = new Vector2(
                    Random.Range(-spawnArea.x, spawnArea.x),
                    Random.Range(-spawnArea.y, spawnArea.y)
                );
                // Spawn ball and get it's controller component
                ballGo = objectPooler.SpawnFromPool(ballData.ballPrefab.name);
                ballGo.transform.position = randomPos;
                ball = ballGo.GetComponent<Ball.Ball>();
                // Initialize controller
                // Data needs to be wrapped in a BallDataContainer because it will be cloned in a later stage
                ball.Init(new BallDataContainer(ballData));
            }
        }

        public void InitializePowerUps()
        {
            powerUpsSpawner = new PowerUpsSpawner(gameData.powerUps);
        }
    }
}