using DG.Tweening;
using UnityEngine;

namespace Player
{
    public class PlayerGraphic
    {
        private readonly SpriteRenderer _renderer;
        private readonly Color _playerColor, _playerInvincibilityColor;
        private readonly Vector2 _playerScale;
        private Vector2 cachedScale;
        private float scaleX;
        private Tween blinkTween;
        
        public PlayerGraphic(SpriteRenderer renderer, Color playerInvincibilityColor)
        {
            _renderer = renderer;
            _playerInvincibilityColor = playerInvincibilityColor;
            _playerScale = _renderer.transform.localScale;
            _playerColor = _renderer.color;
        }
        
        public void Blink()
        { 
            blinkTween = _renderer.DOColor(_playerInvincibilityColor, 0.07f).SetLoops(-1, LoopType.Yoyo);
        }

        public void StopBlink()
        {
            blinkTween.Kill();
            _renderer.color = _playerColor;
        }

        public void FlipPlayer(bool right)
        {
            cachedScale = _playerScale;
            scaleX = cachedScale.x;
            cachedScale.x = right ? scaleX * -1 : scaleX * 1;
            _renderer.transform.localScale = cachedScale;
        }
    }
}