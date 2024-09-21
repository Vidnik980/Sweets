using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UIEnhance
{
    [RequireComponent(typeof(CanvasGroup))]
    public class UIPanel : MonoBehaviour
    {
        public string panelName;
        public float animationDuration;

        [Header("Panel settings")]
        public UIManager.UIPanelAnimation overrideAnimation;
        public Ease overrideEase;

        [Header("Child elements settings")] 
        public bool reinitElementsOnPanelEnable;
        public bool sequentialChildElements;

        private List<IUIElement> _elements = new List<IUIElement>();

        private void Awake()
        {
            InitElements();
        }

        private void OnEnable()
        {
            StartCoroutine(UpdateRect());

            if (reinitElementsOnPanelEnable)
            {
                InitElements();
            }
        }

        public virtual void Open(Action onOpenCallback, UIManager.UIPanelAnimation animation, Ease ease)
        {
            if (overrideAnimation != UIManager.UIPanelAnimation.None)
            {
                animation = overrideAnimation;
            }

            if (overrideEase != Ease.Unset)
            {
                ease = overrideEase;
            }

            RectTransform rect = GetComponent<RectTransform>();
            Vector2 defaultPivot = rect.pivot;

            foreach (IUIElement element in _elements)
            {
                element.Prepare();
            }

            switch (animation)
            {
                case UIManager.UIPanelAnimation.None:
                    onOpenCallback?.Invoke();
                    AnimateUIElements();
                    break;
                case UIManager.UIPanelAnimation.Fade:
                    rect.anchoredPosition = Vector2.zero;
                    GetComponent<CanvasGroup>().alpha = 0;
                    GetComponent<CanvasGroup>().DOFade(1f, animationDuration).OnComplete(() =>
                    {
                        onOpenCallback?.Invoke();
                        AnimateUIElements();
                    }).SetEase(ease);
                    break;

                case UIManager.UIPanelAnimation.Swap:
                    rect.anchoredPosition = new Vector2(-rect.rect.size.x, rect.anchoredPosition.y);
                    rect.DOAnchorPosX(0, animationDuration).OnComplete(() =>
                    {
                        onOpenCallback?.Invoke();
                        AnimateUIElements();
                    }).SetEase(ease);
                    break;
                case UIManager.UIPanelAnimation.Slide:
                    rect.anchoredPosition = new Vector2(-rect.rect.size.x, rect.anchoredPosition.y);
                    rect.DOAnchorPosX(0, animationDuration).OnComplete(() =>
                    {
                        onOpenCallback?.Invoke();
                        AnimateUIElements();
                    }).SetEase(ease);
                    break;

                case UIManager.UIPanelAnimation.SwapAndFade:
                    GetComponent<CanvasGroup>().alpha = 0;
                    GetComponent<CanvasGroup>().DOFade(1f, animationDuration).OnComplete(() =>
                    {
                        onOpenCallback?.Invoke();
                        AnimateUIElements();
                    }).SetEase(ease);

                    rect.anchoredPosition = new Vector2(-rect.rect.size.x, rect.anchoredPosition.y);
                    rect.DOAnchorPosX(0, animationDuration).SetEase(ease);
                    break;

                case UIManager.UIPanelAnimation.SlideAndFade:
                    GetComponent<CanvasGroup>().alpha = 0;
                    GetComponent<CanvasGroup>().DOFade(1f, animationDuration).OnComplete(() =>
                    {
                        onOpenCallback?.Invoke();
                        AnimateUIElements();
                    }).SetEase(ease);

                    rect.anchoredPosition = new Vector2(-rect.rect.size.x, rect.anchoredPosition.y);
                    rect.DOAnchorPosX(0, animationDuration).SetEase(ease);
                    break;

                case UIManager.UIPanelAnimation.ScaleAndFade:
                    GetComponent<CanvasGroup>().alpha = 0;
                    GetComponent<CanvasGroup>().DOFade(1f, animationDuration).OnComplete(() =>
                    {
                        onOpenCallback?.Invoke();
                        AnimateUIElements();
                    }).SetEase(ease);

                    rect.localScale = Vector3.zero;
                    rect.DOScale(Vector3.one, animationDuration).SetEase(ease);
                    break;

                case UIManager.UIPanelAnimation.RotateAtTopLeftCornerAndFade:

                    GetComponent<CanvasGroup>().alpha = 0;
                    GetComponent<CanvasGroup>().DOFade(1f, animationDuration).OnComplete(() =>
                    {
                        rect.pivot = defaultPivot;
                        onOpenCallback?.Invoke();
                        AnimateUIElements();
                    }).SetEase(ease);

                    rect.pivot = new Vector2(0, 1);
                    rect.Rotate(0, 0, -90f);

                    rect.DORotate(Vector3.zero, animationDuration).SetEase(ease);
                    break;

                case UIManager.UIPanelAnimation.RotateAtTopRightCornerAndFade:

                    GetComponent<CanvasGroup>().alpha = 0;
                    GetComponent<CanvasGroup>().DOFade(1f, animationDuration).OnComplete(() =>
                    {
                        rect.pivot = defaultPivot;
                        onOpenCallback?.Invoke();
                        AnimateUIElements();
                    }).SetEase(ease);

                    rect.pivot = new Vector2(1, 1);
                    rect.Rotate(0, 0, -90f);

                    rect.DORotate(Vector3.zero, animationDuration).SetEase(ease);
                    break;

                case UIManager.UIPanelAnimation.RotateAtCenterAndFade:

                    GetComponent<CanvasGroup>().alpha = 0;
                    GetComponent<CanvasGroup>().DOFade(1f, animationDuration).OnComplete(() =>
                    {
                        onOpenCallback?.Invoke();
                        AnimateUIElements();
                    }).SetEase(ease);

                    rect.Rotate(0, 0, -90f);
                    rect.DORotate(Vector3.zero, animationDuration).SetEase(ease);
                    break;

            }
        }

        public virtual void Close(Action onCloseCallback, UIManager.UIPanelAnimation animation, Ease ease)
        {
            if (overrideAnimation != UIManager.UIPanelAnimation.None)
            {
                animation = overrideAnimation;
            }

            if (overrideEase != Ease.Unset)
            {
                ease = overrideEase;
            }

            RectTransform rect = GetComponent<RectTransform>();
            Vector2 defaultPivot = rect.pivot;

            switch (animation)
            {
                case UIManager.UIPanelAnimation.None:
                    onCloseCallback?.Invoke();
                    gameObject.SetActive(false);
                    break;
                case UIManager.UIPanelAnimation.Fade:
                    rect.anchoredPosition = Vector2.zero;
                    GetComponent<CanvasGroup>().alpha = 1f;
                    GetComponent<CanvasGroup>().DOFade(0f, animationDuration).OnComplete(() =>
                    {
                        onCloseCallback?.Invoke();
                        gameObject.SetActive(false);
                    }).SetEase(ease);
                    break;

                case UIManager.UIPanelAnimation.Swap:
                    rect.anchoredPosition = Vector2.zero;
                    rect.DOAnchorPosX(-rect.rect.size.x, animationDuration).OnComplete(() =>
                    {
                        onCloseCallback?.Invoke();
                        gameObject.SetActive(false);
                    }).SetEase(ease);
                    break;

                case UIManager.UIPanelAnimation.Slide:
                    rect.anchoredPosition = Vector2.zero;
                    rect.DOAnchorPosX(rect.rect.size.x * 2, animationDuration).OnComplete(() =>
                    {
                        onCloseCallback?.Invoke();
                        gameObject.SetActive(false);
                    }).SetEase(ease);
                    break;

                case UIManager.UIPanelAnimation.SwapAndFade:
                    GetComponent<CanvasGroup>().alpha = 1f;
                    GetComponent<CanvasGroup>().DOFade(0f, animationDuration).OnComplete(() =>
                    {
                        onCloseCallback?.Invoke();
                        gameObject.SetActive(false);
                    }).SetEase(ease);

                    rect.anchoredPosition = Vector2.zero;
                    rect.DOAnchorPosX(-rect.rect.size.x, animationDuration).SetEase(ease);
                    break;

                case UIManager.UIPanelAnimation.SlideAndFade:
                    GetComponent<CanvasGroup>().alpha = 1f;
                    GetComponent<CanvasGroup>().DOFade(0f, animationDuration).OnComplete(() =>
                    {
                        onCloseCallback?.Invoke();
                        gameObject.SetActive(false);
                    }).SetEase(ease);

                    rect.anchoredPosition = Vector2.zero;
                    rect.DOAnchorPosX(rect.rect.size.x * 2, animationDuration).SetEase(ease);
                    break;

                case UIManager.UIPanelAnimation.ScaleAndFade:
                    GetComponent<CanvasGroup>().alpha = 1f;
                    GetComponent<CanvasGroup>().DOFade(0f, animationDuration).OnComplete(() =>
                    {
                        onCloseCallback?.Invoke();
                        gameObject.SetActive(false);
                    }).SetEase(ease);

                    rect.localScale = Vector3.one;
                    rect.DOScale(Vector3.zero, animationDuration).SetEase(ease);
                    break;

                case UIManager.UIPanelAnimation.RotateAtTopLeftCornerAndFade:

                    GetComponent<CanvasGroup>().alpha = 1f;
                    GetComponent<CanvasGroup>().DOFade(0f, animationDuration).OnComplete(() =>
                    {
                        rect.pivot = defaultPivot;
                        onCloseCallback?.Invoke();
                        gameObject.SetActive(false);
                    }).SetEase(ease);

                    rect.pivot = new Vector2(0, 1);
                    rect.Rotate(0, 0, 0);

                    rect.DORotate(new Vector3(0, 0, -90f), animationDuration).SetEase(ease);
                    break;

                case UIManager.UIPanelAnimation.RotateAtTopRightCornerAndFade:

                    GetComponent<CanvasGroup>().alpha = 1f;
                    GetComponent<CanvasGroup>().DOFade(0f, animationDuration).OnComplete(() =>
                    {
                        rect.pivot = defaultPivot;
                        onCloseCallback?.Invoke();
                        gameObject.SetActive(false);
                    }).SetEase(ease);

                    rect.pivot = new Vector2(1, 1);
                    rect.Rotate(0, 0, 0);

                    rect.DORotate(new Vector3(0, 0, -90f), animationDuration).SetEase(ease);
                    break;

                case UIManager.UIPanelAnimation.RotateAtCenterAndFade:

                    GetComponent<CanvasGroup>().alpha = 1f;
                    GetComponent<CanvasGroup>().DOFade(0f, animationDuration).OnComplete(() =>
                    {
                        onCloseCallback?.Invoke();
                        gameObject.SetActive(false);
                    }).SetEase(ease);

                    rect.Rotate(0, 0, 0);
                    rect.DORotate(new Vector3(0, 0, -90f), animationDuration).SetEase(ease);
                    break;
            }
        }

        private void AnimateUIElements()
        {
            if (!sequentialChildElements)
            {
                foreach (IUIElement element in _elements)
                {
                    if (element != null)
                        element.Animate();
                }
            }
            else
            {
                StartCoroutine(AnimateElementsSequence());
            }
        }

        IEnumerator AnimateElementsSequence()
        {
            foreach (IUIElement element in _elements)
            {
                if (element != null)
                {
                    element.Animate();
                    yield return new WaitForSeconds(element.GetAnimationSequenceDuration());
                }
            }
        }

        private IEnumerator UpdateRect()
        {
            yield return new WaitForEndOfFrame();
            foreach (HorizontalOrVerticalLayoutGroup gr in FindObjectsOfType<HorizontalOrVerticalLayoutGroup>())
            {
                LayoutRebuilder.ForceRebuildLayoutImmediate(gr.transform as RectTransform);
            }
        }
        public void InitElements()
        {
            _elements = GetComponentsInChildren<IUIElement>().ToList();
        }
    }
}   