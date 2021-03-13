using System;
using System.Collections;
using App;
using Audio;
using Data;
using Player.Interfaces;
using TMPro;
using UnityEngine;
using Utils;

namespace Player
{
    public class PlayerController : AppMonoBehaviour
    {
        [SerializeField] private SpriteRenderer renderer;
        [SerializeField] private Canvas playerCanvas;
        [SerializeField] private TextMeshProUGUI livesText, scoreText;
        [SerializeField] private GameObject permaShotIndicator, doubleShotIndicator;
        [SerializeField] private Transform firingPoint;
        public static Action<PlayerController> OnPlayerDead;
        private PlayerMotion playerMotion;
        private PlayerWeapon playerWeapon;
        private IPlayerInput playerInput;
        private PlayerHealth playerHealth;
        private PlayerGraphic playerGraphic;
        private PlayerScore playerScore;
        private PlayerUI playerUi;
        private PlayerPowerUps _playerPowerUps;
        private PowerUp.PowerUp powerUp;
        private Rigidbody2D playerRb;
        private int playerInvincibilityDuration;
        private bool invincible;
        private bool coolDown;
        public string PlayerName { get; private set; }
        public bool PlayerDead { get; private set; }
        public int Score => playerScore.Score;

        // Initialize player components
        public void Init(PlayerData playerData, Camera cam)
        {
            // Player uses a world space canvas
            playerCanvas.worldCamera = cam;
            playerRb = GetComponent<Rigidbody2D>();
            PlayerName = playerData.playerName;
            playerInvincibilityDuration = playerData.playerInvincibilityDuration;
            // Check if we are on mobile or desktop and use the correct input component
            if (SystemInfo.deviceType == DeviceType.Handheld)
            {
                playerInput = new PlayerTouchInput();
            }
            else
            {
                playerInput = new PlayerKeyboardInput(playerData.horizontalAxisName, playerData.fireButtonName);
            }
            playerGraphic = new PlayerGraphic(renderer, playerData.playerInvincibilityColor);
            playerHealth = new PlayerHealth(playerData.playerLives);
            playerScore = new PlayerScore();
            playerUi = new PlayerUI(livesText, scoreText, permaShotIndicator, doubleShotIndicator);
            playerWeapon = new PlayerWeapon(playerData.projectilePrefab, playerData.weaponColor,
                playerData.maxProjectilesAllowed, playerData.projectileSpeed, playerData.delay, firingPoint, cam, this);
            playerMotion = new PlayerMotion(playerData.playerSpeed, playerData.moveThreshold, playerData.maxVelocity,
                playerRb);
            _playerPowerUps = gameObject.AddComponent<PlayerPowerUps>();
            _playerPowerUps.Init(playerWeapon, playerUi, playerHealth);
            playerUi.SetLives(playerData.playerLives);
            playerUi.SetScore(0);
            // Register for projectile events
            Projectile.Projectile.onProjectileDestroyed += OnProjectileDestroyed;
        }

        private void OnProjectileDestroyed(int score, PlayerController player)
        {
            if (player == this)
            { 
                playerScore.ChangeScore(score);
                playerUi.SetScore(playerScore.Score);
            }
        }

        private void FixedUpdate()
        {
            // Check if the player is dead or the game ended
            if (PlayerDead || App.localState.gameOver) return;
            // Move the player
            playerMotion.MovePlayerHorizontally(playerInput.GetAxis());
            // Make sure the player faces the right direction
            if (Math.Abs(playerInput.GetAxis()) > 0.001f)
            {
                playerGraphic.FlipPlayer(playerInput.GetAxis() > 0);
            }
        }

        private void Update()
        {
            // Check if the player is dead or the game ended
            if (PlayerDead || App.localState.gameOver) return;
            // Implement a cooldown to avoid spamming the fire button
            if (!coolDown && playerInput.GetButton())
            {
                coolDown = true;
                playerWeapon.FireWeapon();
                Invoke(nameof(ResetCooldown), 0.1f);
            }
        }

        private void ResetCooldown()
        {
            coolDown = false;
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            // Check if we hit an enemy/obstacle
            if (other.gameObject.CompareTag(Constants.BALL_TAG) && !invincible)
            {
                if (playerHealth.Lives > 0)
                {
                    playerHealth.LoseLife();
                    playerUi.SetLives(playerHealth.Lives);
                    App.audioController.PlayAudio(Constants.HIT);
                    if (playerHealth.Lives <= 0)
                    {
                        HandlePlayerDeath();
                    }
                    else
                    {
                        // IFrames make sure you can't get hit immediately after you got hit 
                        StartCoroutine(InvincibilityFrames());
                    }
                }
            }
            else if (other.gameObject.CompareTag(Constants.POWERUP_TAG))
            {
                // Handle collecting a powerup
                powerUp = other.GetComponent<PowerUp.PowerUp>();
                _playerPowerUps.DoPowerUp(powerUp.PowerUpData.type, powerUp.PowerUpData.duration);
                other.gameObject.SetActive(false);
                App.audioController.PlayAudio(Constants.POWER_UP_SOUND);
            }
        }

        private void HandlePlayerDeath()
        {
            PlayerDead = true;
            playerUi.SetPlayerDead();
            // Raise an event saying the player died
            OnPlayerDead?.Invoke(this);
            App.audioController.PlayAudio(Constants.DEATH);
            Invoke(nameof(HidePlayer), playerInvincibilityDuration); 
        }

        private void HidePlayer()
        {
            gameObject.SetActive(false);
        }

        private IEnumerator InvincibilityFrames()
        {
            invincible = true;
            playerGraphic.Blink();
            yield return new WaitForSeconds(playerInvincibilityDuration);
            playerGraphic.StopBlink();
            invincible = false;
        }
    }
}