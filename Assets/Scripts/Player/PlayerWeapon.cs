using Audio;
using Projectile;
using UnityEngine;
using Utils;

namespace Player
{
    public class PlayerWeapon
    {
        private readonly GameObject _projectilePrefab;
        private readonly float _floorY;
        private readonly PlayerController _playerController;
        private readonly Color _projectileColor;
        private readonly float _projectileSpeed;
        private readonly Transform _firingPoint;
        private int activeProjectiles;

        private int maxProjectilesAllowed;
        private ProjectileType projectileType;

        private GameObject projectile;
        private Projectile.Projectile projectileController;
        private Vector2 pos;

        public PlayerWeapon(GameObject projectile, Color projectileColor, int maxProjectiles, float projectileSpeed, Transform firingPoint,
            PlayerController playerController)
        {
            _floorY = -Utils.Utils.ScreenBounds(Camera.main).y;
            _projectilePrefab = projectile;
            _projectileColor = projectileColor;
            _projectileSpeed = projectileSpeed;
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
                if (activeProjectiles < 0)
                {
                    activeProjectiles = 0;
                }
            }
        }
        
        public void FireWeapon()
        {
            if (activeProjectiles < maxProjectilesAllowed)
            {
                activeProjectiles++;
                pos.x = _firingPoint.position.x;
                pos.y = _floorY;
                projectile = ObjectPooler.Instance.SpawnFromPool(_projectilePrefab);
                projectile.transform.position = pos;
                projectileController = projectile.GetComponent<Projectile.Projectile>();
                projectileController.Init(_projectileColor, _projectileSpeed, projectileType, _playerController);
                AudioController.Instance.PlayAudio(Constants.SHOT);
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