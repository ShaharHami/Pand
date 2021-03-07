using System;
using System.Collections;
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
        private float _delay;
        private float speed;
        private bool grow;
        private ProjectileType type;
        private Vector2 scale;
        private Vector2 initialScale;

        public void Init(Color projectileColor, float projectileSpeed, ProjectileType projectileType, float delay, PlayerController player)
        {
            speed = projectileSpeed;
            renderer.color = projectileColor;
            type = projectileType;
            _delay = delay;
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
                    else
                    {
                        StartCoroutine(DelayedDestroy());
                    }
                    break;
                case Constants.BALL_TAG:
                    DestroyProjectile(1);
                    break;
            }
        }

        private IEnumerator DelayedDestroy()
        {
            yield return new WaitForSeconds(_delay);
            if (gameObject.activeSelf)
            {
                DestroyProjectile(0);
            }
        }
        
        private void DestroyProjectile(int score)
        {
            onProjectileDestroyed?.Invoke(score, playerController);
            gameObject.SetActive(false);
        }
    }
}