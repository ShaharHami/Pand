using Audio;
using Projectile;
using UnityEngine;
using Utils;

namespace Player
{
    public class PlayerWeapon : App.AppBase
    {
        private readonly GameObject _projectilePrefab;
        private readonly float _floorY;
        private readonly PlayerController _playerController;
        private readonly Color _projectileColor;
        private readonly float _projectileSpeed;
        private readonly Transform _firingPoint;
        private readonly float _delay;
        private int activeProjectiles;

        private int maxProjectilesAllowed;
        private ProjectileType projectileType;

        private GameObject projectile;
        private Projectile.Projectile projectileController;
        private Vector2 pos;

        public PlayerWeapon(GameObject projectile, Color projectileColor, int maxProjectiles, float projectileSpeed, float delay, Transform firingPoint, Camera cam,
            PlayerController playerController)
        {
            _floorY = -Utils.Utils.ScreenBounds(cam).y;
            _projectilePrefab = projectile;
            _projectileColor = projectileColor;
            _projectileSpeed = projectileSpeed;
            _delay = delay;
            projectileType = ProjectileType.DEFAULT;
            _firingPoint = firingPoint;
            _playerController = playerController;
            maxProjectilesAllowed = maxProjectiles;
            Projectile.Projectile.onProjectileDestroyed += OnProjectileDestroyed;
        }

        private void OnProjectileDestroyed(int score, PlayerController playerController)
        {
            if (playerController == _playerController)
            {
                activeProjectiles--;
                // This was put in place to handle edge cases
                if (activeProjectiles < 0)
                {
                    activeProjectiles = 0;
                }
            }
        }
        
        public void FireWeapon()
        {
            // Check if there are less projectiles fired than are allowed at the same time
            if (activeProjectiles < maxProjectilesAllowed)
            {
                activeProjectiles++;
                pos.x = _firingPoint.position.x;
                pos.y = _floorY;
                projectile = objectPooler.SpawnFromPool(_projectilePrefab.name);
                projectile.transform.position = pos;
                projectileController = projectile.GetComponent<Projectile.Projectile>();
                projectileController.Init(_projectileColor, _projectileSpeed, projectileType, _delay, _playerController);
                audioController.PlayAudio(Constants.SHOT);
            }
        }

        public void ChangeMaxAllowedProjectiles(int max)
        {
            maxProjectilesAllowed = max;
        }

        public void ChangeProjectileType(ProjectileType type)
        {
            projectileType = type;
        }
    }
}