using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace UIEnhance
{
    [RequireComponent(typeof(CanvasGroup))]
    public class UIContainer : MonoBehaviour, IUIElement
    {
        public float animationDuration = .25f;
        public float animationSequenceDuration = .25f;

        public UIManager.UIElementAnimation overrideAnimation;
        public Ease overrideEase;
        
        public float GetAnimationSequenceDuration() => animationSequenceDuration;

        public void Prepare()
        {
            UIManager.UIElementAnimation animation = UIManager.instance.defaultElementAnimation;
            
            if (overrideAnimation != UIManager.UIElementAnimation.None)
            {
                animation = overrideAnimation;
            }

            RectTransform rect = GetComponent<RectTransform>();
            switch (animation)
            {
                case UIManager.UIElementAnimation.Fade:
                    GetComponent<CanvasGroup>().alpha = 0;
                    break;

                case UIManager.UIElementAnimation.SlideFromLeft:
                    GetComponent<CanvasGroup>().alpha = 0;
                    rect.anchoredPosition = new Vector2(-rect.rect.size.x, rect.anchoredPosition.y);
                    break;
                case UIManager.UIElementAnimation.SlideFromRight:
                    GetComponent<CanvasGroup>().alpha = 0;
                    rect.anchoredPosition = new Vector2(rect.rect.size.x, rect.anchoredPosition.y);
                    break;

                case UIManager.UIElementAnimation.ScaleAndFade:
                    GetComponent<CanvasGroup>().alpha = 0;
                    rect.localScale = Vector3.zero;
                    break;
            }
        }

        public void Animate()
        {
            UIManager.UIElementAnimation animation = UIManager.instance.defaultElementAnimation;
            Ease ease = UIManager.instance.defaultElementEase;
            
            if (overrideAnimation != UIManager.UIElementAnimation.None)
            {
                animation = overrideAnimation;
            }

            if (overrideEase != Ease.Unset)
            {
                ease = overrideEase;
            }

            RectTransform rect = GetComponent<RectTransform>();

            switch (animation)
            {
                case UIManager.UIElementAnimation.Fade:
                    GetComponent<CanvasGroup>().DOFade(1f, animationDuration).SetEase(ease);
                    break;

                case UIManager.UIElementAnimation.SlideFromLeft:
                case UIManager.UIElementAnimation.SlideFromRight:
                    GetComponent<CanvasGroup>().DOFade(1f, animationDuration).SetEase(ease);
                    rect.DOAnchorPosX(0, animationDuration).SetEase(ease);
                    break;

                case UIManager.UIElementAnimation.ScaleAndFade:
                    GetComponent<CanvasGroup>().DOFade(1f, animationDuration).SetEase(ease);
                    rect.DOScale(Vector3.one, animationDuration).SetEase(ease);
                    break;
            }
        }

    }
}