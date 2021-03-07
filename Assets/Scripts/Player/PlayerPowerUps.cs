using System.Collections;
using PowerUp;
using Projectile;
using UnityEngine;

namespace Player
{
    public class PlayerPowerUps : MonoBehaviour
    {
        private PlayerWeapon _playerWeapon;
        private PlayerUI _playerUi;
        private PlayerHealth _playerHealth;
        private bool doubleShotActive, permaShotActive;
        private Coroutine doubleShotCoroutine, permaShotCoroutine;
        public void Init(PlayerWeapon playerWeapon, PlayerUI playerUi, PlayerHealth playerHealth)
        {
            _playerWeapon = playerWeapon;
            _playerHealth = playerHealth;
            _playerUi = playerUi;
        }
        public void DoPowerUp(PowerUpType type, int duration)
        {
            switch (type)
            {
                case PowerUpType.DOUBLESHOT:
                    if (doubleShotActive)
                    {
                        StopCoroutine(doubleShotCoroutine);
                    }
                    doubleShotCoroutine = StartCoroutine(DoubleShotCoroutine(duration));
                    break;
                case PowerUpType.PERMASHOT:
                    if (permaShotActive)
                    {
                        StopCoroutine(permaShotCoroutine);
                    }
                    permaShotCoroutine = StartCoroutine(PermaShotCoroutine(duration));
                    break;
                case PowerUpType.ONEUP:
                    OneUp();
                    break;
            }
        }

        private void OneUp()
        {
            _playerHealth.GainLife();
            _playerUi.SetLives(_playerHealth.Lives);
        }

        private IEnumerator DoubleShotCoroutine(int duration)
        {
            doubleShotActive = true;
            _playerWeapon.ChangeMaxAllowedProjectiles(2);
            _playerUi.SetDoubleShot(true);
            yield return new WaitForSeconds(duration);
            _playerWeapon.ChangeMaxAllowedProjectiles(1);
            _playerUi.SetDoubleShot(false);
            doubleShotActive = false;
        }

        private IEnumerator PermaShotCoroutine(int duration)
        {
            permaShotActive = true;
            _playerWeapon.ChangeProjectileType(ProjectileType.PERMA);
            _playerUi.SetPermaShot(true);
            yield return new  WaitForSeconds(duration);
            _playerWeapon.ChangeProjectileType(ProjectileType.DEFAULT);
            _playerUi.SetPermaShot(false);
            permaShotActive = false;
        }
    }
}