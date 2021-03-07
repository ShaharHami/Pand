using System.Collections.Generic;
using Data;
using UnityEngine;
using Utils;

namespace PowerUp
{
    public class PowerUpsSpawner
    {
        private static List<PowerUpData> _powerUps;
        private PowerUpData powerUpData;
        private GameObject powerUpGO;
        private PowerUp powerUp;

        public PowerUpsSpawner(List<PowerUpData> powerUps)
        {
            _powerUps = powerUps;
            Ball.Ball.HitBallEvent += SpawnPowerUp;
        }

        private void SpawnPowerUp(Vector2 position)
        {
            // Get random power up
            powerUpData = _powerUps[Random.Range(0, _powerUps.Count)];
            // Check if it should spawn and spawn it
            if (Random.Range(0f, 1f) < powerUpData.probability)
            {
                powerUpGO = ObjectPooler.Instance.SpawnFromPool(powerUpData.prefab);
                powerUpGO.transform.position = position;
                powerUp = powerUpGO.GetComponent<PowerUp>();
                powerUp.Init(powerUpData);
            }
        }
    }
}