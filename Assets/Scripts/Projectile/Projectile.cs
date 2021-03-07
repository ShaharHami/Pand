using System;
using Player;
using UnityEngine;
using Utils;

namespace Projectile
{
    public enum ProjectileType
    {
        DEFAULT,
        PERMA
    }
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer renderer;
        public static Action<int, PlayerController> onProjectileDestroyed;
        private PlayerController playerController;
        private float speed;
        private bool grow;
        private ProjectileType type;
        private Vector2 scale;
        private Vector2 initialScale;

        public void Init(Color projectileColor, float projectileSpeed, ProjectileType projectileType, PlayerController player)
        {
            speed = projectileSpeed;
            renderer.color = projectileColor;
            type = projectileType;
            playerController = player;
            initialScale.x = renderer.size.x;
            initialScale.y = 0;
            renderer.size = initialScale;
            grow = true;
        }
        private void Update()
        {
            if (!grow) return;
            scale.x = renderer.size.x;
            scale.y = renderer.size.y + speed * Time.deltaTime;
            renderer.size = scale;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            switch (other.tag)
            {
                case Constants.CEILING_TAG:
                    grow = false;
                    if (type != ProjectileType.PERMA)
                    {
                        DestroyProjectile(0);
                    }
                    break;
                case Constants.BALL_TAG:
                    DestroyProjectile(1);
                    break;
            }
        }

        private void DestroyProjectile(int score)
        {
            onProjectileDestroyed?.Invoke(score, playerController);
            gameObject.SetActive(false);
        }
    }
}