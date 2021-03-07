using System;
using DG.Tweening;
using UnityEngine;

namespace Utils
{
    public class Transition : MonoBehaviour
    {
        [SerializeField] private RectTransform panel;
        [SerializeField] private Ease easeIn, easeOut;

        private void Start()
        {
            panel.pivot= Vector2.up;
            panel.localScale = Vector2.up;
        }

        public void TransitionIn(float duration, Action complete = null)
        {
            panel.pivot = Vector2.one;
            panel.localScale = Vector2.up;
            panel.DOScaleX(1, duration).OnComplete(() => complete?.Invoke()).SetEase(easeIn);
        }

        public void TransitionOut(float duration, Action complete = null)
        {
            panel.pivot = Vector2.up;
            panel.localScale = Vector2.one;
            panel.DOScaleX(0, duration).OnComplete(() => complete?.Invoke()).SetEase(easeOut);
        }
    }
}