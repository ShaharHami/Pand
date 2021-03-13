using System;
using Data;
using Game;
using TMPro;
using UnityEngine;
using Utils;

namespace Player
{
    public class PlayerUI : GlobalAccess
    {
        private readonly TextAndUIData _data;
        private readonly TextMeshProUGUI _livesText;
        private readonly TextMeshProUGUI _scoreText;
        private readonly GameObject _permaShotIndicator, _doubleShotIndicator;

        public PlayerUI(TextMeshProUGUI livesText, TextMeshProUGUI scoreText, GameObject permaShotIndicator, GameObject doubleShotIndicator)
        {
            _permaShotIndicator = permaShotIndicator;
            _doubleShotIndicator = doubleShotIndicator;
            _livesText = livesText;
            _scoreText = scoreText;
            SetDoubleShot(false);
            SetPermaShot(false);
            _data = globalState.gameData.textAndUi;
        }

        public void SetPlayerDead()
        {
            _livesText.text = _data.dead;
        }
        
        public void SetLives(int lives)
        {
            _livesText.text = lives.ToString();
        }

        public void SetScore(int score)
        {
            _scoreText.text = score.ToString();
        }

        public void SetDoubleShot(bool active)
        {
            _doubleShotIndicator.SetActive(active);
        }
        
        public void SetPermaShot(bool active)
        {
            _permaShotIndicator.SetActive(active);
        }
    }
}