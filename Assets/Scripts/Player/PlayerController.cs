using System;
using System.Collections;
using Audio;
using Data;
using Game;
using Player.Interfaces;
using TMPro;
using UnityEngine;
using Utils;

namespace Player
{
    public class PlayerController : MonoBehaviour
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

        public void Init(PlayerData playerData)
        {
            playerCanvas.worldCamera = Camera.main;
            playerRb = GetComponent<Rigidbody2D>();
            PlayerName = playerData.playerName;
            playerInvincibilityDuration = playerData.playerInvincibilityDuration;
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
                playerData.maxProjectilesAllowed, playerData.projectileSpeed, firingPoint, this);
            playerMotion = new PlayerMotion(playerData.playerSpeed, playerData.moveThreshold, playerData.maxVelocity,
                playerRb);
            _playerPowerUps = gameObject.AddComponent<PlayerPowerUps>();
            _playerPowerUps.Init(playerWeapon, playerUi, playerHealth);
            playerUi.SetLives(playerData.playerLives);
            playerUi.SetScore(0);
            Projectile.Projectile.onProjectileDestroyed += OnProjectileDestroyed;
        }

        private void OnProjectileDestroyed(int obj, PlayerController player)
        {
            if (player == this)
            { 
                playerScore.ChangeScore(obj);
                playerUi.SetScore(playerScore.Score);
            }
        }

        private void FixedUpdate()
        {
            if (PlayerDead || LocalState.Instance.gameOver) return;
            playerMotion.MovePlayerHorizontally(playerInput.GetAxis());
            if (Math.Abs(playerInput.GetAxis()) > 0.001f)
            {
                playerGraphic.FlipPlayer(playerInput.GetAxis() > 0);
            }
        }

        private void Update()
        {
            if (PlayerDead || LocalState.Instance.gameOver) return;
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
            if (other.gameObject.CompareTag(Constants.BALL_TAG) && !invincible)
            {
                if (playerHealth.Lives > 0)
                {
                    playerHealth.LoseLife();
                    playerUi.SetLives(playerHealth.Lives);
                    if (playerHealth.Lives <= 0)
                    {
                        HandlePlayerDeath();
                    }
                    else
                    {
                        StartCoroutine(InvincibilityFrames());
                    }
                }
                AudioController.Instance.PlayAudio(Constants.HIT);
            }
            else if (other.gameObject.CompareTag(Constants.POWERUP_TAG))
            {
                powerUp = other.GetComponent<PowerUp.PowerUp>();
                _playerPowerUps.DoPowerUp(powerUp.PowerUpData.type, powerUp.PowerUpData.duration);
                other.gameObject.SetActive(false);
                AudioController.Instance.PlayAudio(Constants.POWER_UP_SOUND);
            }
        }

        private void HandlePlayerDeath()
        {
            PlayerDead = true;
            playerUi.SetPlayerDead();
            OnPlayerDead?.Invoke(this);
            AudioController.Instance.PlayAudio(Constants.DEATH);
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