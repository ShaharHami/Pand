using Audio;
using Data;
using Player;
using UnityEngine;
using Utils;

namespace PowerUp
{
    public enum PowerUpType
    {
        DOUBLESHOT,
        PERMASHOT,
        ONEUP
    }
    public class PowerUp : MonoBehaviour
    {
        private PowerUpData data;
        private PlayerController playerController;
        private Rigidbody2D rb;

        public PowerUpData PowerUpData => data;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        public void Init(PowerUpData powerUpData)
        {
            data = powerUpData;
            rb.isKinematic = false;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(Constants.FLOOR_TAG))
            {
                rb.velocity = Vector2.zero;
                rb.isKinematic = true;
            }
        }
    }
}